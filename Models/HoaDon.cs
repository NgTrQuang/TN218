using System;
using System.Collections.Generic;

namespace TN218.Models
{
    public partial class HoaDon
    {
        public HoaDon()
        {
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
        }

        public string MaHoaDon { get; set; } = null!;
        public DateTime NgayXuatHd { get; set; }
        public string? MaKhachHang { get; set; }
        public string? MaHttt { get; set; }

        public virtual HinhThucThanhToan? MaHtttNavigation { get; set; }
        public virtual KhachHang? MaKhachHangNavigation { get; set; }
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
