using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThietBiDienTu.Areas.Admin.Models;

namespace ThietBiDienTu.Areas.Admin.Controllers
{
    public class DoanhThuController : Controller
    {
        private ThietBiDienTuEntities1 db = new ThietBiDienTuEntities1();
        // GET: Admin/DoanhThu
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult TKBaThang()
        {
            if (Session["TaiKhoanAD"] != null)
            {
                int thangHienTai = DateTime.Now.Month;
                int thangTruoc = thangHienTai - 1;
                int thangTruocTruoc = thangTruoc - 1;
                ViewBag.tHT = thangHienTai;
                ViewBag.tT = thangTruoc;
                ViewBag.tTT = thangTruocTruoc;

                var tkTHT = db.ThongKeThang(DateTime.Now.Year, thangHienTai).FirstOrDefault();
                ViewBag.tkTHienTai = (int)tkTHT.TienThang;

                var tkTT = db.ThongKeThang(DateTime.Now.Year, thangTruoc).FirstOrDefault();
                ViewBag.tkThangTruoc = (int)tkTT.TienThang;

                var tkTTT = db.ThongKeThang(DateTime.Now.Year, thangTruocTruoc).FirstOrDefault();
                decimal tienThangTruocTruoc = (decimal)tkTTT.TienThang;

                ViewBag.tkTTruocTruoc = (int)tienThangTruocTruoc;

                return View();
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }
        }
        [HttpGet]
        public ActionResult TKTheoThang(int Thang, int Nam)
        {
            if (Session["TaiKhoanAD"] != null)
            {
                var tk = db.ThongKeHoaDonTheoThang(Thang, Nam, 3);
                return View(tk.ToList());
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }
        }
        public ActionResult TkTheoNgay(DateTime ngay)
        {
            int Ngay = ngay.Day;
            int Thang = ngay.Month;
            int Nam = ngay.Year;
            if (Session["TaiKhoanAD"] != null)
            {
                var ctdh = db.ThongKeHoaDonTheoNgay(Ngay, Thang, Nam, 3);
                return View(ctdh.ToList());
            }
            else
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }
        }
        public ActionResult TKTheoKhoan(DateTime ngaybd, DateTime ngaykt)
        {
            if (Session["TaiKhoanAD"] != null)
            {
                
                if (ngaybd > ngaykt)
                {
                    
                    ModelState.AddModelError("mkktt", "Ngày bắt đầu phải nhỏ hơn ngày kết thúc.");
                    return View();
                }

                var ctdh = db.ThongKeHDTheoKhoang(ngaybd, ngaykt, 3);
                return View(ctdh.ToList());
            }
            else
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }
        }

        public ActionResult TKBanRa()
        {

            if (Session["TaiKhoanAD"] != null)
            {
                var ctdh = db.ThongKeThang(DateTime.Now.Year,DateTime.Now.Month);
                return View(ctdh.ToList());
            }
            else
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }
        }
        public ActionResult TKTuanBanRa()
        {

            if (Session["TaiKhoanAD"] != null)
            {
                var ctdh = db.ThongKeTuan();
                return View(ctdh.ToList());
            }
            else
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }
        }
        public ActionResult TKNamBanRa()
        {

            if (Session["TaiKhoanAD"] != null)
            {
                var ctdh = db.ThongKeNam(DateTime.Now.Year);
                return View(ctdh.ToList());
            }
            else
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }
        }
    }
}