using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Battleship.Models;

namespace Battleship.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoalitionsController : ControllerBase
    {
        private readonly BattleShipContext _context;

        public CoalitionsController(BattleShipContext context)
        {
            _context = context;
        }

        // GET: api/Coalitions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coalition>>> GetCoalitions()
        {
          if (_context.Coalitions == null)
          {
              return NotFound();
          }
            return await _context.Coalitions.ToListAsync();
        }

        // GET: api/Coalitions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Coalition>> GetCoalition(int id)
        {
          if (_context.Coalitions == null)
          {
              return NotFound();
          }
            var coalition = await _context.Coalitions.FindAsync(id);

            if (coalition == null)
            {
                return NotFound();
            }

            return coalition;
        }

        // PUT: api/Coalitions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoalition(int id, Coalition coalition)
        {
            if (id != coalition.Id)
            {
                return BadRequest();
            }

            _context.Entry(coalition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoalitionExists(id))
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

        // POST: api/Coalitions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Coalition>> PostCoalition(Coalition coalition)
        {
          if (_context.Coalitions == null)
          {
              return Problem("Entity set 'BattleShipContext.Coalitions'  is null.");
          }
            _context.Coalitions.Add(coalition);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoalition", new { id = coalition.Id }, coalition);
        }

        // DELETE: api/Coalitions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoalition(int id)
        {
            if (_context.Coalitions == null)
            {
                return NotFound();
            }
            var coalition = await _context.Coalitions.FindAsync(id);
            if (coalition == null)
            {
                return NotFound();
            }

            _context.Coalitions.Remove(coalition);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CoalitionExists(int id)
        {
            return (_context.Coalitions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
