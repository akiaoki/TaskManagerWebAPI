using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerWebAPI.Entities
{
    /// <summary></summary>
    public enum TaskStatus { 
        /// <summary>
        /// Task is yet to be done
        /// </summary>
        ToDo,
        /// <summary>
        /// Task is in progress
        /// </summary>
        InProgress, 
        /// <summary>
        /// Task is done
        /// </summary>
        Done 
    }

    /// <summary></summary>
    public class Task : EntityBase
    {
        /// <summary></summary>
        public string Name { get; set; }
        /// <summary></summary>
        public TaskStatus Status { get; set; }
        /// <summary></summary>
        public string? Description { get; set; }
        /// <summary></summary>
        public int Priority { get; set; }

        /// <summary></summary>
        [ForeignKey("Project")]
        public Guid? ProjectId { get; set; }
        /// <summary></summary>
        public Project? Project { get; set; }

        /// <summary>
        /// Checks if the task has a project
        /// </summary>
        public bool HasProject() => Project != null;
    }
}
 