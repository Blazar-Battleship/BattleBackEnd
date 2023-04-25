using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Battleship.Models;
using Microsoft.IdentityModel.Tokens;

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

        // POST: salvataggio flotta della coalizione rossa
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

        //POST : salvataggio flotta della coalizione blu
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

        //POST: gestione attacco e aggiornamento database
        [HttpPost("{player}/{enemy}")]
        public async Task<ActionResult<Player>> Shoot(string player, string enemy, ShipSlice cordinates)
        {
            List<ShipSlice> res = _context.ShipSlices.Where(x => x.X == cordinates.X & x.Y == cordinates.Y & x.Team == enemy).ToList();
            List<int> IDs = new List<int>();
            int shot = 0;
            int destroy = 0;

            foreach (ShipSlice s in res)
            {
                s.Status = true;
                IDs.Add(s.ShipId);
                shot++;

                _context.Entry(s).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            foreach (int s in IDs)
            {
                Ship barca = _context.Ships.Where(x => x.Id == s).FirstOrDefault();

                List<ShipSlice> rest = _context.ShipSlices.Where(x => x.ShipId == s & x.Status == false).ToList();

                if (rest.IsNullOrEmpty())
                {
                    destroy++;
                    _context.Ships.Remove(barca);
                    await _context.SaveChangesAsync();
                }
            }

            Player User = _context.Players.Where(x => x.Name == player).FirstOrDefault();
            User.Points += (10 * shot) + (5 * destroy);
            _context.Entry(User).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return Ok(User);
        }

        private bool GridExists(int id)
        {
            return (_context.Grids?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
