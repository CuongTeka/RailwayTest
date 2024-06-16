using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ThietBiDienTu.Areas.Admin.Models;

namespace ThietBiDienTu.Api
{
    public class TaiKhoanController : ApiController
    {
        private ThietBiDienTuEntities1 db = new ThietBiDienTuEntities1();

        // GET: /Admin/NhanVien/Details/5
        [HttpGet]
        [Route("api/nhanvien/details/{id:int}")]
        [ResponseType(typeof(NhanVien))]
        public IHttpActionResult GetNhanVienDetails(int id)
        {
            if (id <= 0)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid ID");
            }

            NhanVien nhanvien = db.NhanViens.Find(id);
            if (nhanvien == null)
            {
                return NotFound();
            }

            return Ok(nhanvien);
        }

        // PUT: api/nhanvien/edit/5
        [HttpPut]
        [Route("api/nhanvien/edit/{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult EditNhanVien(int id, [FromBody] NhanVien nhanvien)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nhanvien.MaAdmin)
            {
                return BadRequest("ID mismatch");
            }

            db.Entry(nhanvien).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NhanVienExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        private bool NhanVienExists(int id)
        {
            return db.NhanViens.Count(e => e.MaAdmin == id) > 0;
        }
    }
}
