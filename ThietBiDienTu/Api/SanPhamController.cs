using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ThietBiDienTu.Areas.Admin.Models;
using System.Data;
using System.Data.Entity;

namespace ThietBiDienTu.Api
{
    public class SanPhamController : ApiController
    {
        private ThietBiDienTuEntities1 db = new ThietBiDienTuEntities1();

        // GET: api/SanPham
        //[HttpGet]
        //[Route("api/sanpham")]
        ////[ResponseType(typeof(IQueryable<SanPham>))]
        //public IHttpActionResult GetSanPhams(int? page = 1)
        //{
        //    int pageSize = 12; // Số lượng sản phẩm muốn hiển thị trong 1 trang
        //    int pageNumber = page ?? 1;

        //    var sanpham = db.SanPhams
        //        .Include(s => s.BaoHanhs)
        //        .Include(s => s.DanhMuc)
        //        .Include(s => s.HinhAnh)
        //        .Include(s => s.HangSanXuat)
        //        .OrderBy(s => s.TenSP)
        //        .Skip((pageNumber - 1) * pageSize)
        //        .Take(pageSize);

        //    return Ok(sanpham);
        //}

        // GET: api/SanPham/All
        [HttpGet]
        [Route("api/sanpham/all")]
        [ResponseType(typeof(IQueryable<SanPham>))]
        public IHttpActionResult GetAllSanPhams()
        {
            var sanpham = db.SanPhams
                .Include(s => s.BaoHanhs)
                .Include(s => s.DanhMuc)
                .Include(s => s.HinhAnh)
                .Include(s => s.HangSanXuat);

            return Ok(sanpham);
        }

        // GET: api/SanPham/Details/{id}
        [HttpGet]
        [Route("api/sanpham/details/{id:int}")]
        [ResponseType(typeof(SanPham))]
        public IHttpActionResult GetSanPhamDetails(int id)
        {
            SanPham sanpham = db.SanPhams.Find(id);
            if (sanpham == null)
            {
                return NotFound();
            }

            return Ok(sanpham);
        }
    }
}
