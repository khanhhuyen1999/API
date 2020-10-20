using System;
using System.Collections.Generic;

namespace DoAn5.Models
{
    public partial class Ncc
    {
        public int MaNcc { get; set; }
        public string TenNcc { get; set; }
        public string DiaChi { get; set; }
        public string Sdt { get; set; }

        public virtual HoaDonNhap MaNccNavigation { get; set; }
    }
}
