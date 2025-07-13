namespace ApiBackend.Models
{
    public enum TaskStatus { ToDo, InProgress, Done }
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public TaskStatus Status { get; set; } = TaskStatus.ToDo;
        public int? AssignedToUserId { get; set; }
        public User? AssignedToUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<TaskHistory> TaskHistories { get; set; } = new();
    }
}
