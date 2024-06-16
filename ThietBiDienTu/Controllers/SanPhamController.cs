using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using PagedList;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ThietBiDienTu.Areas.Admin.Models;
using System.Web.UI;

namespace ThietBiDienTu.Controllers
{
    public class SanPhamController : Controller
    {
        private ThietBiDienTuEntities1 db = new ThietBiDienTuEntities1();
        //
        // GET: /SanPham/
        //Tìm kiếm sản phẩm
        //public List<SanPham> ListSanPham(string Keyword)
        //{
        //    return db.SanPhams.Where(x => x.TenSP.Contains(Keyword)).Select(x => x.TenSP).ToList();
        //}
        //public JsonResult ListSanPham(string q)
        //{
           
        //}
        public ActionResult Sidebar()
        {
            return View();
        }
        public ActionResult SanPham(int? page, string sortOrder)
        {
            int pageSize = 12;
            int pageNumber = page ?? 1;

            IQueryable<SanPham> productList;

            if (Session["TaiKhoan"] != null)
            {
                var user = (KhachHang)Session["TaiKhoan"];
                int userId = user.MaKH;

                // Lấy danh sách sp
                productList = db.SanPhams
                    .Include(x => x.DanhMuc)
                    .Include(x => x.HangSanXuat)
                    .Include(x => x.HinhAnh)
                    .Where(x => x.TrangThai == 1); // Chỉ lấy sp có TrangThai = 1

                // Sử dụng hàm sắp xếp
                productList = SapXep(productList, sortOrder);
                ViewBag.CoTK = userId;
            }
            else
            {
                // Chưa đăng nhập

                // Lấy danh sách sp có TrangThai = 1
                productList = db.SanPhams
                    .Where(x => x.TrangThai == 1)
                    .Include(x => x.DanhMuc)
                    .Include(x => x.HangSanXuat)
                    .Include(x => x.HinhAnh);

                // Sử dụng hàm sắp xếp
                productList = SapXep(productList, sortOrder);
                ViewBag.CoTK = 0;
            }

            // Phân trang
            var pagedProducts = productList.ToPagedList(pageNumber, pageSize);

            ViewBag.SortOrder = sortOrder; // Truyền giá trị sortOrder vào ViewBag

            return View(pagedProducts);
        }
        public IQueryable<SanPham> SapXep(IQueryable<SanPham> productList, string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "price_asc" ? "price_desc" : "price_asc";
            switch (sortOrder)
            {
                case "name_desc":
                    productList = productList.OrderBy(m => m.TenSP);
                    break;
                case "price_asc":
                    productList = productList.OrderBy(m => m.GiaTien);
                    break;
                case "price_desc":
                    productList = productList.OrderByDescending(m => m.GiaTien);
                    break;
                default:
                    productList = productList.OrderBy(m => m.TenSP);
                    break;
            }

            return productList;
        }
        public ActionResult BanChay()
        {
            if (Session["TaiKhoan"] != null)
            {

                var User = (KhachHang)Session["TaiKhoan"];

                int idUser = User.MaKH;

                ViewBag.CoTK = idUser;
                List<int> productIds = db.SanPhams

                    .OrderByDescending(p => p.SoLuongDaBanRa)
                    .Take(3)
                    .Select(p => p.MaSP)
                    .ToList();

                var sanpham = db.SanPhams
                                .Where(p => productIds.Contains(p.MaSP) && p.TrangThai == 1)
                                .Include(s => s.DanhMuc)
                                .Include(s => s.HangSanXuat)
                                .Include(s => s.HinhAnh)
                                .ToList();

                return View(sanpham);

            }
            else
            {
                ViewBag.CoTK = 0;
                List<int> productIds = db.SanPhams

                             .OrderByDescending(p => p.SoLuongDaBanRa)
                             .Take(3)
                             .Select(p => p.MaSP)
                             .ToList();

                var sanpham = db.SanPhams
                                .Where(p => productIds.Contains(p.MaSP) && p.TrangThai == 1)
                                .Include(s => s.DanhMuc)
                                .Include(s => s.HangSanXuat)
                                .Include(s => s.HinhAnh)
                                .ToList();

                return View(sanpham);

            }


        }
        public ActionResult SPBanChay()
        {
            if (Session["TaiKhoan"] != null)
            {

                var User = (KhachHang)Session["TaiKhoan"];

                int idUser = User.MaKH;

                ViewBag.CoTK = idUser;
                List<int> productIds = db.SanPhams

                    .OrderByDescending(p => p.SoLuongDaBanRa)
                    .Take(5)
                    .Select(p => p.MaSP)
                    .ToList();

                var sanpham = db.SanPhams
                                .Where(p => productIds.Contains(p.MaSP) && p.TrangThai == 1)
                                .Include(s => s.DanhMuc)
                                .Include(s => s.HangSanXuat)
                                .Include(s => s.HinhAnh)
                                .ToList();

                return View(sanpham);

            }
            else
            {
                ViewBag.CoTK = 0;
                List<int> productIds = db.SanPhams

                             .OrderByDescending(p => p.SoLuongDaBanRa)
                             .Take(5)
                             .Select(p => p.MaSP)
                             .ToList();

                var sanpham = db.SanPhams
                                .Where(p => productIds.Contains(p.MaSP) && p.TrangThai == 1)
                                .Include(s => s.DanhMuc)
                                .Include(s => s.HangSanXuat)
                                .Include(s => s.HinhAnh)
                                .ToList();

                return View(sanpham);

            }

        }

