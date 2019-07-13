namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public long ID { get; set; }

        [StringLength(50)]
        [Display(Name ="Tài khoản")]
        [Required(ErrorMessage ="Mời bạn nhập tài khoản")]
        public string UserName { get; set; }

        [StringLength(20)]
        //[Display(Name = "Tài khoản")]
        //[Required(ErrorMessage = "Mời bạn nhập tài khoản")]
        public string GroupID { get; set; }


        [StringLength(32)]
        [Display(Name = "Mật khẩu")]
        //[Required(ErrorMessage ="Mời bạn nhập vào mật khẩu")]
        public string Password { get; set; }

        [StringLength(50)]
        [Display(Name = "Họ tên")]
        [Required(ErrorMessage ="Mời bạn nhập vào họ tên")]
        public string Name { get; set; }

        [StringLength(50)]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Mời bạn nhập vào Email")]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(50)]
        [Display(Name = "Điện thoại")]
        public string Phone { get; set; }

        [Display(Name = "Tỉnh/Thành")]
        public int? ProvinceID { get; set; }
        [Display(Name = "Quận/Huyện")]
        public int? DistrictID { get; set; }

        public DateTime CreatedDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }
        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
    }
}
