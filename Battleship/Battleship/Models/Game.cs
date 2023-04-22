using System;
using System.Collections.Generic;

namespace Battleship.Models;

public partial class Game
{
    public int Id { get; set; }

    public virtual ICollection<Coalition> Coalitions { get; set; } = new List<Coalition>();
}
