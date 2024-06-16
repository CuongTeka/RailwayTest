using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using PagedList;
using System.Web.Mvc;
using ThietBiDienTu.Areas.Admin.Models;
using System.IO;

namespace ThietBiDienTu.Areas.Admin.Controllers
{
    public class SanPhamAdminController : Controller
    {
        private ThietBiDienTuEntities1 db = new ThietBiDienTuEntities1();

        // GET: /Admin/SanPham/
        public ActionResult Index(int? page)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                int pageSize = 12; // Số lượng sản phẩm muốn hiển thị trong 1 trang
                int pageNumber = page ?? 1;

                var sanpham = db.SanPhams
                    .Include(s => s.BaoHanhs)
                    .Include(s => s.DanhMuc)
                    .Include(s => s.HinhAnh)
                    .Include(s => s.HangSanXuat)
                    .OrderBy(s => s.TenSP); // Sắp xếp theo tên sản phẩm hoặc thuộc tính bạn muốn

                var pagedSanPham = sanpham.ToPagedList(pageNumber, pageSize);

                return View(pagedSanPham);

                //return View(paginatedSanPham);
                //var sanpham = db.SanPhams.Include(s => s.BaoHanhs).Include(s => s.DanhMuc).Include(s => s.HinhAnh).Include(s => s.HangSanXuat);
                //return View(sanpham.ToList());
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }
          
        }
        public ActionResult RenderIndex()
        {
            if (Session["TaiKhoanAD"] != null)
            {

                var sanpham = db.SanPhams.Include(s => s.BaoHanhs).Include(s => s.DanhMuc).Include(s => s.HinhAnh).Include(s => s.HangSanXuat);
                return View(sanpham.ToList());
            }
            else
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }
            
        }
        public ActionResult Render()
        {
            if (Session["TaiKhoanAD"] != null)
            {
                var sanpham = db.SanPhams.Include(s => s.BaoHanhs).Include(s => s.DanhMuc).Include(s => s.HinhAnh).Include(s => s.HangSanXuat);
                return View("RenderIndex", sanpham.ToList());

            }
            else
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }
            
        }


        // GET: /Admin/SanPham/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                SanPham sanpham = db.SanPhams.Find(id);
                if (sanpham == null)
                {
                    return HttpNotFound();
                }
                return View(sanpham);
            }
            else
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }
            
        }

        // GET: /Admin/SanPham/Create
        public ActionResult Create()
        {
            if (Session["TaiKhoanAD"] != null)
            {

                ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc");

                ViewBag.MaHang = new SelectList(db.HangSanXuats, "MaHang", "TenHang");


                return View();
            }
            else
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }
            
        }

        // POST: /Admin/SanPham/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult Create(SanPhamViewModels viewModel, IEnumerable<HttpPostedFileBase> filec, IEnumerable<HttpPostedFileBase> filepu1, IEnumerable<HttpPostedFileBase> filepu2, IEnumerable<HttpPostedFileBase> filepu3)
        {
            if (Session["TaiKhoanAD"] != null)
            {
                var spc = db.SanPhams.Where(x => x.TenSP == viewModel.SanPham.TenSP);
                
                if (spc.Any())
                {
                    ModelState.AddModelError("SP", "Sản phẩm đã tồn tại");
                    ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", viewModel.SanPham.MaDanhMuc);
                    ViewBag.MaHang = new SelectList(db.HangSanXuats, "MaHang", "TenHang", viewModel.SanPham.MaHang);

                    return View();
                }

                ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", viewModel.SanPham.MaDanhMuc);
                ViewBag.MaHang = new SelectList(db.HangSanXuats, "MaHang", "TenHang", viewModel.SanPham.MaHang);

                foreach (var file in filec)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        // Upload file lên server
                        string fileName1 = Path.GetFileName(file.FileName);

                        viewModel.HinhAnh.LinkHinhAnhChinh = fileName1;


                    }
                }
                foreach (var file in filepu1)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        // Upload file lên server
                        string fileName1 = Path.GetFileName(file.FileName);

                        viewModel.HinhAnh.LinkHinhAnhPhu1 = fileName1;


                    }
                }
                foreach (var file in filepu2)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        // Upload file lên server
                        string fileName1 = Path.GetFileName(file.FileName);

                        viewModel.HinhAnh.LinkHinhAnhPhu2 = fileName1;


                    }
                }
                foreach (var file in filepu3)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        // Upload file lên server
                        string fileName1 = Path.GetFileName(file.FileName);

                        viewModel.HinhAnh.LinkHinhAnhPhu3 = fileName1;


                    }
                }

                if (ModelState.IsValid)
                {
                    SanPham sp = new SanPham
                    {

                        TenSP = viewModel.SanPham.TenSP,
                        GiaGoc = viewModel.SanPham.GiaGoc,
                        UuDai = viewModel.SanPham.UuDai,
                        SoLuongTon = viewModel.SanPham.SoLuongTon,
                        MaDanhMuc = viewModel.SanPham.MaDanhMuc,
                        MoTa = viewModel.SanPham.MoTa,
                        ThongSoKyThuat = viewModel.SanPham.ThongSoKyThuat,
                        MaHang = viewModel.SanPham.MaHang,
                        SoLuongDaBanRa = 0,
                        ChinhSachBaoHanh = viewModel.SanPham.ChinhSachBaoHanh,
                        NgayTao = DateTime.Now,
                        ThoiGianBaoHanh = viewModel.SanPham.ThoiGianBaoHanh,
                        TrangThai=1,
                        MaHinhAnh = 1
                    };

                    db.SanPhams.Add(sp);
                    db.SaveChanges();

                    HinhAnh ha = new HinhAnh
                    {
                        LinkHinhAnhChinh = viewModel.HinhAnh.LinkHinhAnhChinh,
                        LinkHinhAnhPhu1 = viewModel.HinhAnh.LinkHinhAnhPhu1,
                        LinkHinhAnhPhu2 = viewModel.HinhAnh.LinkHinhAnhPhu2,
                        LinkHinhAnhPhu3 = viewModel.HinhAnh.LinkHinhAnhPhu3
                    };
                    db.HinhAnhs.Add(ha);
                    db.SaveChanges();

                    sp.MaHinhAnh = ha.MaHinhAnh;

                    db.SaveChanges();
                    return RedirectToAction("Index");

                }


                return View();
            }
            else
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }

            
        }

        // GET: /Admin/SanPham/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (Session["TaiKhoanAD"] != null)
            {
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
                SanPhamViewModels viewModel = new SanPhamViewModels
                {
                    SanPham = sanpham,
                    HinhAnh = hinhanh
                };
                viewModel.SanPham.MaHinhAnh = hinhanh.MaHinhAnh; // Đảm bảo gán giá trị MaHinhAnh vào trường ẩn




                ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", sanpham.MaDanhMuc);
                ViewBag.MaHinhAnh = new SelectList(db.HinhAnhs, "LinkHinhAnhChinh", "LinkHinhAnhChinh", sanpham.MaHinhAnh);
                ViewBag.MaHang = new SelectList(db.HangSanXuats, "MaHang", "TenHang", sanpham.MaHang);
                ViewBag.MaHinhAnhPhu1 = new SelectList(db.HinhAnhs, "LinkHinhAnhPhu1", "LinkHinhAnhPhu1", hinhanh.LinkHinhAnhPhu1);
                ViewBag.MaHinhAnhPhu2 = new SelectList(db.HinhAnhs, "LinkHinhAnhPhu2", "LinkHinhAnhPhu2", hinhanh.LinkHinhAnhPhu2);
                ViewBag.MaHinhAnhPhu3 = new SelectList(db.HinhAnhs, "LinkHinhAnhPhu3", "LinkHinhAnhPhu3", hinhanh.LinkHinhAnhPhu3);

                return View(viewModel);

            }
            else
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }
            
        }




        // POST: /Admin/SanPham/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SanPhamViewModels viewModel, IEnumerable<HttpPostedFileBase> files, IEnumerable<HttpPostedFileBase> filep1, IEnumerable<HttpPostedFileBase> filep2, IEnumerable<HttpPostedFileBase> filep3)
        {
            if (Session["TaiKhoanAD"] != null)
            {
                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        // Upload file lên server
                        string fileName1 = Path.GetFileName(file.FileName);
                        
                        viewModel.HinhAnh.LinkHinhAnhChinh =  fileName1;
                        

                    }
                }
                foreach (var file in filep1)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        // Upload file lên server
                        string fileName1 = Path.GetFileName(file.FileName);

                        viewModel.HinhAnh.LinkHinhAnhPhu1 = fileName1;


                    }
                }
                foreach (var file in filep2)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        // Upload file lên server
                        string fileName1 = Path.GetFileName(file.FileName);

                        viewModel.HinhAnh.LinkHinhAnhPhu2 = fileName1;


                    }
                }
                foreach (var file in filep3)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        // Upload file lên server
                        string fileName1 = Path.GetFileName(file.FileName);

                        viewModel.HinhAnh.LinkHinhAnhPhu3 = fileName1;


                    }
                }
                if (ModelState.IsValid)
                {
                    SanPham sp = new SanPham
                    {
                        MaSP = viewModel.SanPham.MaSP,
                        TenSP = viewModel.SanPham.TenSP,
                        GiaGoc = viewModel.SanPham.GiaGoc,
                        UuDai=viewModel.SanPham.UuDai,
                        SoLuongTon = viewModel.SanPham.SoLuongTon,
                        MaDanhMuc = viewModel.SanPham.MaDanhMuc,
                        MoTa = viewModel.SanPham.MoTa,
                        ThongSoKyThuat = viewModel.SanPham.ThongSoKyThuat,
                        MaHang = viewModel.SanPham.MaHang,
                        TrangThai=viewModel.SanPham.TrangThai,
                        ChinhSachBaoHanh = viewModel.SanPham.ChinhSachBaoHanh,
                        NgayTao = viewModel.SanPham.NgayTao,
                        ThoiGianBaoHanh = viewModel.SanPham.ThoiGianBaoHanh,
                        SoLuongDaBanRa = viewModel.SanPham.SoLuongDaBanRa,
                        MaHinhAnh = viewModel.SanPham.MaHinhAnh
                    };

                    db.Entry(sp).State = EntityState.Modified;
                    db.SaveChanges();

                    HinhAnh ha = db.HinhAnhs.Find(viewModel.SanPham.MaHinhAnh);
                    ha.LinkHinhAnhChinh = viewModel.HinhAnh.LinkHinhAnhChinh;
                    ha.LinkHinhAnhPhu1 = viewModel.HinhAnh.LinkHinhAnhPhu1;
                    ha.LinkHinhAnhPhu2 = viewModel.HinhAnh.LinkHinhAnhPhu2;
                    ha.LinkHinhAnhPhu3 = viewModel.HinhAnh.LinkHinhAnhPhu3;

                    db.Entry(ha).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", viewModel.SanPham.MaDanhMuc);
                ViewBag.MaHang = new SelectList(db.HangSanXuats, "MaHang", "TenHang", viewModel.SanPham.MaHang);

                return View(viewModel);

            }
            else
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }
            
        }



        // GET: /Admin/SanPham/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["TaiKhoanAD"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                SanPham sanpham = db.SanPhams.Find(id);
                if (sanpham == null)
                {
                    return HttpNotFound();
                }
                return View(sanpham);

            }
            else
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }
            
        }

        // POST: /Admin/SanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["TaiKhoanAD"] != null)
            {
                SanPham sanpham = db.SanPhams.Find(id);
                HinhAnh hinhanh = db.HinhAnhs.Find(sanpham.MaHinhAnh);
                db.SanPhams.Remove(sanpham);
                db.HinhAnhs.Remove(hinhanh);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
