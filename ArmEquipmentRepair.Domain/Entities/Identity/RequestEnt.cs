using ArmEquipmentRepair.Domain.Entities.Common;
using ArmEquipmentRepair.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ArmEquipmentRepair.Domain.Entities.Identity
{
    public class RequestEnt : BaseEntity
    {
        [Required]
        [StringLength(300)]
        public string Description { get; set; }

        [Required]
        public TypeFaultEnum TypeFault { get; set; }

        [Required]
        public RequestStatusEnum Status { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public string Equipment { get; set; }

    }
}
