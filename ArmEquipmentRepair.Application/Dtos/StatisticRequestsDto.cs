using ArmEquipmentRepair.Domain.Enums;

namespace ArmEquipmentRepair.Application.Dtos
{
    public class StatisticRequestsDto
    {
        public int Count { get; set; }
        public int CountSuccsess { get; set; }
        public int CountInProcess{ get; set; }

        //in days

        public double AverageTime { get; set; }
        public Dictionary<TypeFaultEnum, int> CountByTypeFaults { get; set; } = new Dictionary<TypeFaultEnum, int> { };




    }
}
