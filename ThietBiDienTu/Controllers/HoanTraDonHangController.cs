using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using ThietBiDienTu.Areas.Admin.Models;

namespace ThietBiDienTu.Controllers
{
    public class HoanTraDonHangController : Controller
    {
        private ThietBiDienTuEntities1 db = new ThietBiDienTuEntities1();

        // GET: /HoanTraDH/
        public ActionResult HoanTraDonHang()
        {
            if (Session["TaiKhoan"] != null)
            {
                var User = (KhachHang)Session["TaiKhoan"];
                int idUser = User.MaKH;

                var hoantra = db.GetHoanHangByMaKH(idUser);
                return View(hoantra.ToList());
            }
            else
            {
                // Nếu session không tồn tại, có thể chuyển hướng đến trang đăng nhập hoặc thực hiện xử lý khác
                return RedirectToAction("DangNhap", "TrangChu"); // Thay "TenController" bằng tên của controller đăng nhập
            }
        }
        public ActionResult YeuCauHoanTra()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeuCauHoanTra(int MaDDH, String Lydo)
        {
            if (Session["TaiKhoan"] != null)
            {
                var User = (KhachHang)Session["TaiKhoan"];
                int idUser = User.MaKH;

                db.InsertHoanTraDonHang(MaDDH, idUser, Lydo);
                return RedirectToAction("HoanTraDonHang");
            }
            else
            {
                // Nếu session không tồn tại, có thể chuyển hướng đến trang đăng nhập hoặc thực hiện xử lý khác
                return RedirectToAction("DangNhap", "TrangChu"); // Thay "TenController" bằng tên của controller đăng nhập
            }
        }
        public ActionResult XemCTHoanTra(int maDDH)
        {

            if (Session["TaiKhoan"] != null)
            {
                var User = (KhachHang)Session["TaiKhoan"];
                int idUser = User.MaKH;

                var sp=db.GetOrderInfoByMaDDH(maDDH);
                return View(sp.ToList());
            }
            else
            {
                // Nếu session không tồn tại, có thể chuyển hướng đến trang đăng nhập hoặc thực hiện xử lý khác
                return RedirectToAction("DangNhap", "TrangChu"); // Thay "TenController" bằng tên của controller đăng nhập
            }
        }

        // GET: /HoanTraDH/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoanHang hoanhang = db.HoanHangs.Find(id);
            if (hoanhang == null)
            {
                return HttpNotFound();
            }
            return View(hoanhang);
        }

        // GET: /HoanTraDH/Create
        public ActionResult Create()
        {
            ViewBag.MaDDH = new SelectList(db.DDHs, "MaDDH", "MaGiamGia");
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH");
            return View();
        }

        // POST: /HoanTraDH/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHoanHang,MaDDH,MaKH,LyDonHoanHang,TrangThai")] HoanHang hoanhang)
        {
            if (ModelState.IsValid)
            {
                db.HoanHangs.Add(hoanhang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDDH = new SelectList(db.DDHs, "MaDDH", "MaGiamGia", hoanhang.MaDDH);
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", hoanhang.MaKH);
            return View(hoanhang);
        }

        // GET: /HoanTraDH/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoanHang hoanhang = db.HoanHangs.Find(id);
            if (hoanhang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDDH = new SelectList(db.DDHs, "MaDDH", "MaGiamGia", hoanhang.MaDDH);
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", hoanhang.MaKH);
            return View(hoanhang);
        }

        // POST: /HoanTraDH/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHoanHang,MaDDH,MaKH,LyDonHoanHang,TrangThai")] HoanHang hoanhang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoanhang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDDH = new SelectList(db.DDHs, "MaDDH", "MaGiamGia", hoanhang.MaDDH);
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", hoanhang.MaKH);
            return View(hoanhang);
        }

        // GET: /HoanTraDH/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoanHang hoanhang = db.HoanHangs.Find(id);
            if (hoanhang == null)
            {
                return HttpNotFound();
            }
            return View(hoanhang);
        }

        // POST: /HoanTraDH/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HoanHang hoanhang = db.HoanHangs.Find(id);
            db.HoanHangs.Remove(hoanhang);
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

