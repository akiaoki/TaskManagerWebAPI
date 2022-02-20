using System.ComponentModel.DataAnnotations;

namespace TaskManagerWebAPI.Models
{
    /// <summary>
    /// A request for project creation
    /// </summary>
    public class CreateProjectRequest
    {
        /// <remarks>Note that Name size should be between [3, 30]</remarks>
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }
        /// <summary></summary>
        public DateTime? StartDate { get; set; } = null;
        /// <summary></summary>
        public DateTime? CompletionDate { get; set; } = null;
        /// <summary></summary>
        public ProjectStatus Status { get; set; } = ProjectStatus.NotStarted;
        /// <summary></summary>
        public int Priority { get; set; } = 0;
    }
}
