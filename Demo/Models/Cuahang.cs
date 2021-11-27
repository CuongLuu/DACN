namespace Demo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cuahang")]
    public partial class Cuahang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cuahang()
        {
            CTHDs = new HashSet<CTHD>();
            Danhgias = new HashSet<Danhgia>();
            Donhangs = new HashSet<Donhang>();
            SanPhams = new HashSet<SanPham>();
        }

        [Key]
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

        public int idLike { get; set; }

        public int soluongSP { get; set; }

        public virtual Admin Admin { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTHD> CTHDs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Danhgia> Danhgias { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Donhang> Donhangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
