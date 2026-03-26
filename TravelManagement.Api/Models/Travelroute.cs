using System;
using System.Collections.Generic;

namespace TravelManagement.Api.Models;

public partial class Travelroute
{
    public int Id { get; set; }

    public string Source { get; set; } = null!;

    public string Destination { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
