using System;
using System.Collections.Generic;

namespace ArmEquipmentRepair.Infrastructure.Persistence.Model;

public partial class Employee
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string SecondName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public int? RoleId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? LastModify { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    public virtual EmployeeRole? Role { get; set; }
}
