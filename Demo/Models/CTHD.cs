namespace Demo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CTHD")]
    public partial class CTHD
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int maHD { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int maCH { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int maND { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int maSP { get; set; }

        public int? soluong { get; set; }

        public double? giaban { get; set; }

        public double? thanhtien { get; set; }

        public virtual Cuahang Cuahang { get; set; }

        public virtual HoaDon HoaDon { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
