using System;
using System.Collections.Generic;

namespace TravelManagement.Api.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<SystemUser> Users { get; set; } = new List<SystemUser>();
}
