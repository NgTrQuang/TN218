using System.ComponentModel.DataAnnotations;

namespace TN218.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
        public string SoDienThoai { get; set; }
        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        public string Password { get; set; }
        public bool Rememberme { get; set; }

        public LoginModel() { }
    }
}
