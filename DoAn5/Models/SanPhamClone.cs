using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace DoAn5.Models
{
    public partial class SanPhamClone
    {
        public int MaSp { get; set; }
        public string TenSp { get; set; }
        public string MoTa { get; set; }
        public int? MaLoai { get; set; }
        public int? SoLuong { get; set; }
        public int? Gia { get; set; }
        public IFormFile Image { get; set; }

        public SanPham get()
        {
            var item = new SanPham();
            item.MaSp = MaSp;
            item.TenSp = TenSp;
            item.MoTa = MoTa;
            item.MaLoai = MaLoai;
            item.SoLuong = SoLuong;
            item.Gia = Gia;    
            return item;
        }
    }
}
