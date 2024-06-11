using ArmEquipmentRepair.Domain.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace ArmEquipmentRepair.Domain.Entities.Common
{
    public class People : BaseEntity
    {
        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(25)]
        public string SecondName { get; set; }

        [Required]
        [StringLength(25)]
        public string LastName { get; set; }

        [Required]
        [PhoneNumber]
        public string PhoneNumber { get; set; }
    }
}
