using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Mail;
using TN218.Models;
using ZaloPay.Helper;
using ZaloPay.Helper.Crypto;

namespace TN218.Controllers
{
    public class CheckoutController : Controller
    {
        readonly Service _service = new Service();

        [HttpGet]
        public IActionResult Index()
        {
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
            return View(_service.ds_GioHang(2, makh).ToList());
        }

        [HttpGet]
        public IActionResult Confirm(string listgh)
        {
            var st = System.Text.Json.JsonSerializer.Deserialize<List<string>>(listgh);
            var list = new List<GioHang>();
            if (st?.Count() > 0 && !st.First().Contains("GH"))
            {
                string madd = _service.them_GH(st.First());
                list.Add(_service.get_GH(madd));
            }
            if (st?.Count() > 0 && st.First().Contains("GH"))
            {
                foreach (var s in st)
                {
                    var dd = _service.get_GH(s);
                    list.Add(dd);
                }
            }

            var makh = HttpContext.Session.GetString("CurrentUserID");

            ViewBag.Loai = _service.danhSachLoaiSP().ToList();
            ViewData["path"] = "/images/product/";
            if (makh != null)
            {
                ViewData["cart_items"] = _service.ds_GioHang(0, makh).ToList();
                HoaDonModel info = new HoaDonModel();
                var kh = _service.get_KH(makh);
                info.Holot = kh.HoKhachHang;
                info.Ten = kh.TenKhachHang;
                info.SoDienThoai = kh.SoDienThoai;
                info.Email = kh.Email;
                ViewData["info"] = info;
            }
            else
            {
                ViewData["cart_items"] = new List<GioHang>();
                ViewData["info"] = null;
            }
            return View(list);
        }

