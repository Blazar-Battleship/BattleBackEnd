using System;
using System.Collections.Generic;

namespace Battleship.Models;

public partial class Grid
{
    public int Id { get; set; }

    public int? CoalitionId { get; set; }

    public virtual Coalition? Coalition { get; set; }

    public virtual ICollection<Ship> Ships { get; set; } = new List<Ship>();
}
