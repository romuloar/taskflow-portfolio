using Microsoft.AspNetCore.Mvc;
using Workflow.Application.Case.Task.GetTaskById;

namespace Workflow.Presentation.Api.Controllers.Task
{
    [ApiController]
    [Route("api/task/[controller]")]
    public class GetTaskByIdController : ControllerBase
    {
        private readonly IGetTaskByIdApplication _getTaskApplication;

        public GetTaskByIdController(IGetTaskByIdApplication getTaskApplication)
        {
            _getTaskApplication = getTaskApplication;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _getTaskApplication.Execute(id);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }
    }
}
