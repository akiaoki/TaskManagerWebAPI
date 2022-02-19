using System.ComponentModel.DataAnnotations;

namespace TaskManagerWebAPI.Models
{
    public class CreateProjectRequest
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }
        public DateTime? StartDate { get; set; } = null;
        public DateTime? CompletionDate { get; set; } = null;
        public ProjectStatus Status { get; set; } = ProjectStatus.NotStarted;
        public int Priority { get; set; } = 0;
    }
}
