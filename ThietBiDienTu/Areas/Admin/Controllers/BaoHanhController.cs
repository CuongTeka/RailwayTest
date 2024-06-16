using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThietBiDienTu.Areas.Admin.Models;

namespace ThietBiDienTu.Areas.Admin.Controllers
{
    public class BaoHanhController : Controller
    {
        private ThietBiDienTuEntities1 db = new ThietBiDienTuEntities1();
        // GET: Admin/BaoHanh
        public ActionResult BaoHanh()
        {
            if (Session["TaiKhoanAD"] != null)
            {
                var soDonBH = db.YeuCauBHs.Where(x => x.TrangThai == 3).Count();
                ViewBag.ChoBH = soDonBH;
                return View();
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }

        public ActionResult YeuCauBH()
        {

            if (Session["TaiKhoanAD"] != null)
            {

                var yc = db.sp_HienThiYeuCauBHTheoTrangThai(3);
                return View(yc.ToList());
            }
            else
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }
        }
        public ActionResult HTChapNhanBH()
        {

            if (Session["TaiKhoanAD"] != null)
            {

                var yc = db.sp_HienThiYeuCauBHTheoTrangThai(1);
                return View(yc.ToList());
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }
        }
        public ActionResult HTKhongChapNhanBH()
        {

            if (Session["TaiKhoanAD"] != null)
            {

                var yc = db.sp_HienThiYeuCauBHTheoTrangThai(2);
                return View(yc.ToList());
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }
        }
        public ActionResult ChapNhanBH(int MaPBH)
        {

            if (Session["TaiKhoanAD"] != null)
            {

                db.sp_CapNhatTrangThaiBaoHanh(MaPBH, 1, 4);
                return RedirectToAction("BaoHanh");
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }
        }
        public ActionResult TuChoiBH(int MaPBH)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                db.sp_CapNhatTrangThaiBaoHanh(MaPBH, 2, 3);
                return RedirectToAction("BaoHanh");
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }


    }
}