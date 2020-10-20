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
    public class NccsController : ControllerBase
    {
        private readonly DOAN5Context _context;

        public NccsController(DOAN5Context context)
        {
            _context = context;
        }

        // GET: api/Nccs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ncc>>> GetNcc()
        {
            return await _context.Ncc.ToListAsync();
        }

        // GET: api/Nccs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ncc>> GetNcc(int id)
        {
            var ncc = await _context.Ncc.FindAsync(id);

            if (ncc == null)
            {
                return NotFound();
            }

            return ncc;
        }

        // PUT: api/Nccs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNcc(int id, Ncc ncc)
        {
            if (id != ncc.MaNcc)
            {
                return BadRequest();
            }

            _context.Entry(ncc).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NccExists(id))
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

        // POST: api/Nccs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Ncc>> PostNcc(Ncc ncc)
        {
            _context.Ncc.Add(ncc);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNcc", new { id = ncc.MaNcc }, ncc);
        }

        // DELETE: api/Nccs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Ncc>> DeleteNcc(int id)
        {
            var ncc = await _context.Ncc.FindAsync(id);
            if (ncc == null)
            {
                return NotFound();
            }

            _context.Ncc.Remove(ncc);
            await _context.SaveChangesAsync();

            return ncc;
        }

        private bool NccExists(int id)
        {
            return _context.Ncc.Any(e => e.MaNcc == id);
        }
    }
}
