using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DoAn5.Models;
using DoAn5.Helper;
using DoAn5.Services;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace DoAn5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamsController : ControllerBase
    {
        private readonly DOAN5Context _context;
        private readonly IFileService _fileService;
        private object sanphamClone;

        public SanPhamsController(DOAN5Context context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        // GET: api/SanPhams
        [HttpGet]
        public IEnumerable<SanPham> GetSanPham()
        {
            return (from sp in _context.SanPham
                    orderby sp.Gia descending
                    select sp
                    ).Take(12);
        }

        // GET: api/SanPhams/5

        [HttpGet("{id}")]
        public async Task<ActionResult<SanPham>> GetSanPham(int id)
        {
            var sanPham = await _context.SanPham.FindAsync(id);

            if (sanPham == null)
            {
                return NotFound();
            }

            return sanPham;
        }

        [HttpGet("pagination")]
        public ActionResult<IEnumerable<SanPham>> GetPage(int page, int pageSize)
        {
            var paging = Pagination.GetPaged(_context.SanPham, page, pageSize);

            return Ok(paging);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSanPham(int id, [FromBody] Dictionary<string, object> formData)
        {
            var sp = _context.SanPham.Find(id);

            if (sp == null)
            {
                return BadRequest();
            }

            try
            {
                sp.MaLoai = int.Parse(formData["MaLoai"].ToString());
                sp.TenSp = formData["TenSP"].ToString();
                sp.MoTa = formData["MoTa"].ToString();
                sp.SoLuong = int.Parse(formData["SoLuong"].ToString());
                sp.Gia = int.Parse(formData["Gia"].ToString());

                if (formData.ContainsKey("HinhAnh"))
                {
                    var Image = formData["HinhAnh"].ToString();

                    if ((sp.Anh = _fileService.WriteFileBase64("/sanpham", Image)) == null)
                    {
                        sp.Anh = "error.jpg";
                    }
                }

                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SanPhamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SanPham>> PostSanpham([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var sp = new SanPham();

                sp.MaLoai = int.Parse(formData["MaLoai"].ToString());
                sp.TenSp = formData["TenSP"].ToString();
                sp.MoTa = formData["MoTa"].ToString();
                sp.SoLuong = int.Parse(formData["SoLuong"].ToString());
                sp.Gia = int.Parse(formData["Gia"].ToString());

                var Image = formData["HinhAnh"].ToString();

                if ((sp.Anh = _fileService.WriteFileBase64("/sanpham", Image)) == null)
                {
                    sp.Anh = "error.jpg";
                }

                _context.SanPham.Add(sp);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetSanPham", new { id = sp.MaSp }, sp);
            }
            catch (Exception e)
            {
                return Ok(new { message = e.Message });
            }
        }

        [HttpGet("tim-kiem")]
        public IEnumerable<SanPham> GetTimsanpham(string key)
        {
            var sanPhams = _context.SanPham.Where(vl => vl.TenSp.IndexOf(key) != -1);
            return sanPhams;
        }

        [HttpGet("get-loai/{id}")]
        public IEnumerable<SanPham> GetLoaiSanPham(int id)
        {
            var sanPham = _context.SanPham.Where(sp => sp.MaLoai == id);

            return sanPham;
        }

        [HttpGet("get-loai-top4/{id}")]
        public IEnumerable<SanPham> GetLoaiSanPhamTop4(int id)
        {
            var sanPham = _context.SanPham.Where(sp => sp.MaLoai == id).Take(4);

            return sanPham;
        }

        [HttpGet("get-loai-top6/{id}")]
        public IEnumerable<SanPham> GetLoaiSanPhamTop6(int id)
        {
            var sanPham = _context.SanPham.Where(sp => sp.MaLoai == id).Take(6);

            return sanPham;
        }

        // DELETE: api/SanPhams/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SanPham>> DeleteSanPham(int id)
        {
            var sanPham = await _context.SanPham.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }

            _context.SanPham.Remove(sanPham);
            await _context.SaveChangesAsync();

            return sanPham;
        }

        private bool SanPhamExists(int id)
        {
            return _context.SanPham.Any(e => e.MaSp == id);
        }
    }
}
