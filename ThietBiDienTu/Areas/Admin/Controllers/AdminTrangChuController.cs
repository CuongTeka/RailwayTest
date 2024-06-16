using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using PagedList;
using ThietBiDienTu.Areas.Admin.Models;

namespace ThietBiDienTu.Areas.Admin.Controllers
{
    public class AdminTrangChuController : Controller
    {
        private ThietBiDienTuEntities1 db = new ThietBiDienTuEntities1();

        // GET: Admin/AdminTrangChu
        public ActionResult AdminTrangChu()

        {
            if (Session["TaiKhoanAD"] != null)
            {
                int maAd = (int)Session["TaiKhoanAD"];
                int maQuyen = (int)Session["PhanQuyen"];
                var Quyen = db.Quyens.SingleOrDefault(x => x.MaQuyen == maQuyen);
                var tk = db.NhanViens.SingleOrDefault(x => x.MaAdmin == maAd);
                Session["AnhAD"] = tk.AnhAdmin;
                Session["TenAD"] = tk.HoTenAdmin;
                Session["TenQuyen"] = Quyen.TenQuyen;

                int namHienTai = DateTime.Now.Year;
                int thangHienTai = DateTime.Now.Month;
                ViewBag.NamHienTai = namHienTai;

                var ThongKeThang1 = db.ThongKeThang(namHienTai, 1).FirstOrDefault();
                ViewBag.T1 = ThongKeThang1.SLHDThang;

                var ThongKeThang2 = db.ThongKeThang(namHienTai, 2).FirstOrDefault();
                ViewBag.T2 = ThongKeThang2.SLHDThang;

                var ThongKeThang3 = db.ThongKeThang(namHienTai, 3).FirstOrDefault();
                ViewBag.T3 = ThongKeThang3.SLHDThang;

                var ThongKeThang4 = db.ThongKeThang(namHienTai, 4).FirstOrDefault();
                ViewBag.T4 = ThongKeThang4.SLHDThang;

                var ThongKeThang5 = db.ThongKeThang(namHienTai, 5).FirstOrDefault();
                ViewBag.T5 = ThongKeThang5.SLHDThang;

                var ThongKeThang6 = db.ThongKeThang(namHienTai, 6).FirstOrDefault();
                ViewBag.T6 = ThongKeThang6.SLHDThang;

                var ThongKeThang7 = db.ThongKeThang(namHienTai, 7).FirstOrDefault();
                ViewBag.T7 = ThongKeThang7.SLHDThang;

                var ThongKeThang8 = db.ThongKeThang(namHienTai, 8).FirstOrDefault();
                ViewBag.T8 = ThongKeThang8.SLHDThang;

                var ThongKeThang9 = db.ThongKeThang(namHienTai, 9).FirstOrDefault();
                ViewBag.T9 = ThongKeThang9.SLHDThang;

                var ThongKeThang10 = db.ThongKeThang(namHienTai, 10).FirstOrDefault();
                ViewBag.T10 = ThongKeThang10.SLHDThang;

                var ThongKeThang11 = db.ThongKeThang(namHienTai, 11).FirstOrDefault();
                ViewBag.T11 = ThongKeThang11.SLHDThang;

                var ThongKeThang12 = db.ThongKeThang(namHienTai, 12).FirstOrDefault();
                ViewBag.T12 = ThongKeThang12.SLHDThang;

                var TK = db.ThongKeThang(namHienTai, thangHienTai).FirstOrDefault();
                ViewBag.SLHDTuan = TK.SLHDThang;
                ViewBag.SLSPTuan = TK.SLSP;
                ViewBag.TienTuan = TK.TienThang;

                var choXacNhanCount = db.DanhGiaSanPhams
                          .Where(x => x.TrangThai == 3)
                          .Count();
                ViewBag.SoDG= choXacNhanCount;

                var soDonBH=db.YeuCauBHs.Where(x=>x.TrangThai==3).Count();
                ViewBag.SoBH= soDonBH;

                var soDonHoan = db.HoanHangs.Where(x => x.TrangThai == 3).Count();
                ViewBag.SoHT=soDonHoan;

                return View();
            }
            else
            {


                return RedirectToAction("DangNhap", "DangNhap");
            }

        }
        public ActionResult ThongKeSPGanHet()
        {
            if (Session["TaiKhoanAD"] != null)
            {
                var sp = db.SanPhams.Where(s => s.SoLuongTon <= 10).ToList();
                return View(sp.ToList());
            }
            else
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }
        }
        [HttpPost]
        public ActionResult NhapHang(int masp, int sl)
        {
            if (Session["TaiKhoanAD"] != null)
            {
                var sp=db.SanPhams.Where(x=>x.MaSP==masp).SingleOrDefault();
                if (sp == null)
                {
                    return RedirectToAction("AdminTrangChu", "AdminTrangChu");
                }    
                else
                {
                    if(sl<=0)
                    {
                        return RedirectToAction("AdminTrangChu", "AdminTrangChu");
                    }    
                    else
                    {
                        sp.SoLuongTon = sp.SoLuongTon+sl;
                        db.SaveChanges();
                        return RedirectToAction("AdminTrangChu", "AdminTrangChu");
                    }    
                }    
                
            }
            else
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }
        }
        public ActionResult TimKiemSanPham(string timkiem, int? page)
        {
            int pageSize = 12; //Số lượng sản phẩm muốn hiển thị trong 1 trang
            int pageNumber = page ?? 1;

            // Kiểm tra xem tìm kiếm có giá trị hay không
            if (string.IsNullOrEmpty(timkiem))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Lấy danh sách sản phẩm từ cơ sở dữ liệu dựa trên tên sản phẩm
            var listSP = db.SanPhams.Where(m => m.TenSP.Contains(timkiem));
                
            // Kiểm tra xem danh sách sản phẩm có rỗng hay không
            if (!listSP.Any())
            {
                ViewBag.tukhoa = timkiem;
                return View(listSP.OrderBy(m => m.TenSP).ToPagedList(pageNumber, pageSize)); 
            }

            // Truyền dữ liệu tìm kiếm vào ViewBag để sử dụng trong View
            ViewBag.tukhoa = timkiem;

            // Trả về View với danh sách sản phẩm đã phân trang
            return View(listSP.OrderBy(m => m.TenSP).ToPagedList(pageNumber, pageSize));

        }
    }
}