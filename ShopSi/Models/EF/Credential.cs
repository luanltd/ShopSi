
namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Credential")]
    [Serializable]
    public partial class Credential
    {
        [Key]
        [StringLength(20)]
        //[Display(Name = "Tài khoản")]
        //[Required(ErrorMessage = "Mời bạn nhập tài khoản")]
        public string UserGroupID { get; set; }

        [StringLength(50)]
        //[Display(Name = "Tài khoản")]
        //[Required(ErrorMessage = "Mời bạn nhập tài khoản")]
        public string RoleID { get; set; }
    }
}
