using System;
using System.Collections.Generic;

namespace TN218.Models
{
    public partial class HinhThucThanhToan
    {
        public HinhThucThanhToan()
        {
            HoaDons = new HashSet<HoaDon>();
        }

        public string MaHttt { get; set; } = null!;
        public string? TenHttt { get; set; }

        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}
