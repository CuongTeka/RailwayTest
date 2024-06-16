using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThietBiDienTu.Areas.Admin.Models;

namespace ThietBiDienTu.Areas.Admin.Controllers
{
    public class DangNhapController : Controller
    {
        private ThietBiDienTuEntities1 db = new ThietBiDienTuEntities1();
        // GET: Admin/DangNhap
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(string tendn, string mk)
        {
            var tk = db.NhanViens.SingleOrDefault(x => x.TenTaiKhoanAdmin == tendn && x.MatKhauAdmin == mk);
            if (tk == null)
            {
                ModelState.AddModelError("mkktt", "Tài khoản không tồn tại");
                ViewBag.tentaikhoanAdmin = tk;
                return View();
            }
            if (tk.MatKhauAdmin != mk)
            {
                ModelState.AddModelError("mkktt", "Mật khẩu không chính xác");
                ViewBag.tentaikhoanAdmin = tk;
                return View();
            }



            Session["TaiKhoanAd"] = tk.MaAdmin;
            Session["PhanQuyen"] = tk.MaQuyen;

            return RedirectToAction("AdminTrangChu", "AdminTrangChu");

        }
    }
}