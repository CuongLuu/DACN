using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Demo.Models
{
    public partial class DBcontext : DbContext
    {
        public DBcontext()
            : base("name=DBcontext")
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<CMT> CMTs { get; set; }
        public virtual DbSet<CTHD> CTHDs { get; set; }
        public virtual DbSet<Cuahang> Cuahangs { get; set; }
        public virtual DbSet<Danhgia> Danhgias { get; set; }
        public virtual DbSet<Donhang> Donhangs { get; set; }
        public virtual DbSet<Gioithieu> Gioithieux { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<LoaiSP> LoaiSPs { get; set; }
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<Silde> Sildes { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<SubCMT> SubCMTs { get; set; }
        public virtual DbSet<Topping> Toppings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>()
                .Property(e => e.anh)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.sdt)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.matkhau)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.cmnd)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .HasMany(e => e.Cuahangs)
                .WithRequired(e => e.Admin)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Admin>()
                .HasMany(e => e.SanPhams)
                .WithRequired(e => e.Admin)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cuahang>()
                .Property(e => e.sdt)
                .IsUnicode(false);

            modelBuilder.Entity<Cuahang>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Cuahang>()
                .HasMany(e => e.SanPhams)
                .WithRequired(e => e.Cuahang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.anh)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.sdt)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.matkhau)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .HasMany(e => e.CMTs)
                .WithRequired(e => e.NguoiDung)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NguoiDung>()
                .HasMany(e => e.CTHDs)
                .WithRequired(e => e.NguoiDung)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NguoiDung>()
                .HasMany(e => e.Danhgias)
                .WithRequired(e => e.NguoiDung)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NguoiDung>()
                .HasMany(e => e.Donhangs)
                .WithRequired(e => e.NguoiDung)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.hinhanh)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.CMTs)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Silde>()
                .Property(e => e.hinhanh)
                .IsUnicode(false);
        }
    }
}
