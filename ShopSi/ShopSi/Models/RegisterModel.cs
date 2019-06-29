using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopSi.Models
{
    public class RegisterModel
    {
        public long ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Address { set; get; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ProvinceID { get; set; }
        public int? DistrictID { get; set; }


    }
}