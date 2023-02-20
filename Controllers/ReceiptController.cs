using Microsoft.AspNetCore.Mvc;
using TN218.Models;

namespace CH_VatTu.Areas.Shop.Controllers
{
    public class ReceiptController : Controller
    {
        readonly Service _service;
        public ReceiptController(Service service)
        {
            this._service = service;
        }
        [HttpGet]
        public IActionResult List()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("CurrentUserID")))
            {
                return RedirectToAction("Index", "HomePage");
            }

			var model = _service.ds_HD(HttpContext.Session.GetString("CurrentUserID")).ToList();

            var makh = HttpContext.Session.GetString("CurrentUserID");
            ViewBag.Loai = _service.danhSachLoaiSP().ToList();
            ViewData["path"] = "/images/product/";
            if (makh != null)
            {
                ViewData["cart_items"] = _service.ds_GioHang(0, makh).ToList();
            }
            else
            {
                ViewData["cart_items"] = new List<GioHang>();
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Detail(string mahd)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("CurrentUserID")))
            {
                return RedirectToAction("Index", "HomePage");
            }
            var model = _service.get_hoadon(mahd);
            var makh = HttpContext.Session.GetString("CurrentUserID");
			ViewBag.Loai = _service.danhSachLoaiSP().ToList();
			ViewData["path"] = "/images/product/";
			if (makh != null)
			{
				ViewData["cart_items"] = _service.ds_GioHang(0, makh).ToList();
				ViewData["sum"] = _service.tongGT(mahd);
			}
			else
			{
				ViewData["cart_items"] = new List<GioHang>();
			}
			return View(model);
        }
    }
}
