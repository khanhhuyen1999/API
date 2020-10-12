using System;
using System.Collections.Generic;

namespace DoAn5.Models
{
    public partial class HoaDonBan
    {
        public int MaHdb { get; set; }
        public int? MaKh { get; set; }
        public DateTime? NgayBan { get; set; }

        public virtual KhachHang KhachHang { get; set; }
    }
}
