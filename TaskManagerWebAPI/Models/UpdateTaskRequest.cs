using System.ComponentModel.DataAnnotations;

namespace TaskManagerWebAPI.Models
{
    /// <summary>
    /// A request for task update
    /// </summary>
    public class UpdateTaskRequest
    {
        /// <summary></summary>
        [Required]
        public Guid Id { get; set; }
        /// <remarks>Note that Name size should be between [3, 30]</remarks>
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }
        /// <summary></summary>
        [Required]
        public TaskStatus Status { get; set; }
        /// <summary></summary>
        [Required]
        public string? Description { get; set; }
        /// <summary></summary>
        [Required]
        public int Priority { get; set; }
        /// <summary></summary>
        [Required]
        public Guid? ProjectId { get; set; }
    }
}
