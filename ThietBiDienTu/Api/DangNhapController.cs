using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ThietBiDienTu.Areas.Admin.Models;

namespace ThietBiDienTu.Api
{
    public class DangNhapController : ApiController
    {
        private ThietBiDienTuEntities1 db = new ThietBiDienTuEntities1();

        [HttpPost]
        [Route("api/loginAdmin")]
        public IHttpActionResult PostDangNhap([FromBody] DangNhapModel model)
        {
            if (model == null)
            {
                return BadRequest("Dữ liệu không hợp lệ");
            }

            var tk = db.NhanViens.SingleOrDefault(x => x.TenTaiKhoanAdmin == model.tendn && x.MatKhauAdmin == model.mk);
            if (tk == null)
            {
                /*return NotFound();*/ // Tài khoản không tồn tại
                return BadRequest("Sai mật khẩu hoặc tài khoản");
            }

            if (tk.MatKhauAdmin != model.mk)
            {
                return BadRequest("Sai mật khẩu hoặc tài khoản"); // Mật khẩu không chính xác
            }

            // session
            //HttpContext.Current.Session["TaiKhoanAd"] = tk.MaAdmin;
            //HttpContext.Current.Session["PhanQuyen"] = tk.MaQuyen;

            //lưu vào local storage trong app
            //risky but fast

            return Ok(new { message = "Login successful", MaAdmin = tk.MaAdmin, MaQuyen = tk.MaQuyen });
        }
    }
}

public class DangNhapModel
{
    public string tendn { get; set; }
    public string mk { get; set; }
}
