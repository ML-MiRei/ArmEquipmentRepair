using ArmEquipmentRepair.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace ArmEquipmentRepair.Domain.Entities.Identity
{
    public class CommentEnt : BaseEntity
    {
        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public int RequestId { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
    }
}