        [HttpGet]
        public IActionResult Receipt(HoaDonModel model)
        {
            var st = System.Text.Json.JsonSerializer.Deserialize<List<string>>(model.GioHangs);
            var list = new List<GioHang>();
            if (st?.Count() > 0 && !st.First().Contains("GH"))
            {
                string madd = _service.them_GH(st.First());
                list.Add(_service.get_GH(madd));
            }
            if (st?.Count() > 0 && st.First().Contains("GH"))
            {
                foreach (var s in st)
                {
                    var dd = _service.get_GH(s);
                    if (dd.TrangThai != 1)
                        list.Add(dd);
                }
            }

            if (list.Count == 0)
            {
                return RedirectToAction("Index", "HomePage");
            }

            var makh = HttpContext.Session.GetString("CurrentUserID");
            ViewBag.Loai = _service.danhSachLoaiSP().ToList();
            ViewData["path"] = "/images/product/";
            string new_mahd;
            if (makh != null)
            {
                ViewData["cart_items"] = _service.ds_GioHang(0, makh).ToList();
                new_mahd = _service.them_HD(makh, model.Thanhtoan);
            }
            else
            {
                ViewData["cart_items"] = new List<GioHang>();
                new_mahd = _service.them_HD();

            }
            foreach (var gh in list)
            {
                _service.them_CTHD(new_mahd, gh.MaGioHang);
            }
            if (makh != null)
            {
                ViewData["cart_items"] = _service.ds_GioHang(0, makh).ToList();
            }
            else
            {
                ViewData["cart_items"] = new List<GioHang>();
            }
            model.MaHoaDon = new_mahd;
            ViewData["info"] = model;


            //gui email
            long tongHD = 0;
            string chitiet = "";
            foreach (var giohang in list)
            {
                tongHD += (long)giohang.MaSanPhamNavigation.Gia * (long)giohang.SoLuongDat;
                chitiet += _service.itemComponent(giohang.MaSanPhamNavigation.TenSanPham, giohang.SoLuongDat, giohang.MaSanPhamNavigation.Gia, (long)giohang.SoLuongDat * (long)giohang.MaSanPhamNavigation.Gia);
            }
            string tenkh = HttpContext.Session.GetString("CurrentUser");
            string templateForm = "<!DOCTYPE html>\r\n<html>\r\n<head>\r\n<title></title>\r\n<meta charset=\"UTF-8\" />\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">\r\n<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" />\r\n<style type=\"text/css\">\r\n\r\nbody, table, td, a { -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; }\r\ntable, td { mso-table-lspace: 0pt; mso-table-rspace: 0pt; }\r\nimg { -ms-interpolation-mode: bicubic; }\r\n\r\nimg { border: 0; height: auto; line-height: 100%; outline: none; text-decoration: none; }\r\ntable { border-collapse: collapse !important; }\r\nbody { height: 100% !important; margin: 0 !important; padding: 0 !important; width: 100% !important; }\r\n\r\n\r\na[x-apple-data-detectors] {\r\n    color: inherit !important;\r\n    text-decoration: none !important;\r\n    font-size: inherit !important;\r\n    font-family: inherit !important;\r\n    font-weight: inherit !important;\r\n    line-height: inherit !important;\r\n}\r\n\r\n@media screen and (max-width: 480px) {\r\n    .mobile-hide {\r\n        display: none !important;\r\n    }\r\n    .mobile-center {\r\n        text-align: center !important;\r\n    }\r\n}\r\ndiv[style*=\"margin: 16px 0;\"] { margin: 0 !important; }\r\n</style>\r\n<body style=\"margin: 0 !important; padding: 0 !important; background-color: #eeeeee;\" bgcolor=\"#eeeeee\">\r\n\r\n\r\n<div style=\"display: none; font-size: 1px; color: #fefefe; line-height: 1px; font-family: system-ui; max-height: 0px; max-width: 0px; opacity: 0; overflow: hidden;\">\r\nFor what reason would it be advisable for me to think about business content? That might be little bit risky to have crew member like them. \r\n</div>\r\n\r\n<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">\r\n    <tr>\r\n        <td align=\"center\" style=\"background-color: #eeeeee;\" bgcolor=\"#eeeeee\">\r\n        \r\n        <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width:600px;\">\r\n            <tr>\r\n                <td align=\"center\" valign=\"top\" style=\"font-size:0; padding: 35px;background-image: url(https://as1.ftcdn.net/v2/jpg/03/51/66/44/1000_F_351664487_g6NaHezgxg4GzPHEG34fCdvPaW6HqKVM.jpg);\" >\r\n               \r\n                <div style=\"display:inline-block; max-width:50%; min-width:100px; vertical-align:top; width:100%;\">\r\n                    <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width:300px;\">\r\n                        <tr>\r\n                            <td align=\"left\" valign=\"top\" style=\"font-family: system-ui; font-size: 36px; font-weight: 800; line-height: 48px;\" class=\"mobile-center\">\r\n                                <h1 style=\"font-size: 36px; font-weight: 800; margin: 0; color: #ffffff;\">FAST FOOD Cần Thơ</h1>\r\n                            </td>\r\n                            <!-- Open Sans, Helvetica, Arial    Arial, Helvetica, sans-serif, sans-serif-->\r\n                        </tr>\r\n                    </table>\r\n                </div>\r\n                \r\n                <div style=\"display:inline-block; max-width:50%; min-width:100px; vertical-align:top; width:100%;\" class=\"mobile-hide\">\r\n                    <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width:300px;\">\r\n                        <tr>\r\n                            <td align=\"right\" valign=\"top\" style=\"font-family: system-ui; font-size: 48px; font-weight: 400; line-height: 48px;\">\r\n                                <table cellspacing=\"0\" cellpadding=\"0\" border=\"0\" align=\"right\">\r\n                                    <tr>\r\n                                        <td style=\"font-family: system-ui; font-size: 18px; font-weight: 400;\">\r\n                                            <p style=\"font-size: 18px; font-weight: 400; margin: 0; color: #ffffff;\"><a href=\"https://localhost:7132/Home/Index\" target=\"_blank\" style=\"color: #ffffff; text-decoration: none;\">Shop &nbsp;</a></p>\r\n                                        </td>\r\n                                        <td style=\"font-family: system-ui; font-size: 18px; font-weight: 400; line-height: 24px;\">\r\n                                            <a href=\"#\" target=\"_blank\" style=\"color: #ffffff; text-decoration: none;\"><img src=\"https://cdn-icons-png.flaticon.com/512/3075/3075977.png\" width=\"27\" height=\"23\" style=\"display: block; border: 0px;\"/></a>\r\n                                        </td>\r\n                                    </tr>\r\n                                </table>\r\n                            </td>\r\n                        </tr>\r\n                    </table>\r\n                </div>\r\n              \r\n                </td>\r\n            </tr>\r\n            <tr>\r\n                <td align=\"center\" style=\"padding: 35px 35px 20px 35px; background-color: #ffffff;\" bgcolor=\"#ffffff\">\r\n                <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width:600px;\">\r\n                    <tr>\r\n                        <td align=\"center\" style=\"font-family: system-ui; font-size: 16px; font-weight: 400; line-height: 24px; padding-top: 25px;\">\r\n                            <img src=\"https://img.icons8.com/carbon-copy/100/000000/checked-checkbox.png\" width=\"125\" height=\"120\" style=\"display: block; border: 0px;\" /><br>\r\n                            <h2 style=\"font-size: 30px; font-weight: 800; line-height: 36px; color: #333333; margin: 0;\">\r\n                                Xác nhận đơn hàng thành công!\r\n                            </h2>\r\n                        </td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td align=\"left\" style=\"font-family: system-ui; font-size: 16px; font-weight: 400; line-height: 24px; padding-top: 10px;\">\r\n                            <p style=\"font-size: 16px; font-weight: 400; line-height: 24px; color: #777777;\">\r\n                                Xin chào <t>" + tenkh + "</t>, cám ơn bạn đã tin tưởng và đặt hàng tại fastfood.com\r\n                            </p>\r\n                        </td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td align=\"left\" style=\"padding-top: 20px;\">\r\n                            <table cellspacing=\"0\" cellpadding=\"0\" border=\"0\" width=\"100%\">\r\n                                <tr>\r\n                                    <td width=\"75%\" align=\"left\" bgcolor=\"#eeeeee\" style=\"font-family:system-ui; font-size: 16px; font-weight: 800; line-height: 24px; padding: 10px;\">\r\n                                        Tên sản phẩm\r\n                                    </td><td width=\"25%\" align=\"left\" bgcolor=\"#eeeeee\" style=\"font-family:system-ui; font-size: 16px; font-weight: 800; line-height: 24px; padding: 10px;\">\r\n                                        Số lượng\r\n                                    </td>\r\n                                    <td width=\"25%\" align=\"left\" bgcolor=\"#eeeeee\" style=\"font-family:system-ui; font-size: 16px; font-weight: 800; line-height: 24px; padding: 10px;\">\r\n                                        Đơn giá\r\n                                    </td>\r\n                                    <td width=\"75%\" align=\"left\" bgcolor=\"#eeeeee\" style=\"font-family: system-ui; font-size: 16px; font-weight: 800; line-height: 24px; padding: 10px;\">\r\n                                        Thành tiền\r\n                                    </td>\r\n                                </tr>\r\n" + chitiet + "\r\n                            </table>\r\n                        </td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td align=\"left\" style=\"padding-top: 20px;\">\r\n                            <table cellspacing=\"0\" cellpadding=\"0\" border=\"0\" width=\"100%\">\r\n                                <tr>\r\n                                    <td width=\"75%\" align=\"left\" style=\"font-family: system-ui; font-size: 16px; font-weight: 800; line-height: 24px; padding: 10px; border-top: 3px solid #eeeeee; border-bottom: 3px solid #eeeeee;\">\r\n                                        TỔNG\r\n                                    </td>\r\n                                    <td width=\"25%\" align=\"left\" style=\"font-family: system-ui; font-size: 16px; font-weight: 800; line-height: 24px; padding: 10px; border-top: 3px solid #eeeeee; border-bottom: 3px solid #eeeeee;\">\r\n                                        " + tongHD + " VNĐ\r\n                                    </td>\r\n                                </tr>\r\n                            </table>\r\n                        </td>\r\n                    </tr>\r\n                </table>\r\n                \r\n                </td>\r\n            </tr>\r\n             <tr>\r\n                <td align=\"center\" height=\"100%\" valign=\"top\" width=\"100%\" style=\"padding: 0 35px 35px 35px; background-color: #ffffff;\" bgcolor=\"#ffffff\">\r\n                <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width:660px;\">\r\n                    <tr>\r\n                        <td align=\"center\" valign=\"top\" style=\"font-size:0;\">\r\n                            <div style=\"display:inline-block; max-width:100%; min-width:240px; vertical-align:top; width:100%;\">\r\n\r\n                                <table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width:300px;\">\r\n                                    <tr>\r\n                                        <td align=\"left\" valign=\"top\" style=\"font-family: system-ui; font-size: 16px; font-weight: 400; line-height: 24px;\">\r\n                                            <p style=\"font-weight: 800;\">Địa chỉ cửa hàng</p>\r\n                                            <p>Khu II, Đường 3/2, Phường Xuân Khánh\r\n                                                Quận Ninh Kiều, TP.Cần Thơ</p>\r\n\r\n                                        </td>\r\n\r\n                                    </tr>\r\n                                    <tr>\r\n                                        <td align=\"left\" valign=\"top\" style=\"font-family: system-ui; font-size: 16px; font-weight: 400; line-height: 24px;\">\r\n                                            <p style=\"font-weight: 800;\">Liên hệ</p>\r\n                                            <p>Email: Fastfood@gmail.com</p> <br\\>\r\n                                            <p>Số điện thoại: +84 379909081</p>\r\n                                        </td>\r\n\r\n                                    </tr>\r\n                                </table>\r\n                            </div>\r\n                        </td>\r\n                    </tr>\r\n                </table>\r\n                </td>\r\n            </tr>\r\n            <tr>\r\n                <td align=\"center\" style=\" padding: 35px;background-image: url(https://www.chudu24.com/wp-content/uploads/2017/09/1-154.jpg); \">\r\n                <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width:600px;\">\r\n                    <tr>\r\n                        <td align=\"center\" style=\"font-family: system-ui; font-size: 16px; font-weight: 400; line-height: 24px; padding-top: 25px;\">\r\n                            <h2 style=\"font-size: 24px; font-weight: 800; line-height: 30px; color:#DFFF00; margin: 0; background: rgba(0,0,0,.7);\">\r\n                                SẢN PHẨM HOT, KHUYẾN MÃI 30% !!! <br />Gà luộc lá chanh \r\n                            </h2>\r\n                        </td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td align=\"center\" style=\"padding: 25px 0 15px 0;\">\r\n                            <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">\r\n                                <tr>\r\n                                    <td align=\"center\" style=\"border-radius: 5px;\" bgcolor=\"#66b3b7\">\r\n                                      <a href=\"#\" target=\"_blank\" style=\"font-size: 18px; font-family: system-ui; color: #ffffff; text-decoration: none; border-radius: 5px; background-color: #F44336; padding: 15px 30px; border: 1px solid #F44336; display: block;\">MUA NGAY</a>\r\n                                    </td>\r\n                                </tr>\r\n                            </table>\r\n                        </td>\r\n                    </tr>\r\n                </table>\r\n                </td>\r\n            </tr>\r\n            <tr>\r\n                <td align=\"center\" style=\"padding: 35px; background-color: #ffffff;\" bgcolor=\"#ffffff\">\r\n                <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width:600px;\">\r\n                    <tr>\r\n                        <td align=\"center\" style=\"font-family: system-ui; font-size: 14px; font-weight: 400; line-height: 24px; padding: 5px 0 10px 0;\">\r\n                            <p style=\"font-size: 14px; font-weight: 800; line-height: 18px; color: #333333;\">\r\n                                Fast food CT<br>\r\n                                Group 8 - Can Tho 2022.\r\n                            </p>\r\n                        </td>\r\n                    </tr>\r\n                    <tr>\r\n                        <td align=\"left\" style=\"font-family: system-ui; font-size: 14px; font-weight: 400; line-height: 24px;\">\r\n                            <p style=\"font-size: 14px; font-weight: 400; line-height: 20px; color: #777777;\">\r\n                                If you didn't create an account using this email address, please ignore this email or <a href=\"#\" target=\"_blank\" style=\"color: #777777;\">unsusbscribe</a>.\r\n                            </p>\r\n                        </td>\r\n                    </tr>\r\n                </table>\r\n                </td>\r\n            </tr>\r\n        </table>\r\n        </td>\r\n    </tr>\r\n</table>\r\n    \r\n</body>\r\n</html>\r\n";

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("khoiun10x@gmail.com");
            //var makh = HttpContext.Session.GetString("CurrentUserID");
			//var kh = _service.get_KH(makh);
			mailMessage.To.Add(new MailAddress(model.Email));
			//ConfirmEmail confirmEmail = new ConfirmEmail();
			mailMessage.Subject = "Xác nhận đặt hàng thành công!";
			//mailMessage.Body = "Xin Chào, "+ kh.TenKhachHang +"!\nChúng tôi vừa xác nhận đơn hàng bạn vừa đặt vào lúc : ";

			mailMessage.IsBodyHtml = true;
			mailMessage.Body = templateForm;
			SmtpClient smtp = new SmtpClient();
			smtp.Port = 587; // 25 465
			smtp.EnableSsl = true;
			smtp.UseDefaultCredentials = false;
			smtp.Host = "smtp.gmail.com";
			smtp.Credentials = new System.Net.NetworkCredential("khoiun10x@gmail.com", "Uqqtcewnmiwhkema");
			smtp.Send(mailMessage);

			return View(list);
        }

