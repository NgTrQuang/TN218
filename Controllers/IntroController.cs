using Microsoft.AspNetCore.Mvc;
using TN218.Models;

namespace TN218.Controllers
{
    public class IntroController : Controller
    {
        readonly Service _service;

        public IntroController(Service service)
        {
            this._service = service;
        }
        public IActionResult Index()
        {
            //var maKH = HttpContext.Session.GetString("CurrentUserID");
            ViewBag.Loai = _service.danhSachLoaiSP().ToList();
            ViewData["path"] = "/images/product/";
            //if (maKH != null)
            //{
            //    ViewData["cart_items"] = _service.danhSachGioHang(0, maKH).ToList();
            //}
            //else
            //{
            //    ViewData["cart_items"] = new List<GioHang>();
            //}

            ViewData["hot-items"] = _service.danhSachSanPham().ToList();
            ViewData["soluong"] = _service.soLuongSanPham();
            return View();
        }
    }
}
