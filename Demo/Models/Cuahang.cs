namespace Demo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Cuahang")]
    public partial class Cuahang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cuahang()
        {
            CTHDs = new HashSet<CTHD>();
            Donhangs = new HashSet<Donhang>();
            SanPhams = new HashSet<SanPham>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int maCH { get; set; }

        [StringLength(50)]
        public string tenCH { get; set; }

        [StringLength(100)]
        public string diachi { get; set; }

        [StringLength(11)]
        public string sdt { get; set; }

        [StringLength(40)]
        public string email { get; set; }

        public int idAdmin { get; set; }

        [StringLength(50)]
        public string anh { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }

        public virtual Admin Admin { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTHD> CTHDs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Donhang> Donhangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
