namespace TaskManagerWebAPI.Models
{
    public class IndexedProjectResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; } = null;
        public DateTime? CompletionDate { get; set; } = null;
        public ProjectStatus Status { get; set; } = ProjectStatus.NotStarted;
        public int Priority { get; set; } = 0;
    }
}
