using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace TN218.Models
{
    public class Service
    {
        private TN218Context _context = new TN218Context();

        // Loại sản phẩm
        // Danh sách loại sản phẩm
        public IQueryable<LoaiSanPham> danhSachLoaiSP()
        {
            return _context.LoaiSanPhams.Where(x => x.MaLoaiSp != null);
        }

        // Lấy loại sản phẩm
        public LoaiSanPham? getLoai(string maLoai)
        {
            return danhSachLoaiSP().Where(x => x.MaLoaiSp == maLoai).FirstOrDefault();
        }


        // Sản phẩm
        // Danh sách sản phẩm
        public IQueryable<SanPham> danhSachSanPham()
        {
            return _context.SanPhams.Where(x => x.MaSanPham != null);
        }

        // Danh sách sản phẩm theo loại
        public IQueryable<SanPham> danhSachSanPham(string maLoai = null)
        {
            return _context.SanPhams.Where(x => x.MaSanPham != null).Where(x => x.MaLoaiSp == maLoai);
        }

        // Lấy sản phẩm
        public SanPham? getSanPham(string maSanPham)
        {
            return danhSachSanPham().Where(x => x.MaSanPham== maSanPham).FirstOrDefault();
        }

        // Lấy sản phẩm
        public SanPham? GetSanPham(string maSanPham)
        {
            return _context.SanPhams.Where( x => x.MaSanPham == maSanPham).Include(x => x.MaDvtNavigation).FirstOrDefault();
        }

        public int soLuongSanPham()
        {
            return danhSachSanPham().Count();
        }

        // Tìm kiếm sản phẩm
        public IQueryable<SanPham> timKiem(string key)
        {
            return danhSachSanPham().Where(x => x.TenSanPham.Contains(key));
        }

        

        // Mã hóa mật khẩu MD5
        public static string getMD5(string password)
        {
            MD5 mD5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(password);
            byte[] targetData = mD5.ComputeHash(fromData);

            string byte2String = null;
            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;
        }


        //Giohang
        public IQueryable<GioHang> ds_GioHang(int tt = 2, string makh = null)
        {
            if (tt == 3)
            {
                return _context.GioHangs.Where(x => x.MaGioHang != null);
            }
            var rs = _context.GioHangs.Where(x => x.MaKhachHang == makh);
            if (tt == 2)
            {
                return _context.GioHangs.Where(x => x.MaKhachHang != null).Include(x => x.MaSanPhamNavigation).ThenInclude(x=> x.MaDvtNavigation);
            }
            return rs.Where(x => x.TrangThai == tt).Include(x => x.MaSanPhamNavigation).ThenInclude(x => x.MaDvtNavigation);
        }

        public GioHang get_GH(string magh)
        {
            if (!string.IsNullOrEmpty(magh))
            {
                return _context.GioHangs.Where(x => x.MaGioHang == magh).Include(x => x.MaSanPhamNavigation).FirstOrDefault();
            }
            return null;
        }
        public string them_GH(string masp, string makh = null)
        {
            string magh = "GH00" + (ds_GioHang(3).Count() + 1);
            var rs = ds_GioHang(0, makh).Where(x => x.MaSanPham == masp);

            if (rs.Any())
            {
                var model = rs.FirstOrDefault();
                _context.Update(model);
                model.SoLuongDat = model.SoLuongDat + 1;
                _context.SaveChanges();
                return model.MaGioHang;
            }
            GioHang gh = new GioHang();
            gh.MaGioHang = magh;
            gh.MaKhachHang = makh;
            if (makh == null)
            {
                gh.TrangThai = 2;
            }
            else
            {
                gh.TrangThai = 0;
            }
            gh.SoLuongDat = 1;
            gh.MaSanPham = masp;
            _context.GioHangs.Add(gh);
            _context.SaveChanges();
            return magh;
        }

        public void increase(string magh, int sluong)
        {
            var gh = get_GH(magh);
            _context.Update(gh);
            gh.SoLuongDat = sluong;
            _context.SaveChanges();
        }

        public void xoa_GH(string magh)
        {
            if (!string.IsNullOrEmpty(magh))
            {
                var model = _context.GioHangs.Find(magh);
                _context.Update(model);
                if (model.TrangThai == 0 || model.TrangThai == 2)
                {
                    model.TrangThai = -1;
                }
                _context.SaveChanges();
            }
        }

        //Khách hàng
        public IQueryable<KhachHang> ds_KH()
        {
            return _context.KhachHangs.Where(x => x.MaKhachHang != null);
        }
        public KhachHang get_KH(string makh)
        {
            return _context.KhachHangs.Where(x => x.MaKhachHang == makh).FirstOrDefault();
        }

		public KhachHang login_KH(string sdt, string pwd)
		{
			return ds_KH().Where(x => x.SoDienThoai == sdt).Where(x => x.MatKhau == getMD5(pwd)).FirstOrDefault();
		}

		//public IQueryable<KhachHang> list_khachhang()
		//{
		//    return db.KhachHangs.Where(x => x.MaKhachHang != null);
		//}
		//public KhachHang get_khachhang(string makh)
		//{
		//    return list_khachhang().Where(x => x.MaKhachHang == makh).FirstOrDefault();
		//}

		//public KhachHang login_KH(string tendangnhap, string pwd)
		//{
		//    return list_khachhang().Where(x => x.Username == tendangnhap).Where(x => x.MatKhau == pwd).FirstOrDefault();
		//}
		//Hóa đơn
		public IQueryable<HoaDon> ds_HD(string makh = null)
        {
            if (makh == null)
            {
                return _context.HoaDons.Where(x => x.MaHoaDon != null)
                    .Include(x => x.MaHtttNavigation)
                    .Include(x => x.ChiTietHoaDons).ThenInclude(y => y.MaGioHangNavigation)
					.ThenInclude(x => x.MaSanPhamNavigation).ThenInclude(x => x.MaDvtNavigation)
					.OrderByDescending(x => x.MaHoaDon);
			}
            return _context.HoaDons.Where(x => x.MaKhachHang == makh)
				.Include(x => x.MaHtttNavigation)
				.Include(x => x.ChiTietHoaDons).ThenInclude(y => y.MaGioHangNavigation)
				.ThenInclude(x => x.MaSanPhamNavigation).ThenInclude(x => x.MaDvtNavigation)
                .OrderByDescending(x => x.MaHoaDon);
		}
        public HoaDon get_hoadon(string mahd)
        {
            return ds_HD().Where(x => x.MaHoaDon == mahd).FirstOrDefault();
        }
        public string them_HD(string makh = "", string thanhtoan = "")
        {
            HoaDon hoadon = new HoaDon();
            hoadon.MaHoaDon = "HD00" + (ds_HD().Count() + 1);
            if (makh == "")
            {
                hoadon.MaKhachHang = null;
            }
            else
            {
                hoadon.MaKhachHang = makh;
            }
            switch (thanhtoan)
            {
                case "zalopay":
                    {
                        hoadon.MaHttt = "2";
                        break;
                    }
                case "bank":
                    {
                        hoadon.MaHttt = "5";
                        break;
                    }
                case "cod":
                    {
                        hoadon.MaHttt = "1";
                        break;
                    }
                default: hoadon.MaHttt = "1"; break;
			}
            DateTime dateValue = DateTime.UtcNow;
            hoadon.NgayXuatHd = dateValue.ToUniversalTime();

			_context.HoaDons.Add(hoadon);
            _context.SaveChanges();
            return hoadon.MaHoaDon;
        }
        //Chi tiet hoa don
        public IQueryable<ChiTietHoaDon> ds_CTHD(string mahd = null)
        {
            if (mahd == null)
            {
                return _context.ChiTietHoaDons.Where(x => x.MaCthd != null).Include(x => x.MaGioHangNavigation).ThenInclude(x => x.MaSanPhamNavigation);
            }
            return _context.ChiTietHoaDons.Where(x => x.MaHoaDon == mahd).Include(x => x.MaGioHangNavigation).ThenInclude(x => x.MaSanPhamNavigation);
        }
        public string them_CTHD(string mahd, string magh)
        {
            ChiTietHoaDon ct = new ChiTietHoaDon();
            ct.MaCthd = "CT00" + (ds_CTHD().Count() + 1);
            ct.MaGioHang = magh;
            get_GH(magh).TrangThai = 1;
            ct.MaHoaDon = mahd;
            _context.ChiTietHoaDons.Add(ct);
            _context.SaveChanges();
            return ct.MaCthd;
        }

        //Email
        public string itemComponent(string tensp, int soluong, long? dongia, long thanhtien)
        {
            return "<tr>\r\n                                    <td width=\"75%\" align=\"left\" style=\"font-family: system-ui; font-size: 16px; font-weight: 400; line-height: 24px; padding: 15px 10px 5px 10px;\">\r\n                            " +tensp +"   \r\n                                    </td>\r\n                                    <td width=\"25%\" align=\"left\" style=\"font-family: system-ui; font-size: 16px; font-weight: 400; line-height: 24px; padding: 15px 10px 5px 10px;\">\r\n                   "+ soluong+"                     \r\n                                    </td>\r\n                                    <td width=\"75%\" align=\"left\" style=\"font-family: system-ui; font-size: 16px; font-weight: 400; line-height: 24px; padding: 15px 10px 5px 10px;\">\r\n                       "+dongia+"                 \r\n                                    </td>\r\n                                    <td width=\"75%\" align=\"left\" style=\"font-family: system-ui; font-size: 16px; font-weight: 400; line-height: 24px; padding: 15px 10px 5px 10px;\">\r\n                   "+thanhtien+"                     \r\n                                    </td>\r\n                                </tr>";

        }

		public KhachHang getLastestKh()
		{
			return get_KH(ds_KH().Count().ToString());
		}

		public void themKH(RegisterModel model)
		{
			string maKH = "" + (ds_KH().Count() + 1);
			KhachHang kh = new KhachHang();
			kh.MaKhachHang = maKH;
			kh.HoKhachHang = model.HoKhachHang;
			kh.TenKhachHang = model.TenKhachHang;
			kh.NgaySinh = model.NgaySinh;
			kh.GioiTinh = model.GioiTinh;
			kh.SoDienThoai = model.SoDienThoai;
			kh.Email = model.Email;
			kh.DiaChi = model.DiaChi;
			kh.MatKhau = getMD5(model.MatKhau);
			_context.KhachHangs.Add(kh);
			_context.SaveChanges();
		}

		public long? tongGT(string mahd)
		{
			long? sum = 0;
			var dds = ds_CTHD(mahd).ToList();
			foreach (var dd in dds)
			{
				sum += dd.MaGioHangNavigation.SoLuongDat * dd.MaGioHangNavigation.MaSanPhamNavigation.Gia;
			}
			return sum;
		}
	}
}
