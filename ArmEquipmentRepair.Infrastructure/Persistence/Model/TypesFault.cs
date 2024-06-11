using System;
using System.Collections.Generic;

namespace ArmEquipmentRepair.Infrastructure.Persistence.Model;

public partial class TypesFault
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
