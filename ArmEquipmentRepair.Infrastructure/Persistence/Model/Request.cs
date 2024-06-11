using System;
using System.Collections.Generic;

namespace ArmEquipmentRepair.Infrastructure.Persistence.Model;

public partial class Request
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public string Equipment { get; set; } = null!;

    public int? TypeId { get; set; }

    public int? StatusId { get; set; }

    public int? ClientId { get; set; }

    public int? EmployeeId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? LastModify { get; set; }

    public virtual Client? Client { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual RequestStatus? Status { get; set; }

    public virtual TypesFault? Type { get; set; }
}
