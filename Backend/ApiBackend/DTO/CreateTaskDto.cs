namespace ApiBackend.DTO
{
    public class CreateTaskDto
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int? AssignedToUserId { get; set; }
        public int CreatedByUserId { get; set; }
    }
}
