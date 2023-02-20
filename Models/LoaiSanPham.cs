using System;
using System.Collections.Generic;

namespace TN218.Models
{
    public partial class LoaiSanPham
    {
        public LoaiSanPham()
        {
            SanPhams = new HashSet<SanPham>();
        }

        public string MaLoaiSp { get; set; } = null!;
        public string? TenLoaiSp { get; set; }

        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
