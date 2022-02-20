using System.ComponentModel.DataAnnotations;

namespace TaskManagerWebAPI.Models
{
    public class UpdateTaskRequest
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        public TaskStatus Status { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public int Priority { get; set; }
        [Required]
        public Guid? ProjectId { get; set; }
    }
}
