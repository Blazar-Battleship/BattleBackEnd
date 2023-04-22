using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Battleship.Models;
using System.Numerics;
using Newtonsoft.Json.Linq;

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
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Coalition>>> GetCoalitions()
        //{
        //  if (_context.Coalitions == null)
        //  {
        //      return NotFound();
        //  }
        //    return await _context.Coalitions.ToListAsync();
        //}

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
        public async Task<ActionResult<Game>> PostCoalition(Player[] players)
        {
            if (_context.Coalitions == null)
            {
                return Problem("Entity set 'BattleShipContext.Coalitions'  is null.");
            }

            Game game = new Game();
            Coalition coal1 = new Coalition();
            Coalition coal2 = new Coalition();

            game.Coalitions.Add(coal1);
            game.Coalitions.Add(coal2);

            if(players.Length == 2)
            {
                Player player1 = players[0];
                Player player2 = players[1];

                coal1.Players.Add(player1);
                coal2.Players.Add(player2);
            }else if(players.Length > 2)
            {
                var rnd = new Random();
                List<int> numbers = Enumerable.Range(0, players.Length).OrderBy(x => rnd.Next()).Take(players.Length).ToList();
                int num = 0;

                Random rand = new Random();
                Console.WriteLine(numbers);
                foreach (Player i in players.OrderBy(x => rand.Next()))
                {
                    if(num == 0)
                    {
                        Player play1 = i;
                        coal1.Players.Add(play1);
                        num = 1;
                    }
                    else
                    {
                        Player play2 = i;
                        coal2.Players.Add(play2);
                        num = 0;
                    }           

                }
            }

            _context.Games.Add(game);
            await _context.SaveChangesAsync();

            return game;
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
