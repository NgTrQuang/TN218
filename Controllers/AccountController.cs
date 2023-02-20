using Microsoft.AspNetCore.Mvc;
using TN218.Models;

namespace TN408.Areas.Store.Controllers
{
    public class AccountController : Controller
    {
        readonly Service _service;
        public AccountController(Service service)
        {
            this._service = service;
        }
        [HttpGet]
        public IActionResult Info(String makh)
        {
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
			ViewData["hot_items"] = _service.danhSachSanPham().ToList();
			return View(_service.get_KH(makh));
        }
    }
}
