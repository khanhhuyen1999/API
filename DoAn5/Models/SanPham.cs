using System;
using System.Collections.Generic;

namespace DoAn5.Models
{
    public partial class SanPham
    {

        public int MaSp { get; set; }
        public string TenSp { get; set; }
        public string MoTa { get; set; }
        public int? MaLoai { get; set; }
        public string Anh { get; set; }
        public int? SoLuong { get; set; }
        public int? Gia { get; set; }
        public DateTime CreatedAt { get; internal set; }
    }
}
