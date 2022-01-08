using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spitali.Models;

namespace Spitali.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpitalsController : ControllerBase
    {
        private readonly SpitalsContext _context;

        public SpitalsController(SpitalsContext context)
        {
            _context = context;
        }

        // GET: api/Spitals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Spital>>> GetSpitals()
        {
            return await _context.Spitals.ToListAsync();
        }

        // GET: api/Spitals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Spital>> GetSpital(int id)
        {
            var spital = await _context.Spitals.FindAsync(id);

            if (spital == null)
            {
                return NotFound();
            }

            return spital;
        }

        // PUT: api/Spitals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpital(int id, Spital spital)
        {
            if (id != spital.SpitalId)
            {
                return BadRequest();
            }

            _context.Entry(spital).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpitalExists(id))
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

        // POST: api/Spitals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Spital>> PostSpital(Spital spital)
        {
            _context.Spitals.Add(spital);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpital", new { id = spital.SpitalId }, spital);
        }

        // DELETE: api/Spitals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpital(int id)
        {
            var spital = await _context.Spitals.FindAsync(id);
            if (spital == null)
            {
                return NotFound();
            }

            _context.Spitals.Remove(spital);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpitalExists(int id)
        {
            return _context.Spitals.Any(e => e.SpitalId == id);
        }
    }
}
