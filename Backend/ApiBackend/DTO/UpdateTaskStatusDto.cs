namespace ApiBackend.DTO
{
    public class UpdateTaskStatusDto
    {
        public Models.TaskStatus NewStatus { get; set; }
        public int ChangedByUserId { get; set; }
    }
}
