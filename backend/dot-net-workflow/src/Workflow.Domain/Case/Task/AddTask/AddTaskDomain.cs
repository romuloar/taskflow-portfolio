using Rom.Annotations;
using Rom.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace Workflow.Domain.Case.Task.AddTask
{
    /// <summary>
    /// Represents the domain model for adding a new task.
    /// </summary>
    public class AddTaskDomain : RomBaseDomain
    {
        /// <summary>
        /// Description of the task to be added.
        /// </summary>
        [RequiredString(ErrorMessage = "Description is required")]
        [MaxLength(50, ErrorMessage = "Description cannot exceed 50 characters")]
        public string Description { get; set; }
    }
}
