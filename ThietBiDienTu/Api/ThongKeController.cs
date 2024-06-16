using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ThietBiDienTu.Areas.Admin.Models;

namespace ThietBiDienTu.Api
{
    public class ThongKeController : ApiController
    {
        private ThietBiDienTuEntities1 db = new ThietBiDienTuEntities1();

        // GET: api/doanhthu/tkbathang
        [HttpGet]
        [Route("api/doanhthu/tkbathang")]
        public IHttpActionResult TKBaThang()
        {
            int thangHienTai = DateTime.Now.Month;
            int thangTruoc = thangHienTai - 1;
            int thangTruocTruoc = thangTruoc - 1;

            var tkTHT = db.ThongKeThang(DateTime.Now.Year, thangHienTai).FirstOrDefault();
            var tkTT = db.ThongKeThang(DateTime.Now.Year, thangTruoc).FirstOrDefault();
            var tkTTT = db.ThongKeThang(DateTime.Now.Year, thangTruocTruoc).FirstOrDefault();

            var result = new
            {
                tHT = thangHienTai,
                tT = thangTruoc,
                tTT = thangTruocTruoc,
                tkTHienTai = tkTHT?.TienThang ?? 0,
                tkThangTruoc = tkTT?.TienThang ?? 0,
                tkTTruocTruoc = tkTTT?.TienThang ?? 0
            };

            return Ok(result);
        }

        // GET: api/doanhthu/tktheothang?Thang={Thang}&Nam={Nam}
        [HttpGet]
        [Route("api/doanhthu/tktheothang")]
        public IHttpActionResult TKTheoThang(int Thang, int Nam)
        {
            var tk = db.ThongKeHoaDonTheoThang(Thang, Nam, 3);
            return Ok(tk.ToList());
        }

        // GET: api/doanhthu/tktheongay?ngay={ngay}
        [HttpGet]
        [Route("api/doanhthu/tktheongay")]
        public IHttpActionResult TkTheoNgay(DateTime ngay)
        {
            int Ngay = ngay.Day;
            int Thang = ngay.Month;
            int Nam = ngay.Year;

            var ctdh = db.ThongKeHoaDonTheoNgay(Ngay, Thang, Nam, 3);
            return Ok(ctdh.ToList());
        }

        // GET: api/doanhthu/tktheokhoan?ngaybd={ngaybd}&ngaykt={ngaykt}
        [HttpGet]
        [Route("api/doanhthu/tktheokhoan")]
        public IHttpActionResult TKTheoKhoan(DateTime ngaybd, DateTime ngaykt)
        {
            if (ngaybd > ngaykt)
            {
                return BadRequest("Ngày bắt đầu phải nhỏ hơn ngày kết thúc.");
            }

            var ctdh = db.ThongKeHDTheoKhoang(ngaybd, ngaykt, 3);
            return Ok(ctdh.ToList());
        }

        // GET: api/doanhthu/tkbanra
        [HttpGet]
        [Route("api/doanhthu/tkbanra")]
        public IHttpActionResult TKBanRa()
        {
            var ctdh = db.ThongKeThang(DateTime.Now.Year, DateTime.Now.Month);
            return Ok(ctdh.ToList());
        }

        // GET: api/doanhthu/tktuanbanra
        [HttpGet]
        [Route("api/doanhthu/tktuanbanra")]
        public IHttpActionResult TKTuanBanRa()
        {
            var ctdh = db.ThongKeTuan();
            return Ok(ctdh.ToList());
        }

        // GET: api/doanhthu/tknambanra
        [HttpGet]
        [Route("api/doanhthu/tknambanra")]
        public IHttpActionResult TKNamBanRa()
        {
            var ctdh = db.ThongKeNam(DateTime.Now.Year);
            return Ok(ctdh.ToList());
        }
    }
}
