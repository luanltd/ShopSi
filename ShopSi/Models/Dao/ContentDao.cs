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


            if (string.IsNullOrEmpty(content.MetaTitle))
            {
                content.MetaTitle = StringHelper.ToUnsignString(content.Name);

            }
            var model = db.Contents.Find(content.ID);
            model.Image = content.Image;
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
            db.SaveChanges();

            //xử lý tag
            if (!string.IsNullOrEmpty(content.Tags))
            {
                this.RemoveAllContentTag(content.ID);
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
                    this.InsertContentTag(content.ID, tagId);
                }
            }
           
            return content.ID;
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

    }
}
