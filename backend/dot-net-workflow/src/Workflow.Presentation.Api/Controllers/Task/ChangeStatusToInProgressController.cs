using Microsoft.AspNetCore.Mvc;
using Workflow.Application.Case.Task.ChangeStatusToInProgress;

namespace Workflow.Presentation.Api.Controllers.Task
{
    [ApiController]
    [Route("api/task/[controller]")]
    public class ChangeStatusToInProgressController : ControllerBase
    {
        private readonly IChangeStatusToInProgressApplication _changeStatusToInProgress;

        public ChangeStatusToInProgressController(IChangeStatusToInProgressApplication changeStatusToInProgress)
        {
            _changeStatusToInProgress = changeStatusToInProgress;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Change(Guid id)
        {
            var result = await _changeStatusToInProgress.ExecuteAsync(id);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
