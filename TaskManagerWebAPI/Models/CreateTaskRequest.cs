using System.ComponentModel.DataAnnotations;

namespace TaskManagerWebAPI.Models
{
    public class CreateTaskRequest
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }
        public TaskStatus Status { get; set; } = TaskStatus.ToDo;
        public string? Description { get; set; } = null;
        public int Priority { get; set; } = 0;
        public Guid? ProjectId { get; set; } = null;
    }
}
