using Microsoft.AspNetCore.Mvc;
using Workflow.Application.Case.Task.ChangeStatusToDone;

namespace Workflow.Presentation.Api.Controllers.Task
{
    [ApiController]
    [Route("api/task/[controller]")]
    public class ChangeStatusToDoneController : ControllerBase
    {
        private readonly IChangeStatusToDoneApplication _changeStatusToDone;

        public ChangeStatusToDoneController(IChangeStatusToDoneApplication changeStatusToDone)
        {
            _changeStatusToDone = changeStatusToDone;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Change(Guid id)
        {
            var result = await _changeStatusToDone.ExecuteAsync(id);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
