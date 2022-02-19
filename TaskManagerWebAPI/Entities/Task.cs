using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerWebAPI.Entities
{
    public enum TaskStatus { ToDo, InProgress, Done }

    public class Task : EntityBase
    {
        public string Name { get; set; }
        public TaskStatus Status { get; set; }
        public string? Description { get; set; }
        public int Priority { get; set; }

        [ForeignKey("Project")]
        public Guid? ProjectId { get; set; }
        public Project? Project { get; set; }

        public bool HasProject() => Project != null;
    }
}
