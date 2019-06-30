using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EF;
using PagedList;

namespace Models.Dao
{
   public class UserDao
    {
        ShopSiDbContext db = null;
        public UserDao()
        {
            db = new ShopSiDbContext();
        }
        public int Login(string username, string password)
        {
            var model = db.Users.SingleOrDefault(x => x.UserName == username);
            if (model == null)
            {
                return -1;
            }
            else if (model.Status == false)
            {
                return -2;
            }
            else
            {
                if (model.Password == password)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
          
        }

        public long GetById(string username)
        {
          var user=  db.Users.SingleOrDefault(x => x.UserName == username);
          return user.ID;
        }

        public long Insert(User model)
        {
            db.Users.Add(model);
            db.SaveChanges();
            return model.ID;
        }
        public bool CheckUserName(string username)
        {
            var model = db.Users.SingleOrDefault(x => x.UserName == username);
            if (model == null)
                return true;
            else
                return false;
        }
        public bool CheckEmail(string email)
        {
            var model = db.Users.SingleOrDefault(x => x.Email == email);
            if (model == null)
                return true;
            else
                return false;
        }
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

        public User GetUserById(long id)
        {
            return db.Users.Find(id);
        }

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
                model.ProvinceID = user.ProvinceID;
                model.DistrictID = user.DistrictID;
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

        public bool DeleteById(long id)
        {
            var model =db.Users.Find(id);
            db.Users.Remove(model);
            db.SaveChanges();
            return true;
        }

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
    }
}