        [HttpPost]
        public ActionResult ThemSPVaoGio(int productId, int sl)
        {
            if (Session["TaiKhoan"] != null)
            {

                var User = (KhachHang)Session["TaiKhoan"];
                var result = db.AddToCart(User.MaKH, productId, sl);

                return RedirectToAction("SanPham");


            }
            else
            {
                return RedirectToAction("DangNhap", "TrangChu");
            }

        }


        [HttpGet]
        public ActionResult ChiTietSanPham(int? id)
        {
            Session["masplq"] = id;
            if (Session["TaiKhoan"] != null)
            {

                var User = (KhachHang)Session["TaiKhoan"];

                int idUser = User.MaKH;
                var danhgia = db.DanhGiaSanPhams.Where(x => x.MaSP == id && x.TrangThai == 1);
                ViewBag.sl = danhgia.Count();



                ViewBag.CoTK = idUser;
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                SanPham sanpham = db.SanPhams.Find(id);

                if (sanpham == null)
                {
                    return HttpNotFound();
                }

                HinhAnh hinhanh = db.HinhAnhs.Find(sanpham.MaHinhAnh);
                List<layDanhGia_Result> danhGias = db.layDanhGia(id).ToList();
                SanPhamViewModels viewModel = new SanPhamViewModels
                {
                    SanPham = sanpham,
                    HinhAnh = hinhanh,
                    DanhGias = danhGias
                };

                return View(viewModel);
            }
            else
            {
                ViewBag.CoTK = 0;
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                SanPham sanpham = db.SanPhams.Find(id);
                var danhgia = db.DanhGiaSanPhams.Where(x => x.MaSP == id && x.TrangThai==1);
                ViewBag.sl = danhgia.Count();
                if (sanpham == null)
                {
                    return HttpNotFound();
                }

                HinhAnh hinhanh = db.HinhAnhs.Find(sanpham.MaHinhAnh);
                List<layDanhGia_Result> danhGias = db.layDanhGia(id).ToList();
                SanPhamViewModels viewModel = new SanPhamViewModels
                {
                    SanPham = sanpham,
                    HinhAnh = hinhanh,
                    DanhGias = danhGias
                };

                return View(viewModel);
            }



        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChiTietSanPham(int maSP, string noiDung, int soSao)
        {
            Session["masplq"] = maSP;
            if (Session["TaiKhoan"] != null)
            {
                var user = (KhachHang)Session["TaiKhoan"];
                int maKH = user.MaKH;

                try
                {

                    string result = db.danhGia(maKH, maSP, noiDung, soSao).ToString();
                    

                    return RedirectToAction("ChiTietSanPham", new { id = maSP });
                }
                catch (Exception ex)
                {
                    // Log lỗi
                    return View("Lỗi");
                }
            }
            else
            {
                return RedirectToAction("DangNhap");
            }
        }
        public ActionResult SpLienQuan()
        {
            int id = (int)Session["masplq"];
            
                

                try
                {
                    // Lấy thông tin sản phẩm hiện tại
                    var sanPhamHienTai = db.SanPhams
                        .Where(x => x.MaSP == id && x.TrangThai == 1)
                        .Include(x => x.DanhMuc)
                        .Include(x => x.HangSanXuat)
                        .Include(x => x.HinhAnh)
                        .FirstOrDefault();

                    if (sanPhamHienTai != null)
                    {

                    var sanPhamLienQuan = db.SanPhams
                    .Where(x =>
                        x.MaSP != id &&
                        (x.DanhMuc.MaDanhMuc == sanPhamHienTai.DanhMuc.MaDanhMuc ||
                         x.HangSanXuat.MaHang == sanPhamHienTai.HangSanXuat.MaHang) &&
                         x.MaSP != id
                    )
                    .Include(x => x.DanhMuc)
                    .Include(x => x.HangSanXuat)
                    .Include(x => x.HinhAnh)
                    .Take(4)
                    .ToList();


                    return View(sanPhamLienQuan);
                    }
                    else
                    {
                        // Sản phẩm không tồn tại
                        return View("Sản phẩm không tồn tại");
                    }
                }
                catch (Exception ex)
                {
                    // Log lỗi
                    return View("Lỗi");
                }
            
        }


        // GET: /DanhMucSanPham/
        public ActionResult DanhMuc()
        {
            var DsDanhMuc = db.DanhMucs.ToList();
            return View(DsDanhMuc);
        }
        public ActionResult DanhMucSanPham(int? page, int madanhmuc)
        {
            int pageSize = 9;
            int pageNumber = page ?? 1;

            var DsDanhMucSanPham = db.SanPhams.Where(n => n.MaDanhMuc == madanhmuc);

            // Paginate the data
            var pagedDsDanhMucSanPham = DsDanhMucSanPham.OrderBy(s => s.TenSP).ToPagedList(pageNumber, pageSize);

            return View(pagedDsDanhMucSanPham);
            //var DsDanhMucSanPham = db.SanPhams.Where(n => n.MaDanhMuc == madanhmuc).ToList();
            //return View(DsDanhMucSanPham);
        }
        //Sản phẩm theo hãng sản xuất
        public ActionResult LoaiSanPham()
        {
            var DsHangSanXuat = db.HangSanXuats.ToList();
            return View(DsHangSanXuat);
        }
        public ActionResult SanPhamTheoLoai (int? page, int maloai)
        {
            int pageSize = 9;
            int pageNumber = page ?? 1;

            var lstSanPham = db.SanPhams.Where(x => x.MaHang == maloai).OrderBy(x => x.TenSP);

            // Paginate the data
            var pagedSanPham = lstSanPham.ToPagedList(pageNumber, pageSize);

            return View(pagedSanPham);
        }
        

    }
}
