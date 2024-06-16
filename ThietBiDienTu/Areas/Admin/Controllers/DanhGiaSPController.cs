using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThietBiDienTu.Areas.Admin.Models;

namespace ThietBiDienTu.Areas.Admin.Controllers
{
    public class DanhGiaSPController : Controller
    {
        private ThietBiDienTuEntities1 db = new ThietBiDienTuEntities1();
        //
        // GET: /Admin/DanhGiaSP/
        public ActionResult Index()
        {
            if (Session["TaiKhoanAD"] != null)
            {

                var choXacNhanCount = db.DanhGiaSanPhams
                          .Where(x => x.TrangThai == 3)
                          .Count();
                ViewBag.ChoXacNhanCount = choXacNhanCount;
                return View();
            }
            else
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }

        }
        public ActionResult hienThiDanhGia()
        {
            if (Session["TaiKhoanAD"] != null)
            {

                var danhgiahienthi = db.GetDanhGia1();

                return View(danhgiahienthi.ToList());
            }
            else
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }

        }
        public ActionResult khongHienThiDanhGia()
        {
            if (Session["TaiKhoanAD"] != null)
            {

                var danhgiahienthi = db.GetDanhGia2();

                return View(danhgiahienthi.ToList());
            }
            else
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }

        }
        public ActionResult choDanhGia()
        {
            if (Session["TaiKhoanAD"] != null)
            {

                var danhgiahienthi = db.GetDanhGia3();

                return View(danhgiahienthi.ToList());
            }
            else
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }

        }

        public ActionResult AnDanhGia(int id)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                // Lấy thông tin đánh giá cần ẩn
                var danhGia = db.DanhGiaSanPhams.Find(id);

                // Cập nhật trạng thái
                danhGia.TrangThai = 2;

                // Lưu thay đổi
                db.SaveChanges();

                // Trả về trang hiện tại
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }

        }

        public ActionResult HienThi(int id)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                var danhGia = db.DanhGiaSanPhams.Find(id);

                danhGia.TrangThai = 1;

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }
            // Tương tự như trên

        }
    }
}