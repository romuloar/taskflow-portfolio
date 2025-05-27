using Rom.Annotations;
using Rom.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace Workflow.Domain.Case.Task.UpdateTask
{
    public class UpdateTaskDomain : RomBaseDomain
    {        
        [RequiredGuid(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }

        /// <summary>
        /// Description of the task to be added.
        /// </summary>
        [RequiredString(ErrorMessage = "Description is required")]
        [MaxLength(50, ErrorMessage = "Description cannot exceed 50 characters")]        
        public string Description { get; set; }
    }
}
