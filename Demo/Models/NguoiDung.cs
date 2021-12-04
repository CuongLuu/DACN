namespace Demo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("NguoiDung")]
    public partial class NguoiDung
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NguoiDung()
        {
            CTHDs = new HashSet<CTHD>();
            Donhangs = new HashSet<Donhang>();
        }

        [Key]
        public int maND { get; set; }

        [StringLength(50)]
        public string tenND { get; set; }

        public byte? gioitinh { get; set; }

        public DateTime? ngaysinh { get; set; }

        [StringLength(50)]
        public string anh { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }

        [StringLength(11)]
        public string sdt { get; set; }

        [StringLength(40)]
        public string email { get; set; }

        [StringLength(200)]
        public string diachi { get; set; }

        public byte? status { get; set; }

        public DateTime? ngaytao { get; set; }

        public DateTime? ngaysua { get; set; }

        [StringLength(255)]
        public string matkhau { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTHD> CTHDs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Donhang> Donhangs { get; set; }
    }
}
