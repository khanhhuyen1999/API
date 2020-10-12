using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DoAn5.Models;

namespace DoAn5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonNhapsController : ControllerBase
    {
        private readonly DOAN5Context _context;

        public HoaDonNhapsController(DOAN5Context context)
        {
            _context = context;
        }

        // GET: api/HoaDonNhaps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HoaDonNhap>>> GetHoaDonNhap()
        {
            return await _context.HoaDonNhap.ToListAsync();
        }

        // GET: api/HoaDonNhaps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HoaDonNhap>> GetHoaDonNhap(int id)
        {
            var hoaDonNhap = await _context.HoaDonNhap.FindAsync(id);

            if (hoaDonNhap == null)
            {
                return NotFound();
            }

            return hoaDonNhap;
        }

        // PUT: api/HoaDonNhaps/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHoaDonNhap(int id, HoaDonNhap hoaDonNhap)
        {
            if (id != hoaDonNhap.MaHdn)
            {
                return BadRequest();
            }

            _context.Entry(hoaDonNhap).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HoaDonNhapExists(id))
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

        // POST: api/HoaDonNhaps
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HoaDonNhap>> PostHoaDonNhap(HoaDonNhap hoaDonNhap)
        {
            _context.HoaDonNhap.Add(hoaDonNhap);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHoaDonNhap", new { id = hoaDonNhap.MaHdn }, hoaDonNhap);
        }

        // DELETE: api/HoaDonNhaps/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HoaDonNhap>> DeleteHoaDonNhap(int id)
        {
            var hoaDonNhap = await _context.HoaDonNhap.FindAsync(id);
            if (hoaDonNhap == null)
            {
                return NotFound();
            }

            _context.HoaDonNhap.Remove(hoaDonNhap);
            await _context.SaveChangesAsync();

            return hoaDonNhap;
        }

        private bool HoaDonNhapExists(int id)
        {
            return _context.HoaDonNhap.Any(e => e.MaHdn == id);
        }
    }
}
