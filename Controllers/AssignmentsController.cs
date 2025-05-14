using Disaster_Resource_Allocation_API.Models;
using Disaster_Resource_Allocation_API.Models.Response;
using Disaster_Resource_Allocation_API.Models.Response.Error;
using Disaster_Resource_Allocation_API.Services;
using Disaster_Resource_Allocation_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Disaster_Resource_Allocation_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AssignmentsController : ControllerBase
    {
        private readonly IAssignmentService _assignmentService;

        public AssignmentsController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(List<Assignment>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<ErrorResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ProcessAssignments()
        {
            try
            {
                var assignmentResponse = await _assignmentService.ProcessAndAssignResourcesAsync();
                return Ok(assignmentResponse);
            }
            catch (Exception ex)
            {
                ErrorResponse errorResponse = new ErrorResponse();
                errorResponse.errorMsg = ex.Message;
                return StatusCode(500, errorResponse);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Assignment>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<ErrorResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLastAssignments()
        {
            try
            {
                var assignmentResponse = await _assignmentService.GetLastAssignmentsAsync();
                if (assignmentResponse == null)
                {
                    return NotFound("No Assignments Found. Please POST to Generate Assignments Again");
                }
                return Ok(assignmentResponse);
            }
            catch (Exception ex)
            {
                ErrorResponse errorResponse = new ErrorResponse();
                errorResponse.errorMsg = ex.Message;
                return StatusCode(500, errorResponse);
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IEnumerable<ErrorResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ClearAssignments()
        {
            try
            {
                await _assignmentService.ClearAssignmentsAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                ErrorResponse errorResponse = new ErrorResponse();
                errorResponse.errorMsg = ex.Message;
                return StatusCode(500, errorResponse);
            }
        }
    }
}
