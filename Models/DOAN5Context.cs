using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DoAn5.Models
{
    public partial class DOAN5Context : DbContext
    {
        //public DOAN5Context()
        //{
        //}

        public DOAN5Context(DbContextOptions<DOAN5Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Cthdb> Cthdb { get; set; }
        public virtual DbSet<Cthdn> Cthdn { get; set; }
        public virtual DbSet<HoaDonBan> HoaDonBan { get; set; }
        public virtual DbSet<HoaDonNhap> HoaDonNhap { get; set; }
        public virtual DbSet<KhachHang> KhachHang { get; set; }
        public virtual DbSet<LoaiSp> LoaiSp { get; set; }
        public virtual DbSet<Ncc> Ncc { get; set; }
        public virtual DbSet<SanPham> SanPham { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=DESKTOP-UKASAJA\\SQLEXPRESS;Database=DOAN5;Integrated Security=True");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cthdb>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CTHDB");

                entity.Property(e => e.MaHdb).HasColumnName("MaHDB");

                entity.Property(e => e.MaSp).HasColumnName("MaSP");
            });

            modelBuilder.Entity<Cthdn>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CTHDN");

                entity.Property(e => e.MaHdn).HasColumnName("MaHDN");

                entity.Property(e => e.MaSp).HasColumnName("MaSP");
            });

            modelBuilder.Entity<HoaDonBan>(entity =>
            {
                entity.HasKey(e => e.MaHdb);

                entity.Property(e => e.MaHdb)
                    .HasColumnName("MaHDB")
                    .ValueGeneratedNever();

                entity.Property(e => e.MaKh).HasColumnName("MaKH");

                entity.Property(e => e.NgayBan).HasColumnType("datetime");
            });

            modelBuilder.Entity<HoaDonNhap>(entity =>
            {
                entity.HasKey(e => e.MaHdn);

                entity.Property(e => e.MaHdn).HasColumnName("MaHDN");

                entity.Property(e => e.MaNcc).HasColumnName("MaNCC");

                entity.Property(e => e.NgayNhap).HasColumnType("datetime");
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.HasKey(e => e.MaKh);

                entity.Property(e => e.MaKh)
                    .HasColumnName("MaKH")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.DiaChi).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Password).HasMaxLength(200);

                entity.Property(e => e.Sdt).HasColumnName("SDT");

                entity.Property(e => e.TaiKhoan).HasMaxLength(255);

                entity.Property(e => e.TenKh)
                    .HasColumnName("TenKH")
                    .HasMaxLength(100);

                entity.HasOne(d => d.MaKhNavigation)
                    .WithOne(p => p.KhachHang)
                    .HasForeignKey<KhachHang>(d => d.MaKh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KhachHang_HoaDonBan");
            });

            modelBuilder.Entity<LoaiSp>(entity =>
            {
                entity.HasKey(e => e.MaLoaiSp);

                entity.ToTable("LoaiSP");

                entity.Property(e => e.MaLoaiSp).HasColumnName("MaLoaiSP");

                entity.Property(e => e.TenLoaiSp)
                    .HasColumnName("TenLoaiSP")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Ncc>(entity =>
            {
                entity.HasKey(e => e.MaNcc);

                entity.ToTable("NCC");

                entity.Property(e => e.MaNcc)
                    .HasColumnName("MaNCC")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.DiaChi).HasMaxLength(255);

                entity.Property(e => e.Sdt)
                    .HasColumnName("SDT")
                    .HasMaxLength(255);

                entity.Property(e => e.TenNcc)
                    .HasColumnName("TenNCC")
                    .HasMaxLength(100);

                entity.HasOne(d => d.MaNccNavigation)
                    .WithOne(p => p.Ncc)
                    .HasForeignKey<Ncc>(d => d.MaNcc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NCC_HoaDonNhap");
            });

            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.HasKey(e => e.MaSp);

                entity.Property(e => e.MaSp).HasColumnName("MaSP");

                entity.Property(e => e.Anh).HasMaxLength(200);

                entity.Property(e => e.TenSp)
                    .HasColumnName("TenSP")
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
