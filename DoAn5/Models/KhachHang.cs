using System;
using System.Collections.Generic;

namespace DoAn5.Models
{
    public partial class KhachHang
    {
        public int MaKh { get; set; }
        public string TenKh { get; set; }
        public string TaiKhoan { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int? Sdt { get; set; }
        public string DiaChi { get; set; }

        public virtual HoaDonBan MaKhNavigation { get; set; }
    }
}
