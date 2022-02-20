namespace TaskManagerWebAPI.Models
{
    public enum TaskStatus { ToDo, InProgress, Done }

    public class TaskResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TaskStatus Status { get; set; }
        public string? Description { get; set; }
        public int Priority { get; set; }
        public Guid? ProjectId { get; set; }
    }
}
