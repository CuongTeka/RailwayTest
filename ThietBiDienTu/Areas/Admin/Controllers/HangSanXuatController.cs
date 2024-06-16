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
    public class HangSanXuatController : Controller
    {
        private ThietBiDienTuEntities1 db = new ThietBiDienTuEntities1();

        // GET: /Admin/HangSanXuat/
        public ActionResult Index()
        {
            if (Session["TaiKhoanAD"] != null)
            {

                return View(db.HangSanXuats.ToList());
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }

        // GET: /Admin/HangSanXuat/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                HangSanXuat HangSanXuat = db.HangSanXuats.Find(id);
                if (HangSanXuat == null)
                {
                    return HttpNotFound();
                }
                return View(HangSanXuat);
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }

        // GET: /Admin/HangSanXuat/Create
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

        // POST: /Admin/HangSanXuat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHang,TenHang,TTLienHe,MoTa")] HangSanXuat HangSanXuat)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                if (ModelState.IsValid)
                {
                   
                    db.HangSanXuats.Add(HangSanXuat);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(HangSanXuat);
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }

        // GET: /Admin/HangSanXuat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                HangSanXuat HangSanXuat = db.HangSanXuats.Find(id);
                if (HangSanXuat == null)
                {
                    return HttpNotFound();
                }
                return View(HangSanXuat);
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

        }

        // POST: /Admin/HangSanXuat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHang,TenHang,TTLienHe,MoTa")] HangSanXuat HangSanXuat)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                if (ModelState.IsValid)
                {
                    db.Entry(HangSanXuat).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(HangSanXuat);
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }

        // GET: /Admin/HangSanXuat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                HangSanXuat HangSanXuat = db.HangSanXuats.Find(id);
                if (HangSanXuat == null)
                {
                    return HttpNotFound();
                }
                return View(HangSanXuat);
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }

        // POST: /Admin/HangSanXuat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                HangSanXuat HangSanXuat = db.HangSanXuats.Find(id);
                db.HangSanXuats.Remove(HangSanXuat);
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
