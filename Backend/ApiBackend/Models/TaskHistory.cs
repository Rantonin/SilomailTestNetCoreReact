namespace ApiBackend.Models
{
    public class TaskHistory
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public Task Task { get; set; } = null!;
        public int ChangedByUserId { get; set; }
        public User ChangedByUser { get; set; } = null!;
        public string ChangeType { get; set; } = null!;
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public DateTime ChangeDate { get; set; }
    }
}
