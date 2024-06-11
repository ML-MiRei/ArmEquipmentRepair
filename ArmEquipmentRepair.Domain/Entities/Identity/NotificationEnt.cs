using ArmEquipmentRepair.Domain.Entities.Common;

namespace ArmEquipmentRepair.Domain.Entities.Identity
{
    public class NotificationEnt : BaseEntity
    {
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
