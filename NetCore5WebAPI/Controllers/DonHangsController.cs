using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCore5WebAPI.Data;

namespace NetCore5WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonHangsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public DonHangsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/DonHangs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DonHang>>> GetDonHangs()
        {
          if (_context.DonHangs == null)
          {
              return NotFound();
          }
            return await _context.DonHangs.ToListAsync();
        }

        // GET: api/DonHangs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DonHang>> GetDonHang(Guid id)
        {
          if (_context.DonHangs == null)
          {
              return NotFound();
          }
            var donHang = await _context.DonHangs.FindAsync(id);

            if (donHang == null)
            {
                return NotFound();
            }

            return donHang;
        }

        // PUT: api/DonHangs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDonHang(Guid id, DonHang donHang)
        {
            if (id != donHang.MaDH)
            {
                return BadRequest();
            }

            _context.Entry(donHang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonHangExists(id))
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

        // POST: api/DonHangs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DonHang>> PostDonHang(DonHang donHang)
        {
          if (_context.DonHangs == null)
          {
              return Problem("Entity set 'MyDbContext.DonHangs'  is null.");
          }
            _context.DonHangs.Add(donHang);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDonHang", new { id = donHang.MaDH }, donHang);
        }

        // DELETE: api/DonHangs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDonHang(Guid id)
        {
            if (_context.DonHangs == null)
            {
                return NotFound();
            }
            var donHang = await _context.DonHangs.FindAsync(id);
            if (donHang == null)
            {
                return NotFound();
            }

            _context.DonHangs.Remove(donHang);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DonHangExists(Guid id)
        {
            return (_context.DonHangs?.Any(e => e.MaDH == id)).GetValueOrDefault();
        }
    }
}
