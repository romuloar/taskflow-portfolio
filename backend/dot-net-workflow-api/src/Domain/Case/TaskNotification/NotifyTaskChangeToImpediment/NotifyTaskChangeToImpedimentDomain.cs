using Rom.Annotations;
using Rom.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Case.TaskNotification.NotifyTaskChangeToImpediment
{
    public class NotifyTaskChangeToImpedimentDomain : RomBaseDomain
    {
        [RequiredGuid]
        public Guid Id { get; set; }

        [RequiredString(ErrorMessage = "Description is required")]
        [MaxLength(50, ErrorMessage = "Description cannot exceed 50 characters")]
        public string Description { get; set; }
    }
}
