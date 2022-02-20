namespace TaskManagerWebAPI.Models
{
    /// <summary></summary>
    public enum TaskStatus
    {
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

    /// <summary>
    /// A response representing Task entity
    /// </summary>
    public class TaskResponse
    {
        /// <summary></summary>
        public Guid Id { get; set; }
        /// <summary></summary>
        public string Name { get; set; }
        /// <summary></summary>
        public TaskStatus Status { get; set; }
        /// <summary></summary>
        public string? Description { get; set; }
        /// <summary></summary>
        public int Priority { get; set; }
        /// <summary></summary>
        public Guid? ProjectId { get; set; }
    }
}
