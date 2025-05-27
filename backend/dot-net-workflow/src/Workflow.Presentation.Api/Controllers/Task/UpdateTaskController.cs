using Microsoft.AspNetCore.Mvc;
using Workflow.Application.Case.Task.EditDescriptionTask.UpdateTask;
using Workflow.Domain.Case.Task.UpdateTask;

namespace Workflow.Presentation.Api.Controllers.Task
{
    [ApiController]
    [Route("api/[controller]")]
    public class UpdateTaskController : ControllerBase
    {
        private readonly IUpdateTaskApplication _updateTaskApplication;

        public UpdateTaskController(IUpdateTaskApplication updateTaskApplication)
        {
            _updateTaskApplication = updateTaskApplication;
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTaskDomain updateTask)
        {
            var result = await _updateTaskApplication.Execute(updateTask);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
