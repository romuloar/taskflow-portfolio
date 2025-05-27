using Microsoft.AspNetCore.Mvc;
using Workflow.Application.Case.Task.AddTask;
using Workflow.Domain.Case.Task.AddTask;

namespace Workflow.Presentation.Api.Controllers.Task
{
    [ApiController]
    [Route("api/task/[controller]")]
    public class AddTaskController : ControllerBase
    {
        private readonly IAddTaskApplication _addTaskApplication;

        public AddTaskController(IAddTaskApplication addTaskApplication)
        {
            _addTaskApplication = addTaskApplication;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddTaskDomain addTask)
        {
            var result = await _addTaskApplication.Execute(addTask);

            if (!result.IsSuccess)
                return BadRequest(result);

            return CreatedAtAction(nameof(Add), new { result });
        }
    }
}
