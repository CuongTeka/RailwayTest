using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThietBiDienTu.Areas.Admin.Models;

namespace ThietBiDienTu.Controllers
{
    public class GioHangController : Controller
    {
        private ThietBiDienTuEntities1 db = new ThietBiDienTuEntities1();



        public ActionResult SPGioHang()
        {
            // Kiểm tra xem session có tồn tại không
            if (Session["TaiKhoan"] != null)
            {
                // Lấy giá trị từ session


                var User = (KhachHang)Session["TaiKhoan"];
                int idUser = User.MaKH;
                var result = db.GetCartItems(idUser).ToList();
                var cartItems = result.Select(item => new GetCartItems_Result
                {
                    MaSP = item.MaSP,
                    TenSP = item.TenSP,
                    GiaTien = item.GiaTien,
                    SoLuong = item.SoLuong,
                    ThanhTien = item.ThanhTien,
                    LinkHinhAnhChinh = item.LinkHinhAnhChinh
                }).ToList();

                return View(cartItems);
            }
            else
            {
                // Nếu session không tồn tại, có thể chuyển hướng đến trang đăng nhập hoặc thực hiện xử lý khác
                return RedirectToAction("DangNhap", "TrangChu"); // Thay "TenController" bằng tên của controller đăng nhập
            }

        }
        public ActionResult XemGioHang()
        {
            // Kiểm tra xem session có tồn tại không
            if (Session["TaiKhoan"] != null)
            {

                var User = (KhachHang)Session["TaiKhoan"];

                int idUser = User.MaKH;
                var result = db.GetCartItems(idUser).ToList();
                var cartItems = result.Select(item => new GetCartItems_Result
                {
                    MaSP = item.MaSP,
                    TenSP = item.TenSP,
                    GiaTien = item.GiaTien,
                    SoLuong = item.SoLuong,
                    ThanhTien = item.ThanhTien,
                    LinkHinhAnhChinh = item.LinkHinhAnhChinh
                }).ToList();

                return View(cartItems);
            }
            else
            {

                return RedirectToAction("DangNhap", "TrangChu");
            }


        }
        public ActionResult RemoveFromProduct(int masp)
        {

            if (Session["TaiKhoan"] != null)
            {

                var User = (KhachHang)Session["TaiKhoan"];

                db.RemoveFromCart(User.MaKH, masp);
                return RedirectToAction("SanPham", "SanPham");
            }
            else
            {

                return RedirectToAction("DangNhap", "TrangChu");
            }

        }
        public ActionResult soLuongTrongGio()
        {
            if (Session["TaiKhoan"] != null)
            {
                var User = (KhachHang)Session["TaiKhoan"];

                var gh = db.GioHangs.SingleOrDefault(x => x.MaKH == User.MaKH);
                int soLuongTrongCTGioHang = 0;
                if (gh != null)
                {
                    soLuongTrongCTGioHang = db.CTGioHangs.Count(x => x.MaGioHang == gh.MaGioHang);
                }
                else
                {
                    soLuongTrongCTGioHang = 0;
                }


                ViewBag.SoLuongTrongGioHang = soLuongTrongCTGioHang;
                return View();
            }
            else
            {
                ViewBag.SoLuongTrongGioHang = 0;
                return View();
            }

        }
        [HttpPost]
        public ActionResult UpdateQuantity(int masp, int soluong)
        {
            // Gọi proc
            if (Session["TaiKhoan"] != null)
            {
                var User = (KhachHang)Session["TaiKhoan"];
                if (soluong <= 0)
                {
                    db.RemoveFromCart(User.MaKH, masp);
                    return RedirectToAction("GioHang");
                }


                db.UpdateCartItemQuantity(User.MaKH, masp, soluong);

                return RedirectToAction("GioHang");
            }
            else
            {

                return RedirectToAction("DangNhap", "TrangChu");
            }

        }
        public ActionResult RemoveFromCart(int masp)
        {
            if (Session["TaiKhoan"] != null)
            {

                var User = (KhachHang)Session["TaiKhoan"];

                db.RemoveFromCart(User.MaKH, masp);

                return RedirectToAction("GioHang"); // Quay lại trang danh sách
            }
            else
            {

                return RedirectToAction("DangNhap", "TrangChu");
            }


        }

        //
        // GET: /GioHang/
        public ActionResult GioHang()
        {
            if (Session["TaiKhoan"] != null)
            {

                var User = (KhachHang)Session["TaiKhoan"];
                int idUser = User.MaKH;
                var result = db.GetCartItems(idUser).ToList();
                var cartItems = result.Select(item => new GetCartItems_Result
                {
                    MaSP = item.MaSP,
                    TenSP = item.TenSP,
                    GiaTien = item.GiaTien,
                    SoLuong = item.SoLuong,
                    ThanhTien = item.ThanhTien,
                    LinkHinhAnhChinh = item.LinkHinhAnhChinh,
                    SoLuongTon = item.SoLuongTon
                }).ToList();

                return View(cartItems);

            }
            else
            {

                return RedirectToAction("DangNhap", "TrangChu");
            }


        }

    }
}