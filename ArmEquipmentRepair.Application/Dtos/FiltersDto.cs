using ArmEquipmentRepair.Domain.Enums;

namespace ArmEquipmentRepair.Application.Dtos
{
    public class FiltersDto
    {
        public string? SearchingEquipment { get; set; }
        public string? ClientLastName { get; set; }
        public TypeFaultEnum? TypeFault { get; set; }
        public RequestStatusEnum? Status { get; set; }

        public void Clear()
        {
            SearchingEquipment = null;
            ClientLastName = null;
            TypeFault = null;
            Status = null;
        }
    }
}
