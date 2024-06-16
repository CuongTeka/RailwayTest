using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThietBiDienTu.Areas.Admin.Models;

namespace ThietBiDienTu.Areas.Admin.Controllers
{
    public class HoanHangController : Controller
    {
        private ThietBiDienTuEntities1 db = new ThietBiDienTuEntities1();
        // GET: Admin/HoanHang
        public ActionResult HoanHang()
        {
            if (Session["TaiKhoanAD"] != null)
            {
                var soDonHoan = db.HoanHangs.Where(x => x.TrangThai == 3).Count();
                ViewBag.ChoHH = soDonHoan;
                return View();
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }
        public ActionResult HTChapNhanHoan()
        {
            if (Session["TaiKhoanAD"] != null)
            {

                var hh = db.HienThiHoanHangBangTrangThai(1);
                return View(hh.ToList());
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }
        public ActionResult HTKhongChapNhan()
        {
            if (Session["TaiKhoanAD"] != null)
            {

                var hh = db.HienThiHoanHangBangTrangThai(2);
                return View(hh.ToList());
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }


        }
        public ActionResult HTChoChapNhan()
        {
            if (Session["TaiKhoanAD"] != null)
            {

                var hh = db.HienThiHoanHangBangTrangThai(3);
                return View(hh.ToList());
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }
        public ActionResult ChapNhan(int MaHoanHang)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                db.UpdateTrangThaiHoanHang(MaHoanHang, 1);
                return RedirectToAction("HoanHang");
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }
        public ActionResult KChapNhan(int MaHoanHang)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                db.UpdateTrangThaiHoanHang(MaHoanHang, 2);
                return RedirectToAction("HoanHang");
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }
        public ActionResult XemCT(int MaHoanHang)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                var ct = db.GetOrderInfoByMaHoanHang(MaHoanHang);
                return View(ct.FirstOrDefault());
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }
    }
}