using Microsoft.AspNetCore.Mvc;
using Workflow.Application.Case.Task.ChangeStatusToImpediment;

namespace Workflow.Presentation.Api.Controllers.Task
{
    [ApiController]
    [Route("api/task/[controller]")]
    public class ChangeStatusToImpedimentController : ControllerBase
    {
        private readonly IChangeStatusToImpedimentApplication _changeStatusToImpediment;

        public ChangeStatusToImpedimentController(IChangeStatusToImpedimentApplication changeStatusToImpediment)
        {
            _changeStatusToImpediment = changeStatusToImpediment;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Change(Guid id)
        {
            var result = await _changeStatusToImpediment.ExecuteAsync(id);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
