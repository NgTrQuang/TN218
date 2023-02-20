using System;
using System.Collections.Generic;

namespace TN218.Models
{
    public partial class DonViTinh
    {
        public DonViTinh()
        {
            SanPhams = new HashSet<SanPham>();
        }

        public string MaDvt { get; set; } = null!;
        public string? TenDvt { get; set; }

        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
