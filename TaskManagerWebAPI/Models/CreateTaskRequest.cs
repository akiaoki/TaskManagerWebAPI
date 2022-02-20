using System.ComponentModel.DataAnnotations;

namespace TaskManagerWebAPI.Models
{
    /// <summary>
    /// A request for task creation
    /// </summary>
    public class CreateTaskRequest
    {
        /// <remarks>Note that Name size should be between [3, 30]</remarks>
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }
        /// <summary></summary>
        public TaskStatus Status { get; set; } = TaskStatus.ToDo;
        /// <summary></summary>
        public string? Description { get; set; } = null;
        /// <summary></summary>
        public int Priority { get; set; } = 0;
        /// <summary></summary>
        public Guid? ProjectId { get; set; } = null;
    }
}
