namespace Demo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Silde")]
    public partial class Silde
    {
        [Key]
        public int maSilde { get; set; }

        [StringLength(50)]
        public string hinhanh { get; set; }

        [StringLength(30)]
        public string name { get; set; }

        [StringLength(255)]
        public string mota { get; set; }

        public DateTime? ngaytao { get; set; }

        public DateTime? ngaycapnhat { get; set; }
    }
}
