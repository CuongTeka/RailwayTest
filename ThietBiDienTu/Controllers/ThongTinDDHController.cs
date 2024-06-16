using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThietBiDienTu.Areas.Admin.Models;

namespace ThietBiDienTu.Controllers
{
    public class ThongTinDDHController : Controller
    {
        private ThietBiDienTuEntities1 db = new ThietBiDienTuEntities1();
        //
        // GET: /ThongTinDDH/


        public ActionResult ThongTinDDH()
        {

            // Kiểm tra xem session có tồn tại không
            if (Session["TaiKhoan"] != null)
            {
                // Lấy giá trị từ session
                var taiKhoan = (KhachHang)Session["TaiKhoan"];

                var mggList = db.GetMaGiamGiaInfoForKhachHangAndTrangThai(taiKhoan.MaKH, 0);
                ViewBag.MGGList = new SelectList(mggList, "MaGiamGia", "TenMGG","TiLeGiam");

                var dvvc = db.NhaVanChuyens.ToList();
                ViewBag.NVCList = new SelectList(dvvc, "MaNVC", "TenNVC", "ChiPhi");

                var gia=db.GioHangs.Where(x=>x.MaKH==taiKhoan.MaKH).SingleOrDefault();
                ViewBag.GiaGio = gia.TongTienGioHang;

                var diaChi = db.GetCustomerAddress(taiKhoan.MaKH);
                ViewBag.CoTK = taiKhoan.MaKH;
                return View(diaChi.ToList());
            }
            else
            {
                ViewBag.CoTK = 0;
                // Nếu session không tồn tại, có thể chuyển hướng đến trang đăng nhập hoặc thực hiện xử lý khác
                return RedirectToAction("DangNhap", "TrangChu"); // Thay "TenController" bằng tên của controller đăng nhập
            }
        }
        [HttpPost]
        public ActionResult ThongTinDDH(string lastName, string sdt, string sonha, string duong, string phuong, string quanhuyen, string tinhthanh, string desp, string mgg, int manvc)
        {
            if (Session["TaiKhoan"] != null)
            {
                var User = (KhachHang)Session["TaiKhoan"];
                string maGiamGia = Request.Form["MaGiamGia"];
                int DVVC = Convert.ToInt32(Request.Form["MaNVC"]);
                db.CreateOrderFromCart(User.MaKH, maGiamGia, DVVC, sonha, duong, quanhuyen, phuong, quanhuyen, lastName, sdt, desp);
                if (maGiamGia != "")
                {

                    var maGiamGiaKhachHang = db.MaGiamGia_KhachHang.SingleOrDefault(m => m.MaGiamGia == maGiamGia && m.MaKH == User.MaKH);



                    maGiamGiaKhachHang.TrangThai = 1;


                    db.SaveChanges();

                }

                ViewBag.CoTK = User.MaKH;
                return RedirectToAction("Index", "DDHKH");
            }
            else
            {
                ViewBag.CoTK = 0;
                // Nếu session không tồn tại, có thể chuyển hướng đến trang đăng nhập hoặc thực hiện xử lý khác
                return RedirectToAction("DangNhap", "TrangChu"); // Thay "TenController" bằng tên của controller đăng nhập
            }
        }

        //[HttpPost]
        //public ActionResult ThongTinDDH()
        //{
        //    return View();
        //}
        public ActionResult viewNVC()
        {
            var dsNVC = db.NhaVanChuyens.ToList();


            return View(dsNVC);
        }

        public ActionResult ThemMoiDiaChi()
        {


            return View();
        }
        [HttpPost]
        public ActionResult ThemMoiDiaChi(CTDiaChi dcmoi)
        {
            if (Session["TaiKhoan"] != null)
            {
                if (ModelState.IsValid)
                { 
                var User = (KhachHang)Session["TaiKhoan"];


                var rs = db.InsertCustomerAddress(User.MaKH, dcmoi.SoNha, dcmoi.TenDuong, dcmoi.PhuongXa, dcmoi.QuanHuyen, dcmoi.TinhThanh, dcmoi.MacDinh, dcmoi.TenNguoiNhan, dcmoi.SDTNguoiNhan);
                ViewBag.CoTK = User.MaKH;
                return RedirectToAction("ThongTinDDH");
            }
                else
                {
                    ModelState.AddModelError("loi", "Vui lòng nhập đầy đủ");
                    return View();
                }    
            }
            else
            {
                ViewBag.CoTK = 0;
                // Nếu session không tồn tại, có thể chuyển hướng đến trang đăng nhập hoặc thực hiện xử lý khác
                return RedirectToAction("DangNhap", "TrangChu"); // Thay "TenController" bằng tên của controller đăng nhập
            }
        }
    }
}