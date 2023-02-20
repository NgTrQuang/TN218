using System;
using System.Collections.Generic;

namespace TN218.Models
{
    public partial class VanChuyen
    {
        public VanChuyen()
        {
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
        }

        public string MaVanDon { get; set; } = null!;
        public string? MaDvvc { get; set; }
        public int? TrangThai { get; set; }

        public virtual DonViVanChuyen? MaDvvcNavigation { get; set; }
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
