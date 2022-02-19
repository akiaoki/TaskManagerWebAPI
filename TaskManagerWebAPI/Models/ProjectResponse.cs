namespace TaskManagerWebAPI.Models
{
    public enum ProjectStatus { NotStarted, Active, Completed }

    public class ProjectResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public ProjectStatus Status { get; set; }
        public int Priority { get; set; }
    }
}