        [HttpPost]
        public async Task<String> CheckoutWallet(string listgh, string payment)
        {
            String info = "";
            if (payment == "zalopay")
            {
                var st = System.Text.Json.JsonSerializer.Deserialize<List<string>>(listgh);
                List<GioHang> list_gh = new List<GioHang>();

                long? total = 0;
                if (st?.Count() > 0 && !st.First().Contains("GH"))
                {
                    SanPham sp = _service.getSanPham(st.First());
                    total = sp.Gia;
                    info += sp.TenSanPham + ": " + sp.Gia + " x1";
                }
                if (st?.Count() > 0 && st.First().Contains("GH"))
                {
                    foreach (var s in st)
                    {
                        var gh = _service.get_GH(s);
                        list_gh.Add(gh);
                        SanPham sp = gh.MaSanPhamNavigation;
                        info += sp.TenSanPham + ": " + sp.Gia + " x" + gh.SoLuongDat + ", ";
                        total += gh.SoLuongDat * gh.MaSanPhamNavigation.Gia;
                    }
                }

                string app_id = "2553";
                string key1 = "PcY4iZIKFCIdgZvA6ueMcMHHUbRLYjPL";
                string create_order_url = "https://sb-openapi.zalopay.vn/v2/create";

                Random rnd = new Random();
                var embed_data = new { };
                var items = new[] { new { msg = "hello" } };
                //var items = list_gh;
                var param = new Dictionary<string, string>();
                var app_trans_id = rnd.Next(1000000); // Generate a random order's ID.

                param.Add("app_id", app_id);
                param.Add("app_user", "user123");
                param.Add("app_time", Utils.GetTimeStamp().ToString());
                param.Add("amount", total.ToString());
                param.Add("app_trans_id", DateTime.Now.ToString("yyMMdd") + "_" + app_trans_id); // mã giao dich có định dạng yyMMdd_xxxx
                param.Add("embed_data", JsonConvert.SerializeObject(embed_data));
                param.Add("item", JsonConvert.SerializeObject(items));
                param.Add("description", info);
                param.Add("bank_code", "zalopayapp");

                var data = app_id + "|" + param["app_trans_id"] + "|" + param["app_user"] + "|" + param["amount"] + "|"
                        + param["app_time"] + "|" + param["embed_data"] + "|" + param["item"];
                param.Add("mac", HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, key1, data));

                var result = await HttpHelper.PostFormAsync(create_order_url, param);
                return JsonConvert.SerializeObject(result);
            }
            return JsonConvert.SerializeObject(new { payment = payment });
        }
    }
}
