using System.ComponentModel.DataAnnotations;

namespace TaskManagerWebAPI.Entities
{
    /// <summary>
    /// Represents a base for entities adding Id field
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary>
        /// <see cref="Guid"/> of this entity
        /// </summary>
        [Key]
        public Guid Id { get; set; }
    }
}
