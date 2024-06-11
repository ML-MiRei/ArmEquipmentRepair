using System;
using System.Collections.Generic;

namespace ArmEquipmentRepair.Infrastructure.Persistence.Model;

public partial class Client
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string SecondName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string? Email { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? LastModify { get; set; }

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
