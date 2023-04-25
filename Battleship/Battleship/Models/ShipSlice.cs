using System;
using System.Collections.Generic;

namespace Battleship.Models;

public partial class ShipSlice
{
    public int Id { get; set; }

    public int X { get; set; }

    public int Y { get; set; }

    public string? Team { get; set; }

    public bool Status { get; set; }

    public int ShipId { get; set; }

    public virtual Ship Ship { get; set; } = null!;
}
