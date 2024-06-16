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
    public class MaGiamGiaAdminController : Controller
    {
        private ThietBiDienTuEntities1 db = new ThietBiDienTuEntities1();

        // GET: /Admin/MaGiamGiaAdmin/
        public ActionResult Index()
        {
            if (Session["TaiKhoanAD"] != null)
            {

                return View(db.MaGiamGias.ToList());
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }
        public ActionResult AnMGG(string mgg)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                var magiamgia = db.MaGiamGias.FirstOrDefault(m => m.MaGiamGia1 == mgg);

                if (magiamgia != null)
                {

                    magiamgia.TrangThai = 3;


                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {

                    return RedirectToAction("Index");
                }
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }
        public ActionResult CapNhatSK(string mgg)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                var magiamgia1To2 = db.MaGiamGias.Where(m => m.TrangThai == 1);
                foreach (var item in magiamgia1To2)
                {
                    item.TrangThai = 2;
                }


                var magiamgiaMGG = db.MaGiamGias.FirstOrDefault(m => m.MaGiamGia1 == mgg);
                if (magiamgiaMGG != null)
                {
                    magiamgiaMGG.TrangThai = 1;
                }


                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }
        public ActionResult Cho(string mgg)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                var magiamgiaMGG = db.MaGiamGias.FirstOrDefault(m => m.MaGiamGia1 == mgg);
                magiamgiaMGG.TrangThai = 2;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }


        // GET: /Admin/MaGiamGiaAdmin/Details/5
        public ActionResult Details(string id)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                MaGiamGia magiamgia = db.MaGiamGias.Find(id);
                if (magiamgia == null)
                {
                    return HttpNotFound();
                }
                return View(magiamgia);
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }

        // GET: /Admin/MaGiamGiaAdmin/Create
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

        // POST: /Admin/MaGiamGiaAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaGiamGia1,TenMGG,SLDaSuDung,NgayBatDau,NgayKetThuc,TiLeGiam")] MaGiamGia magiamgia)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                if (ModelState.IsValid)
                {
                    if(magiamgia.NgayKetThuc<magiamgia.NgayBatDau)
                    {
                        ModelState.AddModelError("mkktt", "Ngày kết thúc không hợp lệ");
                        
                        return View();
                    }
                    magiamgia.TrangThai = 2;
                    db.MaGiamGias.Add(magiamgia);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(magiamgia);
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }

        // GET: /Admin/MaGiamGiaAdmin/Edit/5
        public ActionResult Edit(string id)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                MaGiamGia magiamgia = db.MaGiamGias.Find(id);
                if (magiamgia == null)
                {
                    return HttpNotFound();
                }
                return View(magiamgia);
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }

        // POST: /Admin/MaGiamGiaAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaGiamGia1,TenMGG,SLDaSuDung,NgayBatDau,NgayKetThuc,TiLeGiam")] MaGiamGia magiamgia)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                if (ModelState.IsValid)
                {
                    db.Entry(magiamgia).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(magiamgia);
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }

        // GET: /Admin/MaGiamGiaAdmin/Delete/5
        public ActionResult Delete(string id)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                MaGiamGia magiamgia = db.MaGiamGias.Find(id);
                if (magiamgia == null)
                {
                    return HttpNotFound();
                }
                return View(magiamgia);
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }

        // POST: /Admin/MaGiamGiaAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (Session["TaiKhoanAD"] != null)
            {

                MaGiamGia magiamgia = db.MaGiamGias.Find(id);
                db.MaGiamGias.Remove(magiamgia);
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
