using System.ComponentModel.DataAnnotations;

namespace TaskManagerWebAPI.Models
{
    /// <summary>
    /// A request for project update
    /// </summary>
    public class UpdateProjectRequest
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
        public DateTime? StartDate { get; set; }
        /// <summary></summary>
        [Required]
        public DateTime? CompletionDate { get; set; }
        /// <summary></summary>
        [Required]
        public ProjectStatus Status { get; set; } = ProjectStatus.NotStarted;
        [Required]
        /// <summary></summary>
        public int Priority { get; set; } = 0;
    }
}
