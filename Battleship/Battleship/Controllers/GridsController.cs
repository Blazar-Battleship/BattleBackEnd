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
        [HttpPost("rossa")]
        public async Task<ActionResult<Grid>> PostGrid(Ship[] flotta1)
        {
            Grid grid = new Grid();
            Coalition co = _context.Coalitions.Where(x=> x.Name=="red").FirstOrDefault();
            foreach(Ship s in flotta1)
            {
                grid.Ships.Add(s);
            }
            co.Grids.Add(grid);
            await _context.SaveChangesAsync();
            return grid;

        }

        [HttpPost("blu")]
        public async Task<ActionResult<Grid>> PostGrid2(Ship[] flotta1)
        {
            Grid grid = new Grid();
            Coalition co = _context.Coalitions.Where(x => x.Name == "blue").FirstOrDefault();
            foreach (Ship s in flotta1)
            {
                grid.Ships.Add(s);
            }
            co.Grids.Add(grid);
            await _context.SaveChangesAsync();
            return grid;

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
