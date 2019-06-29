using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EF;

namespace Models.Dao
{
    public class MenuDao
    {
        ShopSiDbContext db = null;
        public MenuDao()
        {
            db = new ShopSiDbContext();
        }
        public List<Menu> GetTopMenu(int typeID)
        {
            var model= db.Menus.Where(x=>x.TypeID==typeID).ToList();
            return model;
        }
    }
}
