namespace ArmEquipmentRepair.Infrastructure.Persistence.Model;

public partial class Comment
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;

    public int? EmployeeId { get; set; }

    public int? RequestId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? LastModify { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Request? Request { get; set; }
}
