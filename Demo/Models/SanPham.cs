namespace Demo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("SanPham")]
    public partial class SanPham
    {
        internal string categoryname;
        internal string authorphoto;
        internal string authorname;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            CMTs = new HashSet<CMT>();
            CTHDs = new HashSet<CTHD>();
           
        }

        [Key]
        public int maSP { get; set; }

        public int maLoaiSP { get; set; }

        public int idAdmin { get; set; }

        public int maTP { get; set; }

        public int maSize { get; set; }

        public int maCH { get; set; }

        [StringLength(50)]
        public string tenSP { get; set; }

        public double? gia { get; set; }

        [StringLength(255)]
        public string mota { get; set; }

        [StringLength(50)]
        public string hinhanh { get; set; }

        public int? status { get; set; }

        public DateTime? ngaytao { get; set; }

        public DateTime? ngaycapnhat { get; set; }

        public virtual Admin Admin { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CMT> CMTs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTHD> CTHDs { get; set; }

        public virtual Cuahang Cuahang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        

        public virtual LoaiSP LoaiSP { get; set; }

        public virtual Size Size { get; set; }

        public virtual Topping Topping { get; set; }
        [NotMapped]
        public System.Web.HttpPostedFileBase ImageUpload { get; set; }
    }
}
