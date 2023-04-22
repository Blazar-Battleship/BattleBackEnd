using System;
using System.Collections.Generic;

namespace Battleship.Models;

public partial class Player
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? Points { get; set; }

    public int? CoalitionId { get; set; }

    public virtual Coalition? Coalition { get; set; }
}
