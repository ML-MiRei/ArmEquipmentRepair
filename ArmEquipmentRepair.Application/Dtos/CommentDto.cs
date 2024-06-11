namespace ArmEquipmentRepair.Application.Dtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsCreator { get; set; }
        public string CreatorName { get; set; }
    }
}
