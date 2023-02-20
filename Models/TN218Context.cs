using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TN218.Models
{
    public partial class TN218Context : DbContext
    {
        public TN218Context()
        {
        }

        public TN218Context(DbContextOptions<TN218Context> options)
            : base(options)
        {
        }

        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; } = null!;
        public virtual DbSet<DonViTinh> DonViTinhs { get; set; } = null!;
        public virtual DbSet<DonViVanChuyen> DonViVanChuyens { get; set; } = null!;
        public virtual DbSet<GioHang> GioHangs { get; set; } = null!;
        public virtual DbSet<HinhThucThanhToan> HinhThucThanhToans { get; set; } = null!;
        public virtual DbSet<HoaDon> HoaDons { get; set; } = null!;
        public virtual DbSet<KhachHang> KhachHangs { get; set; } = null!;
        public virtual DbSet<KhuyenMai> KhuyenMais { get; set; } = null!;
        public virtual DbSet<LoaiSanPham> LoaiSanPhams { get; set; } = null!;
        public virtual DbSet<SanPham> SanPhams { get; set; } = null!;
        public virtual DbSet<VanChuyen> VanChuyens { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:lmanh0408.database.windows.net,1433;Initial Catalog=TN218;User ID=lmanh4048;Password=LMAlmaht2st;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietHoaDon>(entity =>
            {
                entity.HasKey(e => e.MaCthd)
                    .HasName("PK__ChiTiet___1E4FA77118543E4C");

                entity.ToTable("ChiTiet_HoaDon");

                entity.Property(e => e.MaCthd)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaCTHD");

                entity.Property(e => e.MaGioHang)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MaHoaDon)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MaKhuyenMai)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MaVanDon)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.MaGioHangNavigation)
                    .WithMany(p => p.ChiTietHoaDons)
                    .HasForeignKey(d => d.MaGioHang)
                    .HasConstraintName("FK__ChiTiet_H__MaGio__3F466844");

                entity.HasOne(d => d.MaHoaDonNavigation)
                    .WithMany(p => p.ChiTietHoaDons)
                    .HasForeignKey(d => d.MaHoaDon)
                    .HasConstraintName("FK__ChiTiet_H__MaHoa__3E52440B");

                entity.HasOne(d => d.MaKhuyenMaiNavigation)
                    .WithMany(p => p.ChiTietHoaDons)
                    .HasForeignKey(d => d.MaKhuyenMai)
                    .HasConstraintName("FK__ChiTiet_H__MaKhu__412EB0B6");

                entity.HasOne(d => d.MaVanDonNavigation)
                    .WithMany(p => p.ChiTietHoaDons)
                    .HasForeignKey(d => d.MaVanDon)
                    .HasConstraintName("FK__ChiTiet_H__MaVan__403A8C7D");
            });

            modelBuilder.Entity<DonViTinh>(entity =>
            {
                entity.HasKey(e => e.MaDvt)
                    .HasName("PK__DonViTin__3D895AFE4B9CA786");

                entity.ToTable("DonViTinh");

                entity.Property(e => e.MaDvt)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaDVT");

                entity.Property(e => e.TenDvt)
                    .HasMaxLength(40)
                    .HasColumnName("TenDVT");
            });

            modelBuilder.Entity<DonViVanChuyen>(entity =>
            {
                entity.HasKey(e => e.MaDvvc)
                    .HasName("PK__DonViVan__36ECC45E3E52270D");

                entity.ToTable("DonViVanChuyen");

                entity.Property(e => e.MaDvvc)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaDVVC");

                entity.Property(e => e.TenDonViVc)
                    .HasMaxLength(40)
                    .HasColumnName("TenDonViVC");
            });

            modelBuilder.Entity<GioHang>(entity =>
            {
                entity.HasKey(e => e.MaGioHang)
                    .HasName("PK__GioHang__F5001DA32689BCB2");

                entity.ToTable("GioHang");

                entity.Property(e => e.MaGioHang)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MaKhachHang)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MaSanPham)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.MaKhachHangNavigation)
                    .WithMany(p => p.GioHangs)
                    .HasForeignKey(d => d.MaKhachHang)
                    .HasConstraintName("FK__GioHang__MaKhach__3A81B327");

                entity.HasOne(d => d.MaSanPhamNavigation)
                    .WithMany(p => p.GioHangs)
                    .HasForeignKey(d => d.MaSanPham)
                    .HasConstraintName("FK__GioHang__MaSanPh__3B75D760");
            });

            modelBuilder.Entity<HinhThucThanhToan>(entity =>
            {
                entity.HasKey(e => e.MaHttt)
                    .HasName("PK__HinhThuc__1038E8CA02940E67");

                entity.ToTable("HinhThucThanhToan");

                entity.Property(e => e.MaHttt)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaHTTT");

                entity.Property(e => e.TenHttt)
                    .HasMaxLength(40)
                    .HasColumnName("TenHTTT");
            });

            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity.HasKey(e => e.MaHoaDon)
                    .HasName("PK__HoaDon__835ED13BEB70F7A2");

                entity.ToTable("HoaDon");

                entity.Property(e => e.MaHoaDon)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MaHttt)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaHTTT");

                entity.Property(e => e.MaKhachHang)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.NgayXuatHd)
                    .HasColumnType("date")
                    .HasColumnName("NgayXuatHD");

                entity.HasOne(d => d.MaHtttNavigation)
                    .WithMany(p => p.HoaDons)
                    .HasForeignKey(d => d.MaHttt)
                    .HasConstraintName("FK__HoaDon__MaHTTT__29572725");

                entity.HasOne(d => d.MaKhachHangNavigation)
                    .WithMany(p => p.HoaDons)
                    .HasForeignKey(d => d.MaKhachHang)
                    .HasConstraintName("FK__HoaDon__MaKhachH__286302EC");
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.HasKey(e => e.MaKhachHang)
                    .HasName("PK__KhachHan__88D2F0E5201CC403");

                entity.ToTable("KhachHang");

                entity.Property(e => e.MaKhachHang)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DiaChi).HasMaxLength(200);

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.GioiTinh).HasMaxLength(3);

                entity.Property(e => e.HoKhachHang).HasMaxLength(12);

                entity.Property(e => e.MatKhau)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.SoDienThoai)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TenKhachHang).HasMaxLength(32);
            });

            modelBuilder.Entity<KhuyenMai>(entity =>
            {
                entity.HasKey(e => e.MaKhuyenMai)
                    .HasName("PK__KhuyenMa__6F56B3BDF39ECC9C");

                entity.ToTable("KhuyenMai");

                entity.Property(e => e.MaKhuyenMai)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.GiaTriKm).HasColumnName("GiaTriKM");

                entity.Property(e => e.NgayBatDau).HasColumnType("date");

                entity.Property(e => e.NgayKetThuc).HasColumnType("date");

                entity.Property(e => e.TenKhuyenMai).HasMaxLength(100);
            });

            modelBuilder.Entity<LoaiSanPham>(entity =>
            {
                entity.HasKey(e => e.MaLoaiSp)
                    .HasName("PK__LoaiSanP__1224CA7CA70FDE6D");

                entity.ToTable("LoaiSanPham");

                entity.Property(e => e.MaLoaiSp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaLoaiSP");

                entity.Property(e => e.TenLoaiSp)
                    .HasMaxLength(40)
                    .HasColumnName("TenLoaiSP");
            });

            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.HasKey(e => e.MaSanPham)
                    .HasName("PK__SanPham__FAC7442D089ED7AA");

                entity.ToTable("SanPham");

                entity.Property(e => e.MaSanPham)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HinhAnh).HasMaxLength(100);

                entity.Property(e => e.MaDvt)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaDVT");

                entity.Property(e => e.MaLoaiSp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaLoaiSP");

                entity.Property(e => e.TenSanPham).HasMaxLength(200);

                entity.HasOne(d => d.MaDvtNavigation)
                    .WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.MaDvt)
                    .HasConstraintName("FK__SanPham__MaDVT__34C8D9D1");

                entity.HasOne(d => d.MaLoaiSpNavigation)
                    .WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.MaLoaiSp)
                    .HasConstraintName("FK__SanPham__MaLoaiS__35BCFE0A");
            });

            modelBuilder.Entity<VanChuyen>(entity =>
            {
                entity.HasKey(e => e.MaVanDon)
                    .HasName("PK__VanChuye__910BDE78CD1D8375");

                entity.ToTable("VanChuyen");

                entity.Property(e => e.MaVanDon)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MaDvvc)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaDVVC");

                entity.HasOne(d => d.MaDvvcNavigation)
                    .WithMany(p => p.VanChuyens)
                    .HasForeignKey(d => d.MaDvvc)
                    .HasConstraintName("FK__VanChuyen__MaDVV__300424B4");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
