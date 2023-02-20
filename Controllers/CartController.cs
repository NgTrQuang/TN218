using Microsoft.AspNetCore.Mvc;
using TN218.Models;

namespace TN218.Controllers
{
    public class CartController : Controller
    {
        readonly Service _service = new Service();

        [HttpGet]
        public IActionResult MyItems()
        {
            var makh = HttpContext.Session.GetString("CurrentUserID");
            ViewBag.Loai = _service.danhSachLoaiSP().ToList();
            ViewData["path"] = "/images/product/";
            ViewData["cart_items"] = _service.ds_GioHang(0, makh).ToList();
            return View(_service.ds_GioHang(0, makh).ToList());
        }

        [HttpPost]
        public PartialViewResult RemoveItems(string madd)
        {
            var model = _service.get_GH(madd);
            if (model.TrangThai == 0)
            {
                _service.xoa_GH(madd);
            }
            return get_cart();
        }

        [HttpPost]
        public PartialViewResult RemoveCartItem(string magh)
        {
            var makh = HttpContext.Session.GetString("CurrentUserID");
            var model = _service.get_GH(magh);
            if (model.TrangThai == 0)
            {
                _service.xoa_GH(magh);
            }

            ViewData["path"] = "/images/product/";
            return PartialView("_Cart_Full", _service.ds_GioHang(0, makh).ToList());
        }

        [HttpPost]
        public JsonResult AddItems(string masp)
        {
            var makh = HttpContext.Session.GetString("CurrentUserID");
            if (makh == null)
            {
                return Json(null);
            }
            string kq = "Not add";
            if (!string.IsNullOrEmpty(masp))
            {
                kq = _service.them_GH(masp, makh);
            }
            return Json(kq);
        }

        [HttpPost]
        public PartialViewResult get_cart()
        {
            var makh = HttpContext.Session.GetString("CurrentUserID");
            return PartialView("_Cart", _service.ds_GioHang(0, makh).ToList());
        }

        [HttpPost]
        public JsonResult Increase(string magh, int sluong)
        {
            _service.increase(magh, sluong);
            return Json(magh + " " + sluong);
        }
    }
}
