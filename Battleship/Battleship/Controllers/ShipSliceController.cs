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
    public class ShipSliceController : ControllerBase
    {
        private readonly BattleShipContext _context;

        public ShipSliceController(BattleShipContext context)
        {
            _context = context;
        }

        // GET: api/ShipSlice
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShipSlice>>> GetShipSlices()
        {
          if (_context.ShipSlices == null)
          {
              return NotFound();
          }
            return await _context.ShipSlices.ToListAsync();
        }

        // GET: api/ShipSlice/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShipSlice>> GetShipSlice(int id)
        {
          if (_context.ShipSlices == null)
          {
              return NotFound();
          }
            var shipSlice = await _context.ShipSlices.FindAsync(id);

            if (shipSlice == null)
            {
                return NotFound();
            }

            return shipSlice;
        }

        // PUT: api/ShipSlice/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShipSlice(int id, ShipSlice shipSlice)
        {
            if (id != shipSlice.Id)
            {
                return BadRequest();
            }

            _context.Entry(shipSlice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShipSliceExists(id))
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

        // POST: api/ShipSlice
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShipSlice>> PostShipSlice(ShipSlice shipSlice)
        {
          if (_context.ShipSlices == null)
          {
              return Problem("Entity set 'BattleShipContext.ShipSlices'  is null.");
          }
            _context.ShipSlices.Add(shipSlice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShipSlice", new { id = shipSlice.Id }, shipSlice);
        }

        // DELETE: api/ShipSlice/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShipSlice(int id)
        {
            if (_context.ShipSlices == null)
            {
                return NotFound();
            }
            var shipSlice = await _context.ShipSlices.FindAsync(id);
            if (shipSlice == null)
            {
                return NotFound();
            }

            _context.ShipSlices.Remove(shipSlice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShipSliceExists(int id)
        {
            return (_context.ShipSlices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
