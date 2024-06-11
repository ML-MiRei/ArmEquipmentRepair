namespace ArmEquipmentRepair.Application.Dtos
{
    public class PeopleDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string FormatedPhoneNumber { get; set; }
        public string? Email { get; set; } = null;

    }
}
