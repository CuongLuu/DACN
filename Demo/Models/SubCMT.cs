namespace Demo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SubCMT")]
    public partial class SubCMT
    {
        [Key]
        public int maSupcmt { get; set; }

        public int? maND { get; set; }

        public int? maCMT { get; set; }

        [StringLength(250)]
        public string content { get; set; }

        public DateTime? ngaytao { get; set; }

        public DateTime? ngaysua { get; set; }

        public virtual CMT CMT { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }
    }
}
