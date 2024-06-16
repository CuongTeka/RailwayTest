using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThietBiDienTu.Areas.Admin.Models
{
    public class SanPhamViewModels
    {
        public SanPham SanPham { get; set; }
        public HinhAnh HinhAnh { get; set; }
        public List<layDanhGia_Result> DanhGias { get; set; }
    }
}