using System.ComponentModel.DataAnnotations;

namespace TaskManagerWebAPI.Models
{
    public class UpdateProjectRequest
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        public DateTime? StartDate { get; set; }
        [Required]
        public DateTime? CompletionDate { get; set; }
        [Required]
        public ProjectStatus Status { get; set; } = ProjectStatus.NotStarted;
        [Required]
        public int Priority { get; set; } = 0;
    }
}
