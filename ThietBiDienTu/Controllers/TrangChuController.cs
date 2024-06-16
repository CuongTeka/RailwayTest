using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using PagedList;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using ThietBiDienTu.Areas.Admin.Models;
using System.Net;

namespace ThietBiDienTu.Controllers
{
    public class TrangChuController : Controller
    {
        ThietBiDienTuEntities1 db = new ThietBiDienTuEntities1();
        //
        // GET: /TrangChu/
        public ActionResult TrangChu()
        {
            if (Session["TaiKhoan"] != null)
            {

                var User = (KhachHang)Session["TaiKhoan"];

                int idUser = User.MaKH;

                ViewBag.CoTK = idUser;
                var mggCount=0;
                var mggHT = db.MaGiamGias.FirstOrDefault(x => x.TrangThai == 1);
                if(mggHT!=null)
                {
                    mggCount = (from mgg in db.MaGiamGias
                                join mggkh in db.MaGiamGia_KhachHang on mgg.MaGiamGia1 equals mggkh.MaGiamGia
                                where mgg.MaGiamGia1 == mggHT.MaGiamGia1 && mggkh.MaKH == idUser
                                select new { mgg, mggkh }).Count();
                }    
                else
                {
                    mggCount = 0;
                }    
                int trangthai = 0;
                 

                
                if (mggCount > 0)
                {
                    trangthai = 1;
                }


                if (trangthai == 0)
                {
                    Session["daluu"] = 0;

                }
                else
                {
                    Session["daluu"] = 1;

                }
                return View();
            }
            else
            {
                ViewBag.CoTK = 0;
                return View();
            }

        }
        public ActionResult LayMGG(string mgg)
        {
            if (Session["TaiKhoan"] != null)
            {
                var User = (KhachHang)Session["TaiKhoan"];
                int idUser = User.MaKH;
                db.LuuMaGiamGiaForKhachHang(mgg, idUser);
                return RedirectToAction("TrangChu", "TrangChu");
            }
            else
            {

                return RedirectToAction("DangNhap", "TrangChu");
            }
        }
        public ActionResult SuKienMGG()
        {

            var mggList = db.MaGiamGias.Where(x => x.TrangThai == 1).ToList();

            return View(mggList);
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(KhachHang taikhoan, string RePassword)
        {
            if (ModelState.IsValid)
            {
                if (taikhoan.MKTKKH != RePassword)
                {
                    ModelState.AddModelError("RePass", "Mật khẩu nhập lại không đúng");
                    return View();
                }
                var existingEmail = db.KhachHangs.FirstOrDefault(kh => kh.Email == taikhoan.Email);
                if (existingEmail != null)
                {
                    ModelState.AddModelError("Email", "Email đã tồn tại");
                    return View();
                }

                var existingSDT = db.KhachHangs.FirstOrDefault(kh => kh.SoDienThoaiKH == taikhoan.SoDienThoaiKH);
                if (existingSDT != null)
                {
                    ModelState.AddModelError("SDT", "Số điện thoại đã tồn tại");
                    return View();
                }
                var existingAccount = db.KhachHangs.FirstOrDefault(kh => kh.TenTKKH == taikhoan.TenTKKH);

                if (existingAccount == null)
                {
                    taikhoan.MaQuyen = 1;
                    db.KhachHangs.Add(taikhoan);
                    db.SaveChanges();

                    return RedirectToAction("DangNhap");
                }
                else
                {
                    
                    ModelState.AddModelError("TenTK", "Tên tài khoản đã tồn tại");
                    return View();
                }
            }

            // ModelState.IsValid == false, quay lại trang đăng ký để hiển thị lỗi
            return View();
        }

        //mã hóa mật khẩu
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }



        [HttpGet]
        public ActionResult DangNhap()
        {

            return View();
        }
        public ActionResult DangNhap(string tentaikhoan, string matkhau)
        {
            var taikhoan = db.KhachHangs.SingleOrDefault(x => x.TenTKKH.Equals(tentaikhoan));

            if (taikhoan == null)
            {
                ModelState.AddModelError("tkktt", "Tài khoản hoặc mật khẩu không đúng");

                ViewBag.tentaikhoan = tentaikhoan;
                return View();
            }

            if (taikhoan.MKTKKH != matkhau)
            {
                // Tăng số lần đăng nhập không thành công
                int soLanDangNhapThatBai = 1;
                if (Session["SoLanDangNhapThatBai"] != null)
                {
                    soLanDangNhapThatBai = (int)Session["SoLanDangNhapThatBai"] + 1;
                }
                Session["SoLanDangNhapThatBai"] = soLanDangNhapThatBai;

                
                if (soLanDangNhapThatBai >= 5)
                {
                    
                    Session["SoLanDangNhapThatBai"] = null;
                    return RedirectToAction("QuenMK", "TrangChu");
                }

                ModelState.AddModelError("mkktt", "Mật khẩu không chính xác");

                ViewBag.tentaikhoan = tentaikhoan;
                return View();
            }

            
            Session["SoLanDangNhapThatBai"] = null;

            var anhtk = taikhoan.AnhKH;
            var tentk = taikhoan.TenKH;
            Session["anhtk"] = anhtk;
            Session["tentk"] = tentk;

            //Lưu vào session
            Session["TaiKhoan"] = taikhoan;
            return RedirectToAction("TrangChu");
        }

