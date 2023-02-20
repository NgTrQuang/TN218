using System.ComponentModel.DataAnnotations;

namespace TN218.Models
{
    public class HoaDonModel
    {
        [Required(ErrorMessage = "Họ lót không được trống!")]
        public string Holot { get; set; }
        [Required(ErrorMessage = "Tên không được trống!")]
        public string Ten { get; set; }
        [Required(ErrorMessage = "Số điện thoại là bắt buộc!")]
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
		public string Thanhtoan { get; set; }
		public string GioHangs { get; set; }
        public string MaHoaDon { get; set; }

        public HoaDonModel()
        {
            this.GioHangs = "";
            this.Email = "";
            this.MaHoaDon = "";
        }
    }
}
