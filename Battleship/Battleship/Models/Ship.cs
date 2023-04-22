using System;
using System.Collections.Generic;

namespace Battleship.Models;

public partial class Ship
{
    public int Id { get; set; }

    public int GridId { get; set; }

    public virtual Grid Grid { get; set; } = null!;

    public virtual ICollection<ShipSlice> ShipSlices { get; set; } = new List<ShipSlice>();
}
