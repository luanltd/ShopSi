using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopSi.Areas.Admin.Models
{
    public class District
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ProvinceID { get; set; }
    }
}