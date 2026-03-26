using System;
using System.Collections.Generic;

namespace TravelManagement.Api.Models;

public partial class Booking
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int RouteId { get; set; }

    public DateTime? BookingDate { get; set; }

    public virtual Travelroute Route { get; set; } = null!;

    public virtual SystemUser User { get; set; } = null!;
}
