using ArmEquipmentRepair.Domain.Entities.Identity;
using ArmEquipmentRepair.Domain.Enums;
using System.Collections.ObjectModel;

namespace ArmEquipmentRepair.Application.Dtos
{
    public class RequestFullInfoDto
    {
        public int RequestId { get; set; }
        public string Description { get; set; }
        public TypeFaultEnum Type { get; set; }
        public RequestStatusEnum Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Equipment { get; set; }
        public PeopleDTO Client { get; set; }
        public PeopleDTO? Executor { get; set; }
        public ObservableCollection<CommentDto> Comments { get; set; }
    }
}
