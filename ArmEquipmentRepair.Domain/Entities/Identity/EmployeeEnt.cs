using ArmEquipmentRepair.Domain.Entities.Common;
using ArmEquipmentRepair.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ArmEquipmentRepair.Domain.Entities.Identity
{
    public class EmployeeEnt : People
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public EmployeeRoleEnum Role { get; set; }
    }
}
