using System;
using System.Collections.Generic;

namespace TN218.Models
{
    public partial class DonViVanChuyen
    {
        public DonViVanChuyen()
        {
            VanChuyens = new HashSet<VanChuyen>();
        }

        public string MaDvvc { get; set; } = null!;
        public string? TenDonViVc { get; set; }

        public virtual ICollection<VanChuyen> VanChuyens { get; set; }
    }
}