        public ActionResult QuenMK()
        {
            Session["MKKH"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult QuenMK(String TenTKKH, String Email, String SoDienThoaiKH)
        {
            Session["MKKH"] = null;
            var tk=db.KhachHangs.Where(x=>x.TenTKKH==TenTKKH && x.Email==Email && x.SoDienThoaiKH==SoDienThoaiKH).SingleOrDefault();
            if(tk!=null)
            {
                
                Session["MKKH"]= tk.MKTKKH;
                return View();
            }
            else
            {
                ModelState.AddModelError("quenmk", "Thông tin bạn cung cấp chưa chính xác");
                return View();
            }    
        }
        public ActionResult SPNoiBac()
        {
            if (Session["TaiKhoan"] != null)
            {

                var User = (KhachHang)Session["TaiKhoan"];

                int idUser = User.MaKH;

                ViewBag.CoTK = idUser;
                List<int?> spnoibacNullable = db.DanhGiaSanPhams
                 .OrderByDescending(p => p.DanhGia)
                 .Take(5)
                 .Select(p => p.MaSP)
                 .ToList();
                var sanpham = db.SanPhams
                                .Where(p => spnoibacNullable.Contains(p.MaSP) && p.TrangThai == 1)
                                .Include(s => s.DanhMuc)
                                .Include(s => s.HangSanXuat)
                                .Include(s => s.HinhAnh)
                                .ToList();

                return View(sanpham);

            }
            else
            {
                ViewBag.CoTK = 0;
                List<int?> spnoibacNullable = db.DanhGiaSanPhams
                 .OrderByDescending(p => p.DanhGia)
                 .Take(5)
                 .Select(p => p.MaSP)
                 .ToList();

                var sanpham = db.SanPhams
                                .Where(p => spnoibacNullable.Contains(p.MaSP) && p.TrangThai == 1)
                                .Include(s => s.DanhMuc)
                                .Include(s => s.HangSanXuat)
                                .Include(s => s.HinhAnh)
                                .ToList();

                return View(sanpham);

            }

        }
        public ActionResult GioiThieu()
        {
            return View();
        }
        public ActionResult TimKiem(string timkiem, int? page)
        {
            int pageSize = 4; //Số lượng sản phẩm muốn hiển thị trong 1 trang
            int pageNumber = page ?? 1;
            var listSP = db.SanPhams.Where(m => m.TenSP.Contains(timkiem) && m.TrangThai == 1);
            ViewBag.tukhoa = timkiem;
            return View(listSP.OrderBy(m=>m.TenSP).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult DSDanhMuc()
        {
            var listDanhMucSP = db.SanPhams;
            return View(listDanhMucSP);
        }
        public ActionResult SanPhamTheoHSX(int? madanhmuc, int? mahang, int? page)
        {
            int pageSize = 4; // Số lượng sản phẩm muốn hiển thị trong 1 trang
            int pageNumber = page ?? 1;

            if (madanhmuc == null || mahang == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var lstSP = db.SanPhams
                .Where(n => n.MaDanhMuc == madanhmuc && n.MaHang == mahang && n.TrangThai == 1)
                .OrderBy(s => s.TenSP); // Ensure the list is ordered

            if (lstSP.Count() == 0)
            {
                return View(lstSP.OrderBy(m => m.TenSP).ToPagedList(pageNumber, pageSize));
            }

            var pagedSanPham = lstSP.ToPagedList(pageNumber, pageSize);
            return View(pagedSanPham);
        }
        //Điện thoại bán chạy
        public ActionResult DienThoaiBanChay()
        {
            try
            {
                int idUser = 0;
                List<int?> topSanPhamIds = new List<int?>();

                if (Session["TaiKhoan"] != null)
                {
                    var user = (KhachHang)Session["TaiKhoan"];
                    idUser = user.MaKH;
                    ViewBag.CoTK = idUser;

                    var sanPhamDienThoai = db.SanPhams
                        .Where(p => p.DanhMuc.MaDanhMuc == 1 && p.TrangThai==1)  
                        .OrderByDescending(p => p.DanhGiaSanPhams.Count())  
                        .Take(3)  
                        .Include(s => s.DanhMuc)
                        .Include(s => s.HangSanXuat)
                        .Include(s => s.HinhAnh)
                        .ToList();

                    return View(sanPhamDienThoai);
                }
                else
                {
                    ViewBag.CoTK = 0;

                   
                    var sanPhamTheoDanhGia = db.SanPhams
                        .Where(p => p.DanhMuc.MaDanhMuc == 1 && p.TrangThai == 1)  
                        .OrderByDescending(p => p.DanhGiaSanPhams.Count())  
                        .Take(3)  
                        .Include(s => s.DanhMuc)
                        .Include(s => s.HangSanXuat)
                        .Include(s => s.HinhAnh)
                        .ToList();

                    return View(sanPhamTheoDanhGia);
                }
            }
            catch (Exception ex)
            {
              
                Console.WriteLine($"Lỗi trong DienThoaiBanChay: {ex.Message}");

            }

           
            return View("Error");
        }

        public ActionResult DienThoaiBanChayTheoSoLuongBan()
        {
            try
            {
                int idUser = 0;

                if (Session["TaiKhoan"] != null)
                {
                    var user = (KhachHang)Session["TaiKhoan"];
                    idUser = user.MaKH;
                    ViewBag.CoTK = idUser;

                 
                    var sanPhamDienThoai = db.SanPhams
                        .Where(p => p.DanhMuc.MaDanhMuc == 1 && p.TrangThai == 1)  
                        .OrderByDescending(p => p.SoLuongDaBanRa)  
                        .Take(3)  
                        .Include(s => s.DanhMuc)
                        .Include(s => s.HangSanXuat)
                        .Include(s => s.HinhAnh)
                        .ToList();

                    return View(sanPhamDienThoai);
                }
                else
                {
                    ViewBag.CoTK = 0;

                    var sanPhamBanChay = db.SanPhams
                        .Where(p => p.DanhMuc.MaDanhMuc == 1 && p.TrangThai == 1) 
                        .OrderByDescending(p => p.SoLuongDaBanRa) 
                        .Take(3)  
                        .Include(s => s.DanhMuc)
                        .Include(s => s.HangSanXuat)
                        .Include(s => s.HinhAnh)
                        .ToList();

                    return View(sanPhamBanChay);
                }
            }
            catch (Exception ex)
            {
             
                Console.WriteLine($"Lỗi trong DienThoaiBanChayTheoHangSanXuat: {ex.Message}");
             
            }

           
            return View("Error");
        }
        //Laptop
        public ActionResult LaptopBanChayTheoDanhGia()
        {
            try
            {
                int idUser = 0;

                if (Session["TaiKhoan"] != null)
                {
                    var user = (KhachHang)Session["TaiKhoan"];
                    idUser = user.MaKH;
                    ViewBag.CoTK = idUser;

                    var laptopBanChay = db.SanPhams
                        .Where(p => p.DanhMuc.TenDanhMuc.ToLower() == "Laptop" && p.TrangThai == 1)  
                        .OrderByDescending(p => p.DanhGiaSanPhams.Count()) 
                        .Take(3)  
                        .Include(s => s.DanhMuc)
                        .Include(s => s.HangSanXuat)
                        .Include(s => s.HinhAnh)
                        .ToList();

                    return View(laptopBanChay);
                }
                else
                {
                    ViewBag.CoTK = 0;

                    var laptopTheoDanhGia = db.SanPhams
                        .Where(p => p.DanhMuc.TenDanhMuc.ToLower() == "Laptop" && p.TrangThai == 1)  
                        .OrderByDescending(p => p.DanhGiaSanPhams.Count()) 
                        .Take(3) 
                        .Include(s => s.DanhMuc)
                        .Include(s => s.HangSanXuat)
                        .Include(s => s.HinhAnh)
                        .ToList();

                    return View(laptopTheoDanhGia);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Lỗi trong LaptopBanChay: {ex.Message}");

            }

    
            return View("Error");
        }
        public ActionResult LaptopBanChayTheoSoLuongBan()
        {
            try
            {
                int idUser = 0;

                if (Session["TaiKhoan"] != null)
                {
                    var user = (KhachHang)Session["TaiKhoan"];
                    idUser = user.MaKH;
                    ViewBag.CoTK = idUser;

                    // Lấy danh sách sản phẩm laptop và sắp xếp theo số lượng bán ra giảm dần
                    var laptopBanChay = db.SanPhams
                        .Where(p => p.DanhMuc.TenDanhMuc.ToLower() == "Laptop" && p.TrangThai == 1)  // Thay đổi điều kiện danh mục tùy thuộc vào cấu trúc dữ liệu của bạn
                        .OrderByDescending(p => p.SoLuongDaBanRa)  // Sắp xếp theo số lượng bán ra giảm dần
                        .Take(3)  // Chọn 3 sản phẩm đầu tiên
                        .Include(s => s.DanhMuc)
                        .Include(s => s.HangSanXuat)
                        .Include(s => s.HinhAnh)
                        .ToList();

                    return View(laptopBanChay);
                }
                else
                {
                    ViewBag.CoTK = 0;

                    // Lấy danh sách sản phẩm laptop và sắp xếp theo số lượng bán ra giảm dần
                    var laptopBanChay = db.SanPhams
                        .Where(p => p.DanhMuc.TenDanhMuc.ToLower() == "Laptop" && p.TrangThai == 1)  // Thay đổi điều kiện danh mục tùy thuộc vào cấu trúc dữ liệu của bạn
                        .OrderByDescending(p => p.SoLuongDaBanRa)  // Sắp xếp theo số lượng bán ra giảm dần
                        .Take(3)  // Chọn 3 sản phẩm đầu tiên
                        .Include(s => s.DanhMuc)
                        .Include(s => s.HangSanXuat)
                        .Include(s => s.HinhAnh)
                        .ToList();

                    return View(laptopBanChay);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi trong LaptopBanChayTheoSoLuongBan: {ex.Message}");
            }

            return View("Error");
        }

        //Loa
        public ActionResult LoaBanChayTheoDanhGia()
        {
            try
            {
                int idUser = 0;

                if (Session["TaiKhoan"] != null)
                {
                    var user = (KhachHang)Session["TaiKhoan"];
                    idUser = user.MaKH;
                    ViewBag.CoTK = idUser;

                    var laptopBanChay = db.SanPhams
                        .Where(p => p.DanhMuc.TenDanhMuc.ToLower() == "Loa" && p.TrangThai == 1)  
                        .OrderByDescending(p => p.DanhGiaSanPhams.Count())  
                        .Take(3)  
                        .Include(s => s.DanhMuc)
                        .Include(s => s.HangSanXuat)
                        .Include(s => s.HinhAnh)
                        .ToList();

                    return View(laptopBanChay);
                }
                else
                {
                    ViewBag.CoTK = 0;

                    var laptopTheoDanhGia = db.SanPhams
                        .Where(p => p.DanhMuc.TenDanhMuc.ToLower() == "Loa" && p.TrangThai == 1)  
                        .OrderByDescending(p => p.DanhGiaSanPhams.Count())  
                        .Take(3)  
                        .Include(s => s.DanhMuc)
                        .Include(s => s.HangSanXuat)
                        .Include(s => s.HinhAnh)
                        .ToList();

                    return View(laptopTheoDanhGia);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi trong LaptopBanChay: {ex.Message}");
            }

            return View("Error");
        }
        public ActionResult LoaBanChayTheoSoLuongBan()
        {
            try
            {
                int idUser = 0;

                if (Session["TaiKhoan"] != null)
                {
                    var user = (KhachHang)Session["TaiKhoan"];
                    idUser = user.MaKH;
                    ViewBag.CoTK = idUser;

                    // Lấy danh sách sản phẩm laptop và sắp xếp theo số lượng bán ra giảm dần
                    var laptopBanChay = db.SanPhams
                        .Where(p => p.DanhMuc.TenDanhMuc.ToLower() == "Loa" && p.TrangThai == 1)  // Thay đổi điều kiện danh mục tùy thuộc vào cấu trúc dữ liệu của bạn
                        .OrderByDescending(p => p.SoLuongDaBanRa)  // Sắp xếp theo số lượng bán ra giảm dần
                        .Take(3)  // Chọn 3 sản phẩm đầu tiên
                        .Include(s => s.DanhMuc)
                        .Include(s => s.HangSanXuat)
                        .Include(s => s.HinhAnh)
                        .ToList();

                    return View(laptopBanChay);
                }
                else
                {
                    ViewBag.CoTK = 0;

                    // Lấy danh sách sản phẩm laptop và sắp xếp theo số lượng bán ra giảm dần
                    var laptopBanChay = db.SanPhams
                        .Where(p => p.DanhMuc.TenDanhMuc.ToLower() == "Loa" && p.TrangThai == 1)  // Thay đổi điều kiện danh mục tùy thuộc vào cấu trúc dữ liệu của bạn
                        .OrderByDescending(p => p.SoLuongDaBanRa)  // Sắp xếp theo số lượng bán ra giảm dần
                        .Take(3)  // Chọn 3 sản phẩm đầu tiên
                        .Include(s => s.DanhMuc)
                        .Include(s => s.HangSanXuat)
                        .Include(s => s.HinhAnh)
                        .ToList();

                    return View(laptopBanChay);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi trong LaptopBanChayTheoSoLuongBan: {ex.Message}");
            }

            return View("Error");
        }
        public ActionResult LienHe()
        {
            return View();
        }
      


    }
}
