namespace ApiBackend.DTO
{
    public class AssignTaskDto
    {
        public int? AssignedToUserId { get; set; }
        public int ChangedByUserId { get; set; }
    }
}
