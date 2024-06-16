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
    public class TKKhachHangController : Controller
    {
        private ThietBiDienTuEntities1 db = new ThietBiDienTuEntities1();

        // GET: /TKKhachHang/
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] != null)
            {



                var User = (KhachHang)Session["TaiKhoan"];
                int idUser = User.MaKH;
                var khachhang = db.KhachHangs.Where(x => x.MaKH == idUser);
                return View(khachhang.ToList());
            }
            else
            {

                return RedirectToAction("DangNhap", "TrangChu");
            }

        }

        public ActionResult DSMaGG()
        {
            if (Session["TaiKhoan"] != null)
            {
                var User = (KhachHang)Session["TaiKhoan"];
                int idUser = User.MaKH;
                var mgg = db.GetMaGiamGiaInfoForKhachHang(idUser);
                return View(mgg.ToList());
            }
            else
            {

                return RedirectToAction("DangNhap", "TrangChu");
            }
        }

        // GET: /TKKhachHang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachhang = db.KhachHangs.Find(id);
            if (khachhang == null)
            {
                return HttpNotFound();
            }
            return View(khachhang);
        }

        // GET: /TKKhachHang/Create
        public ActionResult Create()
        {
            ViewBag.MaQuyen = new SelectList(db.Quyens, "MaQuyen", "TenQuyen");
            return View();
        }

        // POST: /TKKhachHang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKH,TenKH,TenTKKH,MKTKKH,AnhKH,SoDienThoaiKH,MaQuyen,Email")] KhachHang khachhang)
        {
            if (ModelState.IsValid)
            {
                db.KhachHangs.Add(khachhang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaQuyen = new SelectList(db.Quyens, "MaQuyen", "TenQuyen", khachhang.MaQuyen);
            return View(khachhang);
        }

        // GET: /TKKhachHang/Edit/5
        public ActionResult Edit()
        {
            if (Session["TaiKhoan"] != null)
            {
                var User = (KhachHang)Session["TaiKhoan"];
                int id = User.MaKH;

                var dh = db.DDHs.Where(x => x.MaKH == id && x.TrangThai == 3).ToList();

                int TongSoTien = 0;
                if (dh is null)
                {
                    TongSoTien = 0;
                    
                }
                else
                {
                    foreach (var donHang in dh)
                    {
                        if (donHang.ThanhTien.HasValue)
                        {
                            TongSoTien += (int)donHang.ThanhTien;
                        }
                    }

                }
                ViewBag.TongSo = TongSoTien;
                ViewBag.TongSoHD=dh.Count();


                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                KhachHang khachhang = db.KhachHangs.Find(id);
                if (khachhang == null)
                {
                    return HttpNotFound();
                }
                ViewBag.MaQuyen = new SelectList(db.Quyens, "MaQuyen", "TenQuyen", khachhang.MaQuyen);
                return View(khachhang);
            }
            else
            {

                return RedirectToAction("DangNhap", "TrangChu");
            }

        }

        // POST: /TKKhachHang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKH,TenKH,MKTKKH,AnhKH")] KhachHang khachhang, string mkCu)
        {
            if (Session["TaiKhoan"] != null)
            {
                var User = (KhachHang)Session["TaiKhoan"];
                int id = User.MaKH;
                if (ModelState.IsValid)
            {
                var tkCu=db.KhachHangs.Where(x=>x.MaKH == id && x.MKTKKH==mkCu ).SingleOrDefault();
                if(tkCu == null)
                {
                    ModelState.AddModelError("MKCu", "Mật khẩu không đúng !");
                    return View();
                }
                var existingKhachHang = db.KhachHangs.Find(khachhang.MaKH);

                
                existingKhachHang.TenKH = khachhang.TenKH;
                
                
                existingKhachHang.AnhKH = khachhang.AnhKH;
                

                
                db.Entry(existingKhachHang).State = EntityState.Modified;

                
                db.SaveChanges();

                return RedirectToAction("Edit");
            }
            
            return View(khachhang);
            }
            else
            {

                return RedirectToAction("DangNhap", "TrangChu");
            }
        }
        public ActionResult DoiTT()
        {
            if (Session["TaiKhoan"] != null)
            {
                var User = (KhachHang)Session["TaiKhoan"];
                int id = User.MaKH;
                KhachHang khachhang = db.KhachHangs.Find(id);
                return View(khachhang);
            }
            else
            {

                return RedirectToAction("DangNhap", "TrangChu");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoiTT( KhachHang khachhang, string mkCu)
        {
            if (Session["TaiKhoan"] != null)
            {
                var User = (KhachHang)Session["TaiKhoan"];
                int id = User.MaKH;
                if (ModelState.IsValid)
                {
                    var tkCu = db.KhachHangs.Where(x => x.MaKH == id && x.MKTKKH == mkCu).SingleOrDefault();
                    if (tkCu == null)
                    {
                        ModelState.AddModelError("MKCu", "Mật khẩu không đúng !");
                        return View();
                    }
                    var existingKhachHang = db.KhachHangs.Find(khachhang.MaKH);


                    existingKhachHang.MKTKKH=khachhang.MKTKKH;
                    existingKhachHang.Email = khachhang.Email;
                    existingKhachHang.SoDienThoaiKH = khachhang.SoDienThoaiKH;



                    db.Entry(existingKhachHang).State = EntityState.Modified;


                    db.SaveChanges();

                    return RedirectToAction("DoiTT");
                }

                return View(khachhang);
            }
            else
            {

                return RedirectToAction("DangNhap", "TrangChu");
            }
        }
        // GET: /TKKhachHang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachhang = db.KhachHangs.Find(id);
            if (khachhang == null)
            {
                return HttpNotFound();
            }
            return View(khachhang);
        }

        // POST: /TKKhachHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KhachHang khachhang = db.KhachHangs.Find(id);
            db.KhachHangs.Remove(khachhang);
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
