using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ThietBiDienTu.Areas.Admin.Models;

namespace ThietBiDienTu.Controllers
{
    public class BaoHanhSPController : Controller
    {
        private ThietBiDienTuEntities1 db = new ThietBiDienTuEntities1();

        // GET: /BaoHanh/
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] != null)
            {

                var taiKhoan = (KhachHang)Session["TaiKhoan"];

                var baohanh = db.sp_HienThiBaoHanh(taiKhoan.MaKH);
                return View(baohanh.ToList());
            }
            else
            {
                ViewBag.CoTK = 0;

                return RedirectToAction("DangNhap", "TrangChu");
            }

        }
        public ActionResult YeuCauBH()
        {
            if (Session["TaiKhoan"] != null)
            {

                var taiKhoan = (KhachHang)Session["TaiKhoan"];

                var baohanh = db.sp_HienThiYeuCauBHTheoMaKH(taiKhoan.MaKH);
                return View(baohanh.ToList());
            }
            else
            {
                ViewBag.CoTK = 0;

                return RedirectToAction("DangNhap", "TrangChu");
            }
        }
        public ActionResult XemChiTietBaoHanh(int MaBH)
        {
            var bh = db.sp_HienThiBaoHanhTheoMaBH(MaBH).FirstOrDefault();
            return View(bh);
        }
        public ActionResult TaoYeuCau(int MaBH)
        {
            return View();
        }
        [HttpPost]
        public ActionResult TaoYeuCau(TaoYeuCauViewModel yc)
        {
            if (Session["TaiKhoan"] != null)
            {

                var taiKhoan = (KhachHang)Session["TaiKhoan"];

                db.sp_TaoYeuCauBH(yc.MaBH, yc.LyDo);
                return RedirectToAction("YeuCauBH", "BaoHanhSP");
            }
            else
            {
                ViewBag.CoTK = 0;

                return RedirectToAction("DangNhap", "TrangChu");
            }
        }

        // GET: /BaoHanh/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaoHanh baohanh = db.BaoHanhs.Find(id);
            if (baohanh == null)
            {
                return HttpNotFound();
            }
            return View(baohanh);
        }

        // GET: /BaoHanh/Create
        public ActionResult Create()
        {
            ViewBag.MaDDH = new SelectList(db.DDHs, "MaDDH", "MaGiamGia");
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH");
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");
            return View();
        }

        // POST: /BaoHanh/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaBH,MaDDH,MaSP,NgayDH,MaKH,TenKH,SDT,LyDo,TrangThai")] BaoHanh baohanh)
        {
            if (ModelState.IsValid)
            {
                db.BaoHanhs.Add(baohanh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDDH = new SelectList(db.DDHs, "MaDDH", "MaGiamGia", baohanh.MaDDH);
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", baohanh.MaKH);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", baohanh.MaSP);
            return View(baohanh);
        }

        // GET: /BaoHanh/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaoHanh baohanh = db.BaoHanhs.Find(id);
            if (baohanh == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDDH = new SelectList(db.DDHs, "MaDDH", "MaGiamGia", baohanh.MaDDH);
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", baohanh.MaKH);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", baohanh.MaSP);
            return View(baohanh);
        }

        // POST: /BaoHanh/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaBH,MaDDH,MaSP,NgayDH,MaKH,TenKH,SDT,LyDo,TrangThai")] BaoHanh baohanh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(baohanh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDDH = new SelectList(db.DDHs, "MaDDH", "MaGiamGia", baohanh.MaDDH);
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", baohanh.MaKH);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", baohanh.MaSP);
            return View(baohanh);
        }

        // GET: /BaoHanh/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaoHanh baohanh = db.BaoHanhs.Find(id);
            if (baohanh == null)
            {
                return HttpNotFound();
            }
            return View(baohanh);
        }

        // POST: /BaoHanh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BaoHanh baohanh = db.BaoHanhs.Find(id);
            db.BaoHanhs.Remove(baohanh);
            db.SaveChanges();
            return RedirectToAction("Index");
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
