using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThietBiDienTu.Areas.Admin.Models
{
    public class QuanLyDanhGiaViewModel
    {
        public List<DanhGiaSanPham> DanhGiaDangHienThi { get; set; }
        public List<DanhGiaSanPham> DanhGiaAn { get; set; }
        public List<DanhGiaSanPham> DanhGiaChoXacNhan { get; set; }
    }
}