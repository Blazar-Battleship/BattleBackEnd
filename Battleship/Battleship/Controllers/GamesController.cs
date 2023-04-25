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
    public class GamesController : ControllerBase
    {
        private readonly BattleShipContext _context;

        public GamesController(BattleShipContext context)
        {
            _context = context;
        }

        // POST: api/Games
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
            coal1.Name = "red";
            coal2.Name = "blue";

            game.Coalitions.Add(coal1);
            game.Coalitions.Add(coal2);

            if (players.Length == 2)
            {
                Player player1 = players[0];
                Player player2 = players[1];

                coal1.Players.Add(player1);
                coal2.Players.Add(player2);
            }
            else if (players.Length > 2)
            {
                var rnd = new Random();
                List<int> numbers = Enumerable.Range(0, players.Length).OrderBy(x => rnd.Next()).Take(players.Length).ToList();
                int num = 0;

                Random rand = new Random();
                Console.WriteLine(numbers);
                foreach (Player i in players.OrderBy(x => rand.Next()))
                {
                    if (num == 0)
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

        // DELETE: api/Games/5
        [HttpDelete]
        public async Task<List<Player>> DeleteGame(Game game)
        {
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return _context.Players.OrderBy(x => x.Points).ToList();
        }

   

        private bool GameExists(int id)
        {
            return (_context.Games?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
