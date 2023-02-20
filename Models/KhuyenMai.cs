using System;
using System.Collections.Generic;

namespace TN218.Models
{
    public partial class KhuyenMai
    {
        public KhuyenMai()
        {
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
        }

        public string MaKhuyenMai { get; set; } = null!;
        public string? TenKhuyenMai { get; set; }
        public double? GiaTriKm { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }

        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
