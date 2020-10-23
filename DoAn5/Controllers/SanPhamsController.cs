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
        public async Task<IActionResult> PutSanPham(int id, [FromForm] SanPhamClone)
        {
            var sanpham = _context.SanPham.Find(id);

            if (sanpham == null)
            {
                return BadRequest();
            }

            try
            {
                sanpham.MaSp = sanphamClone.MaSp;
                sanpham.TenSp = sanphamClone.TenSp;
                sanpham.MoTa = sanphamClone.MoTa;
                sanpham.Anh = sanphamClone.Anh;
                sanpham.MaLoai = sanphamClone.MaLoai;
                sanpham.SoLuong = sanphamClone.Soluong;
                sanpham.Gia = sanphamClone.Gia;

                var image = _fileService.WriteFile(SanPhamClone.Image, "/sanpham);
                sanpham.Image = image;

                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!sanphamExists(id))
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
        public async Task<ActionResult<SanPham>> PostSanpham([FromForm] SanPhamClone sanphamClone)
        {
            try
            {
                var sanpham = sanphamClone.get();
                var image = _fileService.WriteFile(sanphamClone.Image, "/sanpham");
                sanpham.Image = image;
                sanpham.CreatedAt = DateTime.Now;
                _context.SanPham.Add(sanpham);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetSanPham", new { id =sanpham.MaSp }, sanpham);
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

        // PUT: api/SanPhams/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSanPham(int id, SanPham sanPham)
        {
            if (id != sanPham.MaSp)
            {
                return BadRequest();
            }

            _context.Entry(sanPham).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

            return NoContent();
        }

        // POST: api/SanPhams
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SanPham>> PostSanPham(SanPham sanPham)
        {
            _context.SanPham.Add(sanPham);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSanPham", new { id = sanPham.MaSp }, sanPham);
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
