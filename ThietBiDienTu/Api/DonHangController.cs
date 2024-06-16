using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ThietBiDienTu.Areas.Admin.Models;

namespace ThietBiDienTu.Api
{
    public class DonHangController : ApiController
    {
        private ThietBiDienTuEntities1 db = new ThietBiDienTuEntities1();

        // GET: api/donhang/quanly
        [HttpGet]
        [Route("api/donhang/quanly")]
        [ResponseType(typeof(IQueryable<DDH>))]
        public IHttpActionResult QuanLyDonHang()
        {
            var ddh = db.GetOrderInfoByStatus1(1);
            return Ok(ddh);
        }

        // GET: api/donhang/dagiao
        [HttpGet]
        [Route("api/donhang/dagiao")]
        [ResponseType(typeof(IQueryable<DDH>))]
        public IHttpActionResult DaGiao()
        {
            var ddh = db.GetOrderInfoByStatus1(2);
            return Ok(ddh.ToList());
        }

        // PUT: api/donhang/xacnhandagiao/5
        [HttpPut]
        [Route("api/donhang/xacnhandagiao/{id:int}")]
        public IHttpActionResult XacNhanDaGiao(int id)
        {
            var dh = db.DDHs.Find(id);
            if (dh == null)
            {
                return NotFound();
            }

            dh.TrangThai = 3;
            db.SaveChanges();
            return Ok();
        }

        // PUT: api/donhang/xacnhandh/5
        [HttpPut]
        [Route("api/donhang/xacnhandh/{id:int}")]
        public IHttpActionResult XacNhanDH(int id)
        {
            var dh = db.DDHs.Find(id);
            if (dh == null)
            {
                return NotFound();
            }

            dh.TrangThai = 2;
            db.SaveChanges();
            return Ok();
        }

        ////GET: api/donhang/huydh/5
        //[HttpGet]
        //[Route("api/donhang/huydh/{MaDDH:int}")]
        //[ResponseType(typeof(void))]
        //public IHttpActionResult HuyDH(int MaDDH)
        //{
        //    ViewBag.MaDDH = MaDDH;
        //    return Ok();
        //}

        // POST: api/donhang/huydh
        [HttpPost]
        [Route("api/donhang/huydh")]
        public IHttpActionResult HuyDH([FromBody] HuyDHRequest request)
        {
            db.CapNhatHuyDonHangByMaDDH(request.MaDDH, request.LyDo);
            return Ok();
        }

        // GET: api/donhang/dahuy
        [HttpGet]
        [Route("api/donhang/dahuy")]
        [ResponseType(typeof(IQueryable<HuyHang>))]
        public IHttpActionResult DaHuy()
        {
            var DaHuy = db.HuyHangs.Where(x => x.MaKH == 3);
            return Ok(DaHuy.ToList());
        }

        // GET: api/donhang/xemchitietdh/5
        [HttpGet]
        [Route("api/donhang/xemchitietdh/{idDDH:int}")]
        [ResponseType(typeof(IQueryable<CTDDH>))]
        public IHttpActionResult XemChiTietDH(int idDDH)
        {
            var XemCT = db.GetOrderInfoByMaDDH(idDDH);
            return Ok(XemCT.ToList());
        }
    }

    public class HuyDHRequest
    {
        public int MaDDH { get; set; }
        public string LyDo { get; set; }
    }
}

