using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TaskManagerWebAPI.Entities
{
    /// <summary></summary>
    public enum ProjectStatus { 
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

    /// <summary></summary>
    public class Project : EntityBase
    {
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

        /// <summary></summary>
        public IEnumerable<Entities.Task> Tasks { get; set; }

        /// <summary>
        /// Adds several <see cref="Entities.Task"/> to this project
        /// </summary>
        /// <param name="tasks">Specified <see cref="Entities.Task"/>s to be added</param>
        /// <exception cref="NullReferenceException">Thrown if some of the tasks are null</exception>
        public void AddTasks([DisallowNull] params Entities.Task[] tasks)
        {
            foreach (var task in tasks)
            {
                if (task == null)
                    throw new NullReferenceException($"Parameter {nameof(tasks)}'s elements cannot be null");

                task.Project = this;
            }
        }

        /// <summary>
        /// Removes several <see cref="Entities.Task"/> to this project
        /// </summary>
        /// <param name="tasks">Specified <see cref="Entities.Task"/>s to be removed</param>
        /// <exception cref="NullReferenceException">Thrown if some of the tasks are null</exception>
        /// <exception cref="InvalidOperationException">Thrown if some of the tasks were not attached to this project</exception>
        public void RemoveTasks([DisallowNull] params Entities.Task[] tasks)
        {
            foreach (var task in tasks)
            {
                if (task == null)
                    throw new NullReferenceException($"Parameter {nameof(tasks)}'s elements cannot be null");

                if (task.Project != this)
                    throw new InvalidOperationException($"Parameter {nameof(tasks)}'s elements were not attached to this project");

                task.Project = null;
            }
        }
    }
}
