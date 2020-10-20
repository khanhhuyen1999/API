using System;
using System.Collections.Generic;

namespace DoAn5.Models
{
    public partial class HoaDonNhap
    {
        public int MaHdn { get; set; }
        public int? MaNcc { get; set; }
        public DateTime? NgayNhap { get; set; }

        public virtual Ncc Ncc { get; set; }
    }
}
