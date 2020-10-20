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
    public class HoaDonBansController : ControllerBase
    {
        private readonly DOAN5Context _context;

        public HoaDonBansController(DOAN5Context context)
        {
            _context = context;
        }

        // GET: api/HoaDonBans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HoaDonBan>>> GetHoaDonBan()
        {
            return await _context.HoaDonBan.ToListAsync();
        }

        // GET: api/HoaDonBans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HoaDonBan>> GetHoaDonBan(int id)
        {
            var hoaDonBan = await _context.HoaDonBan.FindAsync(id);

            if (hoaDonBan == null)
            {
                return NotFound();
            }

            return hoaDonBan;
        }

        // PUT: api/HoaDonBans/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHoaDonBan(int id, HoaDonBan hoaDonBan)
        {
            if (id != hoaDonBan.MaHdb)
            {
                return BadRequest();
            }

            _context.Entry(hoaDonBan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HoaDonBanExists(id))
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

        // POST: api/HoaDonBans
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<HoaDonBan>> PostHoaDonBan(HoaDonBan hoaDonBan)
        {
            _context.HoaDonBan.Add(hoaDonBan);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HoaDonBanExists(hoaDonBan.MaHdb))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetHoaDonBan", new { id = hoaDonBan.MaHdb }, hoaDonBan);
        }

        // DELETE: api/HoaDonBans/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HoaDonBan>> DeleteHoaDonBan(int id)
        {
            var hoaDonBan = await _context.HoaDonBan.FindAsync(id);
            if (hoaDonBan == null)
            {
                return NotFound();
            }

            _context.HoaDonBan.Remove(hoaDonBan);
            await _context.SaveChangesAsync();

            return hoaDonBan;
        }

        private bool HoaDonBanExists(int id)
        {
            return _context.HoaDonBan.Any(e => e.MaHdb == id);
        }
    }
}
