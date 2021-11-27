namespace Demo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Gioithieu")]
    public partial class Gioithieu
    {
        [Key]
        public int idGT { get; set; }

        [StringLength(300)]
        public string noidung { get; set; }

        public DateTime? ngaytao { get; set; }

        public DateTime? ngaycapnhat { get; set; }
    }
}
