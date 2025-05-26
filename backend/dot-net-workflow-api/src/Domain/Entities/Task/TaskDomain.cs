using Domain.Generic.Task;
using Rom.Annotations;
using Rom.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Task
{
    /// <summary>
    /// Represents the domain model for a task entity.
    /// </summary>
    public class TaskDomain : RomBaseDomain
    {
        /// <summary>
        /// Unique identifier for the task.
        /// </summary>
        [RequiredGuid(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }

        /// <summary>
        /// Description of the task to be added.
        /// </summary>
        [RequiredString(ErrorMessage = "Description is required")]
        [MaxLength(50, ErrorMessage = "Description cannot exceed 50 characters")]
        public string Description { get; set; }

        /// <summary>
        /// Status of the task.
        /// </summary>
        public EnumTaskStatus Status { get; set; }
    }
}
