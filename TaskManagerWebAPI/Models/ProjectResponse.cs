namespace TaskManagerWebAPI.Models
{
    /// <summary></summary>
    public enum ProjectStatus
    {
        /// <summary>
        /// Project was not started
        /// </summary>
        NotStarted,
        /// <summary>
        /// Project currently is active
        /// </summary>
        Active,
        /// <summary>
        /// Project currently is completed
        /// </summary>
        Completed
    }

    /// <summary>
    /// A response representing Project entity
    /// </summary>
    public class ProjectResponse
    {
        /// <summary></summary>
        public Guid Id { get; set; }
        /// <summary></summary>
        public string Name { get; set; }
        /// <summary></summary>
        public DateTime? StartDate { get; set; }
        /// <summary></summary>
        public DateTime? CompletionDate { get; set; }
        /// <summary></summary>
        public ProjectStatus Status { get; set; }
        /// <summary></summary>
        public int Priority { get; set; }
    }
}
