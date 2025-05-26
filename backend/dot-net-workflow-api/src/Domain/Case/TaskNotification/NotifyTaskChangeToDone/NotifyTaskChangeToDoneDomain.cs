using Rom.Annotations;
using Rom.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace Domain.Case.TaskNotification.NotifyTaskChangeToDone
{
    public class NotifyTaskChangeToDoneDomain : RomBaseDomain
    {
        /// <summary>
        /// Unique identifier for the task.
        /// </summary>
        [RequiredGuid]
        public Guid Id { get; set; }
        /// <summary>
        /// Description of the task to be added.
        /// </summary>
        [RequiredString(ErrorMessage = "Description is required")]
        [MaxLength(50, ErrorMessage = "Description cannot exceed 50 characters")]
        public string Description { get; set; }
        
    }
}
