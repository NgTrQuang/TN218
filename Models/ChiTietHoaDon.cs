using System;
using System.Collections.Generic;

namespace TN218.Models
{
    public partial class ChiTietHoaDon
    {
        public string MaCthd { get; set; } = null!;
        public string? MaHoaDon { get; set; }
        public string? MaGioHang { get; set; }
        public string? MaVanDon { get; set; }
        public string? MaKhuyenMai { get; set; }

        public virtual GioHang? MaGioHangNavigation { get; set; }
        public virtual HoaDon? MaHoaDonNavigation { get; set; }
        public virtual KhuyenMai? MaKhuyenMaiNavigation { get; set; }
        public virtual VanChuyen? MaVanDonNavigation { get; set; }
    }
}
