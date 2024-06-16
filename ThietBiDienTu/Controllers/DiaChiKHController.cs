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
    public class DiaChiKHController : Controller
    {
        private ThietBiDienTuEntities1 db = new ThietBiDienTuEntities1();

        // GET: /DiaChiKH/
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] != null)
            {
                // Lấy giá trị từ session
                var taiKhoan = (KhachHang)Session["TaiKhoan"];

                // Sử dụng giá trị từ session trong các logic của bạn
                var diaChi = db.GetCustomerAddress(taiKhoan.MaKH);
                ViewBag.CoTK = taiKhoan.MaKH;
                return View(diaChi.ToList());
            }
            else
            {
                ViewBag.CoTK = 0;
                // Nếu session không tồn tại, có thể chuyển hướng đến trang đăng nhập hoặc thực hiện xử lý khác
                return RedirectToAction("DangNhap", "TrangChu"); // Thay "TenController" bằng tên của controller đăng nhập
            }
        }

        // GET: /DiaChiKH/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTDiaChi ctdiachi = db.CTDiaChis.Find(id);
            if (ctdiachi == null)
            {
                return HttpNotFound();
            }
            return View(ctdiachi);
        }

        // GET: /DiaChiKH/Create
        public ActionResult Create()
        {
            ViewBag.MaDiaChi = new SelectList(db.DiaChis, "MaDiaChi", "MaDiaChi");
            return View();
        }

        // POST: /DiaChiKH/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaCTDiaChi,MaDiaChi,SoNha,TenDuong,PhuongXa,QuanHuyen,TinhThanh,MacDinh,TenNguoiNhan,SDTNguoiNhan")] CTDiaChi ctdiachi)
        {
            if (ModelState.IsValid)
            {
                db.CTDiaChis.Add(ctdiachi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDiaChi = new SelectList(db.DiaChis, "MaDiaChi", "MaDiaChi", ctdiachi.MaDiaChi);
            return View(ctdiachi);
        }

        // GET: /DiaChiKH/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTDiaChi ctdiachi = db.CTDiaChis.Find(id);
            if (ctdiachi == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDiaChi = new SelectList(db.DiaChis, "MaDiaChi", "MaDiaChi", ctdiachi.MaDiaChi);
            return View(ctdiachi);
        }

        // POST: /DiaChiKH/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaCTDiaChi,MaDiaChi,SoNha,TenDuong,PhuongXa,QuanHuyen,TinhThanh,MacDinh,TenNguoiNhan,SDTNguoiNhan")] CTDiaChi ctdiachi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ctdiachi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDiaChi = new SelectList(db.DiaChis, "MaDiaChi", "MaDiaChi", ctdiachi.MaDiaChi);
            return View(ctdiachi);
        }

        // GET: /DiaChiKH/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTDiaChi ctdiachi = db.CTDiaChis.Find(id);
            if (ctdiachi == null)
            {
                return HttpNotFound();
            }
            return View(ctdiachi);
        }

        // POST: /DiaChiKH/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CTDiaChi ctdiachi = db.CTDiaChis.Find(id);
            db.CTDiaChis.Remove(ctdiachi);
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
