﻿using System.ComponentModel.DataAnnotations;

namespace TN218.Models
{
    public class RegisterModel
    {
        public string? MaKhachHang { get; set; }
        [Required(ErrorMessage = "Họ lót không được bỏ trống"), MaxLength(32)]
        public string HoKhachHang { get; set; }
        [Required(ErrorMessage = "Tên không được bỏ trống")]
        public string TenKhachHang { get; set; }
        [Required(ErrorMessage = "Ngày sinh không được để trống")]
        public DateTime NgaySinh { get; set; }
        [Required(ErrorMessage = "Giới tính không được bỏ trống")]
        public string GioiTinh { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [MaxLength(10)]
        public string? SoDienThoai { get; set; }
        [Required(ErrorMessage = "Địa chỉ email không được bỏ trống")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được bỏ trống")]
        public string DiaChi { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string MatKhau { get; set; }
        public string MatKhau2 { get; set; }

        public RegisterModel() { }
    }
}
