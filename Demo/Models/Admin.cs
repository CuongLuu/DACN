namespace Demo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Admin")]
    public partial class Admin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Admin()
        {
            Cuahangs = new HashSet<Cuahang>();
            SanPhams = new HashSet<SanPham>();
        }

        [Key]
        public int idAdmin { get; set; }

        [StringLength(50)]
        public string ten { get; set; }

        [StringLength(50)]
        public string anh { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }
        public byte? gioitinh { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ngaysinh { get; set; }

        [StringLength(11)]
        public string sdt { get; set; }

        [StringLength(200)]
        public string diachi { get; set; }

        [StringLength(255)]
        public string matkhau { get; set; }

        [StringLength(40)]
        public string email { get; set; }

        [StringLength(12)]
        public string cmnd { get; set; }

        public byte? status { get; set; }

        public byte? type { get; set; }

        public DateTime? ngaytao { get; set; }

        public DateTime? ngaysua { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cuahang> Cuahangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
