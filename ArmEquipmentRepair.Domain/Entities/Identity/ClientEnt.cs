using ArmEquipmentRepair.Domain.Entities.Common;
using ArmEquipmentRepair.Domain.ValidationAttributes;

namespace ArmEquipmentRepair.Domain.Entities.Identity
{
    public class ClientEnt : People
    {
        [Email]
        public string? Email { get; set; }
    }
}
