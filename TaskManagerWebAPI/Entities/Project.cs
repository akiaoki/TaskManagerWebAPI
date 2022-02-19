using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TaskManagerWebAPI.Entities
{
    public enum ProjectStatus { NotStarted, Active, Completed }

    public class Project : EntityBase
    {
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public ProjectStatus Status { get; set; }
        public int Priority { get; set; }

        public IEnumerable<Entities.Task> Tasks { get; set; }

        public void AddTasks([DisallowNull] params Entities.Task[] tasks)
        {
            foreach (var task in tasks)
            {
                if (task == null)
                    throw new NullReferenceException($"Parameter {nameof(tasks)}'s elements cannot be null");

                task.Project = this;
            }
        }

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
