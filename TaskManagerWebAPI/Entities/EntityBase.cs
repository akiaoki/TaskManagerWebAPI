using System.ComponentModel.DataAnnotations;

namespace TaskManagerWebAPI.Entities
{
    public abstract class EntityBase
    {
        [Key]
        public Guid Id { get; set; }
    }
}
