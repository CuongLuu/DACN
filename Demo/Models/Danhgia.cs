namespace Demo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Danhgia")]
    public partial class Danhgia
    {
        [Key]
        public int idLike { get; set; }

        public int maCH { get; set; }

        public int maND { get; set; }

        public int maSP { get; set; }

        [Column("danhgia")]
        [StringLength(30)]
        public string danhgia1 { get; set; }

        public virtual Cuahang Cuahang { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
