
namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("Role")]
    public partial class Role
    {
        [Key]
        [StringLength(50)]
        //[Display(Name = "Tài khoản")]
        //[Required(ErrorMessage = "Mời bạn nhập tài khoản")]
        public string ID { get; set; }

        [StringLength(50)]
        //[Display(Name = "Tài khoản")]
        //[Required(ErrorMessage = "Mời bạn nhập tài khoản")]
        public string Name { get; set; }
    }
}
