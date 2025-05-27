using Microsoft.AspNetCore.Mvc;
using Workflow.Application.Case.Task.DeleteTask;

namespace Workflow.Presentation.Api.Controllers.Task
{
    [ApiController]
    [Route("api/task/[controller]")]
    public class DeleteTaskController : ControllerBase
    {
        private readonly IDeleteTaskApplication _deleteTaskApplication;

        public DeleteTaskController(IDeleteTaskApplication deleteTaskApplication)
        {
            _deleteTaskApplication = deleteTaskApplication;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _deleteTaskApplication.ExecuteAsync(id);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
