using Microsoft.AspNetCore.Mvc;
using Workflow.Application.Case.Task.GetAllTask;

namespace Workflow.Presentation.Api.Controllers.Task
{
    [ApiController]
    [Route("api/task/[controller]")]
    public class GetAllTaskController : ControllerBase
    {
        private readonly IGetAllTaskApplication _getAll;

        public GetAllTaskController(IGetAllTaskApplication getAll)
        {
            _getAll = getAll;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var result = await _getAll.ExecuteAsync();

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
