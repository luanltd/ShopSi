using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EF;
using Common;
using PagedList;
namespace Models.Dao
{
   public class ContentDao
    {
        ShopSiDbContext db = null;
        public ContentDao()
        {
            db = new ShopSiDbContext();
        }

        public Content GetById(long id)
        {
            return db.Contents.Find(id);
        }

        public long Create(Content content)
        {
            //xử lý alias
            if (string.IsNullOrEmpty(content.MetaTitle))
            {
                content.MetaTitle = StringHelper.ToUnsignString(content.Name);

            }
            db.Contents.Add(content);
            db.SaveChanges();

            //xử lý tag
            if (!string.IsNullOrEmpty(content.Tags))
            {
                string[] tags = content.Tags.Split(',');
                foreach(var tag in tags)
                {

                    var tagId = StringHelper.ToUnsignString(tag);
                    var existedTag = this.CheckTag(tagId);
                    //inset tag to table tag
                    if (!existedTag)
                    {
                        this.InsertTag(tagId, tag);
                    }
                    //insert contenttag in table
                    this.InsertContentTag(content.ID, tagId);
                }
            }
            return content.ID;
        }

        public long Edit(Content content)
        {
            //xử lý alias
            var model = db.Contents.Find(content.ID);
            model.Image = content.Image;
            model.MetaTitle = content.MetaTitle;
            model.CategoryID = content.CategoryID;
            model.CreatedBy = content.CreatedBy;
            model.Description = content.Description;
            model.Detail = content.Detail;
            model.Name = content.Name;
            model.Status = content.Status;
            model.TopHot = content.TopHot;
            model.ViewCount = content.ViewCount;
            model.Tags = content.Tags;
            model.CreatedDate = DateTime.Now;

            if (string.IsNullOrEmpty(model.MetaTitle))
            {
                model.MetaTitle = StringHelper.ToUnsignString(model.Name);
            }
         
            db.SaveChanges();

            //xử lý tag
            if (!string.IsNullOrEmpty(model.Tags))
            {
                this.RemoveAllContentTag(model.ID);
                string[] tags = content.Tags.Split(',');
                foreach (var tag in tags)
                {

                    var tagId = StringHelper.ToUnsignString(tag);
                    var existedTag = this.CheckTag(tagId);
                    //inset tag to table tag
                    if (!existedTag)
                    {
                        this.InsertTag(tagId, tag);
                    }
                    //insert contenttag in table
                    this.InsertContentTag(model.ID, tagId);
                }
            }
           
            return model.ID;
        }

        public void RemoveAllContentTag(long contentid)
        {
            db.ContentTags.RemoveRange(db.ContentTags.Where(x => x.ContentID == contentid));
            db.SaveChanges(); 
        }

        public void InsertTag(string id, string name)
        {
            var tag = new Tag();
            tag.ID = id;
            tag.Name = name;
            db.Tags.Add(tag);
            db.SaveChanges();
        }

        public void InsertContentTag(long contentid, string tagid)
        {
            var contenttag = new ContentTag();
            contenttag.ContentID = contentid;
            contenttag.TagID = tagid;
            db.ContentTags.Add(contenttag);
            db.SaveChanges();
        }

        public bool CheckTag(string id)
        {
            return db.Tags.Count(x => x.ID == id) > 0;
        }

        public IEnumerable<Content> GetAllContent(string searching, int page, int pageSize)
        {
            IEnumerable<Content> model = db.Contents;
            if (!string.IsNullOrEmpty(searching))
            {
                model = model.Where(x => x.Name.Contains(searching));
            }
            return model = model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
            // return model;
        }

        //Phan trang danh cho client
        public IEnumerable<Content> GetAllContent(ref int totalRecode, int page, int pageSize)
        {
            IEnumerable<Content> model = db.Contents;
            totalRecode = model.Count();
            return model = model.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            // return model;
        }


        public IEnumerable<Content> ListAllByTag(ref int totalRecode,string tag,int page, int pageSize)
        {
            IEnumerable<Content> model = (from a in db.Contents
                         join b in db.ContentTags
                         on a.ID equals b.ContentID
                         where b.TagID == tag
                         select new
                         {
                             Name = a.Name,
                             MetaTitle = a.MetaTitle,
                             Image = a.Image,
                             Description = a.Description,
                             CreatedDate = a.CreatedDate,
                             CreatedBy = a.CreatedBy,
                             ID = a.ID
                         }).AsEnumerable().Select(x => new Content()
                         {
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Image = x.Image,
                             Description = x.Description,
                             CreatedDate = x.CreatedDate,
                             CreatedBy = x.CreatedBy,
                             ID = x.ID
                         });
            totalRecode = model.Count();
            return model = model.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            // return model;
        }

        public Tag GetTag(string id)
        {
            return db.Tags.Find(id);
        }

        public List<Tag> ListTag(long contentid)
        {
            var model = (from a in db.Tags
                         join b in db.ContentTags
                         on a.ID equals b.TagID
                         where b.ContentID == contentid
                         select new
                         {
                             ID = b.TagID,
                             Name = a.Name
                         }).AsEnumerable().Select(x => new Tag()
                         {
                             ID=x.ID,
                             Name=x.Name

                         });

            return model.ToList();
        }
    }
}
