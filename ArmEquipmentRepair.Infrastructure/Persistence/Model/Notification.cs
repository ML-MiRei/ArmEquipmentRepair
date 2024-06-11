using System;
using System.Collections.Generic;

namespace ArmEquipmentRepair.Infrastructure.Persistence.Model;

public partial class Notification
{
    public int Id { get; set; }

    public string Text { get; set; } = null!;

    public int? EmployeeId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual Employee? Employee { get; set; }
}
