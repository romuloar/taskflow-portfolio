using Rom.Annotations;
using Rom.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace Workflow.Domain.Case.TaskNotification.NotifyTaskDeleted
{
    public class NotifyTaskDeletedDomain : RomBaseDomain
    {
        [RequiredGuid]
        public Guid Id { get; set; }

        [RequiredString(ErrorMessage = "Description is required")]
        [MaxLength(50, ErrorMessage = "Description cannot exceed 50 characters")]
        public string Description { get; set; }
    }
}
