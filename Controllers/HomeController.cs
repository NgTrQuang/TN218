using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TN218.Models;

namespace TN218.Controllers
{

    public class HomeController : Controller
    {
        readonly Service _service;

        public HomeController(Service service)
        {
            this._service = service;
        }

        public IActionResult Index()
        {
            //HttpContext.Session.SetString("CurrentUserID", "1");
            var maKH = HttpContext.Session.GetString("CurrentUserID");
            ViewBag.Loai = _service.danhSachLoaiSP().ToList();
            ViewData["path"] = "/images/product/";
            if (maKH != null)
            {
                ViewData["cart_items"] = _service.ds_GioHang(0, maKH).ToList();
            }
            else
            {
                ViewData["cart_items"] = new List<GioHang>();
            }

            ViewData["hot-items"] = _service.danhSachSanPham().ToList();
            ViewData["soluong"] = _service.soLuongSanPham();
            return View(_service.danhSachSanPham().Take(16).ToList());
        }

        public IActionResult getListCartItem()
        {
            return View(_service.ds_GioHang().ToList());
        }
    }
}