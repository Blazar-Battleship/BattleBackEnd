using System;
using System.Collections.Generic;

namespace Battleship.Models;

public partial class Coalition
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int GameId { get; set; }

    public virtual Game Game { get; set; } = null!;

    public virtual ICollection<Grid> Grids { get; set; } = new List<Grid>();

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
