using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopSi.Models
{
    public class LoginModel
    {
        [Display(Name ="Tên tài khoản")]
        [Required(ErrorMessage ="Mời bạn nhập tài khoản")]
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Mời bạn nhập mật khẩu")]
        public string Password { get; set; }
    }
}