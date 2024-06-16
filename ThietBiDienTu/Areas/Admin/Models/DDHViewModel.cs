using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThietBiDienTu.Areas.Admin.Models
{
    public class DDHViewModel
    {
        public GetCustomerAddress_Result diachi { get; set; }
        public List<NhaVanChuyen> nhavanchuyen { get; set; }
    }
}