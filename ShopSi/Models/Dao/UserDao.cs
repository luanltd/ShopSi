using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EF;
using PagedList;
using Common;

namespace Models.Dao
{
   public class UserDao
    {
        ShopSiDbContext db = null;
        //contructions
        public UserDao()
        {
            db = new ShopSiDbContext();
        }
        //Hàm login
        public int Login(string username, string password, bool isLoginAdmin=false)
        {
            var model = db.Users.SingleOrDefault(x => x.UserName == username);
            if (model == null)
            {
                return -1;
            }
           
            else
            {
                if (isLoginAdmin == true)
                {
                    if (model.GroupID == CommonUser.USER_ADMIN || model.GroupID == CommonUser.USER_MOD)
                    {
                        if (model.Status == false)
                        {
                            return -2;
                        }
                        else if (model.Password == password)
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        return 3;
                    }
                }

                else
                {
                    if (model.Status == false)
                    {
                        return -2;
                    }
                    else if (model.Password == password)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
               
            }
          
        }
        //Hàm lấy ra ID của user
        public long GetById(string username)
        {
          var user=  db.Users.SingleOrDefault(x => x.UserName == username);
          return user.ID;
        }
        //Hàm lấy ra User
        public User GetUser(string username)
        {
            return  db.Users.SingleOrDefault(x => x.UserName == username);
          
        }
        //Hàm thêm user
        public long Insert(User model)
        {
            db.Users.Add(model);
            db.SaveChanges();
            return model.ID;
        }
        //hàm kiểm tra username có tồn tại không
        public bool CheckUserName(string username)
        {
            var model = db.Users.SingleOrDefault(x => x.UserName == username);
            if (model == null)
                return true;
            else
                return false;
        }
        //hàm kiểm tra email có tồn tại không
        public bool CheckEmail(string email)
        {
            var model = db.Users.SingleOrDefault(x => x.Email == email);
            if (model == null)
                return true;
            else
                return false;
        }
        //hàm lấy ra tất cả user và phân trang
        public IEnumerable<User> GetAllUser(string searching,int page,int pageSize)
        {
            IEnumerable<User> model = db.Users;
            if (!string.IsNullOrEmpty(searching))
            {
                model = model.Where(x => x.Name.Contains(searching)||x.UserName.Contains(searching));
            }
            return model = model.OrderByDescending(x => x.ID).ToPagedList(page,pageSize);
           // return model;
        }
        //hàm lấy ra user khi có id
        public User GetUserById(long id)
        {
            return db.Users.Find(id);
        }
        //hàm cập nhập user
        public bool Update(User user)
        {
            try
            {
                var model = db.Users.Find(user.ID);
                model.Name = user.Name;
                model.Address = user.Address;
                model.Email = user.Email;
                model.Phone = user.Phone;
                model.Status = user.Status;
                model.CreatedDate = DateTime.Now;
                if (user.ProvinceID != null)
                {
                    model.ProvinceID = user.ProvinceID;
                }
                else
                {
                    model.ProvinceID = model.ProvinceID;
                }
                if (user.DistrictID != null)
                {
                    model.DistrictID = user.DistrictID;
                }
                else
                {
                    model.DistrictID = model.DistrictID;
                }
             
                if (!string.IsNullOrEmpty(user.Password))
                {
                    model.Password = user.Password;
                }
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        //hàm xóa user theo id
        public bool DeleteById(long id)
        {
            var model =db.Users.Find(id);
            db.Users.Remove(model);
            db.SaveChanges();
            return true;
        }
        //hàm thêm user thông qua facebook
        public long InsertForFacebook(User model)
        {
            var user = db.Users.SingleOrDefault(x => x.UserName == model.UserName);
            if (user == null)
            {
                db.Users.Add(model);
                db.SaveChanges();
                return model.ID;
            }
            else
            {
                return user.ID;
            }

        }
        //hàm thay đổi trạng thái cho phép khóa hoặc kích hoạt người dùng
        public bool ChangeStatus(long id)
        {
            var model = db.Users.Find(id);
            model.Status =! model.Status;
            db.SaveChanges();
            return model.Status;
        }
        //hàm phân quyền người dùng
        public List<string> GetListCredential(string username)
        {
            var user = db.Users.Single(x => x.UserName == username);
            var data = (from a in db.Credentials
                       join b in db.UserGroups on a.UserGroupID equals b.ID
                       join c in db.Roles on a.RoleID equals c.ID
                       where b.ID == user.GroupID
                       select new
                       {
                           RoleID = a.RoleID,
                           UserGroupID = a.UserGroupID
                       }).AsEnumerable().Select(x=>new Credential() {
                           RoleID = x.RoleID,
                           UserGroupID = x.UserGroupID
                       });
            return data.Select(x => x.RoleID).ToList();
        }
    }
}
