using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ThietBiDienTu.Areas.Admin.Models;

namespace ThietBiDienTu.Areas.Admin.Controllers
{
    public class NhanVienController : Controller
    {
        private ThietBiDienTuEntities1 db = new ThietBiDienTuEntities1();

        // GET: /Admin/NhanVien/
        public ActionResult Index()
        {
            if (Session["TaiKhoanAD"] != null)
            {

                var nhanviens = db.NhanViens.Include(n => n.Quyen);
                return View(nhanviens.ToList());
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }

        // GET: /Admin/NhanVien/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                NhanVien nhanvien = db.NhanViens.Find(id);
                if (nhanvien == null)
                {
                    return HttpNotFound();
                }
                return View(nhanvien);
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }

        // GET: /Admin/NhanVien/Create
        public ActionResult Create()
        {
            if (Session["TaiKhoanAD"] != null)
            {

                ViewBag.MaQuyen = new SelectList(db.Quyens, "MaQuyen", "TenQuyen");
                return View();
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }

        // POST: /Admin/NhanVien/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaAdmin,TenTaiKhoanAdmin,MatKhauAdmin,HoTenAdmin,AnhAdmin,MaQuyen")] NhanVien nhanvien)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                if (ModelState.IsValid)
                {
                    nhanvien.AnhAdmin = "data:image/png;base64,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";
                    db.NhanViens.Add(nhanvien);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.MaQuyen = new SelectList(db.Quyens, "MaQuyen", "TenQuyen", nhanvien.MaQuyen);
                return View(nhanvien);
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }

        // GET: /Admin/NhanVien/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                NhanVien nhanvien = db.NhanViens.Find(id);
                if (nhanvien == null)
                {
                    return HttpNotFound();
                }
                ViewBag.MaQuyen = new SelectList(db.Quyens, "MaQuyen", "TenQuyen", nhanvien.MaQuyen);
                return View(nhanvien);
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }

        // POST: /Admin/NhanVien/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaAdmin,TenTaiKhoanAdmin,MatKhauAdmin,HoTenAdmin,AnhAdmin,MaQuyen")] NhanVien nhanvien)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                if (ModelState.IsValid)
                {
                    db.Entry(nhanvien).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.MaQuyen = new SelectList(db.Quyens, "MaQuyen", "TenQuyen", nhanvien.MaQuyen);
                return View(nhanvien);
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }

        // GET: /Admin/NhanVien/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                NhanVien nhanvien = db.NhanViens.Find(id);
                if (nhanvien == null)
                {
                    return HttpNotFound();
                }
                return View(nhanvien);
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }

        // POST: /Admin/NhanVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                NhanVien nhanvien = db.NhanViens.Find(id);
                db.NhanViens.Remove(nhanvien);
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
