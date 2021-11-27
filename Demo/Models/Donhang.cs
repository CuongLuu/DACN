namespace Demo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Donhang")]
    public partial class Donhang
    {
        [Key]
        public int maDH { get; set; }

        public int maHD { get; set; }

        public int maCH { get; set; }

        public int maND { get; set; }

        [StringLength(20)]
        public string tiendo { get; set; }

        public virtual Cuahang Cuahang { get; set; }

        public virtual HoaDon HoaDon { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }
    }
}
