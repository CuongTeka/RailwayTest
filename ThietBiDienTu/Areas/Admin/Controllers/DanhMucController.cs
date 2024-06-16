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
    public class DanhMucController : Controller
    {
        private ThietBiDienTuEntities1 db = new ThietBiDienTuEntities1();

        // GET: /Admin/DanhMuc/
        public ActionResult Index()
        {
            if (Session["TaiKhoanAD"] != null)
            {

                return View(db.DanhMucs.ToList());
            }
            else
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }

        }

        // GET: /Admin/DanhMuc/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DanhMuc danhmuc = db.DanhMucs.Find(id);
                if (danhmuc == null)
                {
                    return HttpNotFound();
                }
                return View(danhmuc);
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }

        // GET: /Admin/DanhMuc/Create
        public ActionResult Create()
        {
            if (Session["TaiKhoanAD"] != null)
            {

                return View();
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }

        // POST: /Admin/DanhMuc/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDanhMuc,TenDanhMuc")] DanhMuc danhmuc)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                if (ModelState.IsValid)
                {
                    db.DanhMucs.Add(danhmuc);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(danhmuc);
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }

        // GET: /Admin/DanhMuc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DanhMuc danhmuc = db.DanhMucs.Find(id);
                if (danhmuc == null)
                {
                    return HttpNotFound();
                }
                return View(danhmuc);
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }

        // POST: /Admin/DanhMuc/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDanhMuc,TenDanhMuc")] DanhMuc danhmuc)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                if (ModelState.IsValid)
                {
                    db.Entry(danhmuc).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(danhmuc);
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }

        // GET: /Admin/DanhMuc/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DanhMuc danhmuc = db.DanhMucs.Find(id);
                if (danhmuc == null)
                {
                    return HttpNotFound();
                }
                return View(danhmuc);
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }

        // POST: /Admin/DanhMuc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                DanhMuc danhmuc = db.DanhMucs.Find(id);
                db.DanhMucs.Remove(danhmuc);
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
