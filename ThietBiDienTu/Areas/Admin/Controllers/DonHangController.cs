using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThietBiDienTu.Areas.Admin.Models;

namespace ThietBiDienTu.Areas.Admin.Controllers
{
    public class DonHangController : Controller
    {
        private ThietBiDienTuEntities1 db = new ThietBiDienTuEntities1();
        //
        // GET: /Admin/NhanVien/
        public ActionResult QuanLyDonHang()
        {
            if (Session["TaiKhoanAD"] != null)
            {

                var ddh = db.GetOrderInfoByStatus1(1);
                return View(ddh.ToList());
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }
        public ActionResult DaGiao()
        {
            if (Session["TaiKhoanAD"] != null)
            {

                var ddh = db.GetOrderInfoByStatus1(2);
                return View(ddh.ToList());
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }
        public ActionResult XacNhanDaGiao(int id)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                var dh = db.DDHs.Find(id);


                dh.TrangThai = 3;


                db.SaveChanges();
                return RedirectToAction("QuanLyDonHang");
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }
        public ActionResult XacNhanDH(int id)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                var dh = db.DDHs.Find(id);


                dh.TrangThai = 2;


                db.SaveChanges();
                return RedirectToAction("QuanLyDonHang");
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }
        public ActionResult HuyDH(int MaDDH)
        {
            if (Session["TaiKhoanAD"] != null)
            {
                ViewBag.MaDDH = MaDDH;
                return View();
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }
        [HttpPost]
        public ActionResult HuyDH(int MaDDH, String LyDo)
        {

            if (Session["TaiKhoanAD"] != null)
            {

                db.CapNhatHuyDonHangByMaDDH(MaDDH, LyDo);
                return RedirectToAction("QuanLyDonHang");
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }




        }
        public ActionResult DaHuy()
        {
            if (Session["TaiKhoanAD"] != null)
            {
                var DaHuy = db.HuyHangs.Where(x => x.MaKH == 3);
                return View(DaHuy.ToList());
            }
            else
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }

        }
        public ActionResult XemChiTietDH(int idDDH)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                var XemCT = db.GetOrderInfoByMaDDH(idDDH);


                return View(XemCT.ToList());
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }



        }
    }
}