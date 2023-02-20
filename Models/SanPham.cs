using System;
using System.Collections.Generic;

namespace TN218.Models
{
    public partial class SanPham
    {
        public SanPham()
        {
            GioHangs = new HashSet<GioHang>();
        }

        public string MaSanPham { get; set; } = null!;
        public string? TenSanPham { get; set; }
        public long? Gia { get; set; }
        public string? HinhAnh { get; set; }
        public string? MaDvt { get; set; }
        public string? MaLoaiSp { get; set; }

        public virtual DonViTinh? MaDvtNavigation { get; set; }
        public virtual LoaiSanPham? MaLoaiSpNavigation { get; set; }
        public virtual ICollection<GioHang> GioHangs { get; set; }
    }
}
