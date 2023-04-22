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
    public class GridsController : ControllerBase
    {
        private readonly BattleShipContext _context;

        public GridsController(BattleShipContext context)
        {
            _context = context;
        }

        // GET: api/Grids
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Grid>>> GetGrids()
        {
          if (_context.Grids == null)
          {
              return NotFound();
          }
            return await _context.Grids.ToListAsync();
        }

        // GET: api/Grids/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Grid>> GetGrid(int id)
        {
          if (_context.Grids == null)
          {
              return NotFound();
          }
            var grid = await _context.Grids.FindAsync(id);

            if (grid == null)
            {
                return NotFound();
            }

            return grid;
        }

        // PUT: api/Grids/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGrid(int id, Grid grid)
        {
            if (id != grid.Id)
            {
                return BadRequest();
            }

            _context.Entry(grid).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GridExists(id))
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

        // POST: api/Grids
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Grid>> PostGrid(Grid grid)
        {
          if (_context.Grids == null)
          {
              return Problem("Entity set 'BattleShipContext.Grids'  is null.");
          }
            _context.Grids.Add(grid);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGrid", new { id = grid.Id }, grid);
        }

        // DELETE: api/Grids/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrid(int id)
        {
            if (_context.Grids == null)
            {
                return NotFound();
            }
            var grid = await _context.Grids.FindAsync(id);
            if (grid == null)
            {
                return NotFound();
            }

            _context.Grids.Remove(grid);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GridExists(int id)
        {
            return (_context.Grids?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
