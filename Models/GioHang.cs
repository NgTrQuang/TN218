using System;
using System.Collections.Generic;

namespace TN218.Models
{
    public partial class GioHang
    {
        public GioHang()
        {
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
        }

        public string MaGioHang { get; set; } = null!;
        public int SoLuongDat { get; set; }
        public int TrangThai { get; set; }
        public string? MaKhachHang { get; set; }
        public string? MaSanPham { get; set; }

        public virtual KhachHang? MaKhachHangNavigation { get; set; }
        public virtual SanPham? MaSanPhamNavigation { get; set; }
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
