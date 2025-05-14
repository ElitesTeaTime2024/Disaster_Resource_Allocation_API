using Disaster_Resource_Allocation_API.Models;
using Disaster_Resource_Allocation_API.Models.Response.Error;
using Disaster_Resource_Allocation_API.Repositories.Interface;
using Disaster_Resource_Allocation_API.Services;
using Disaster_Resource_Allocation_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Disaster_Resource_Allocation_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AreasController : ControllerBase
    {
        private readonly IAreaService _areaService;

        public AreasController(IAreaService areaService)
        {
            _areaService = areaService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<AffectedArea>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<ErrorResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAreas([FromBody] List<AffectedArea> areas)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var createdAreas = await _areaService.CreateAreasAsync(areas);
                return CreatedAtAction(nameof(GetAllAreas), createdAreas);
            }
            catch (Exception ex)
            {
                ErrorResponse errorResponse = new ErrorResponse();
                errorResponse.errorMsg = ex.Message;
                return StatusCode(500, errorResponse);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AffectedArea>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<ErrorResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAreas()
        {
            try
            {
                var areas = await _areaService.GetAllAreasAsync();
                return Ok(areas);
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
        public async Task<IActionResult> DeleteAllTrucks()
        {
            try
            {
                await _areaService.DeleteAllAreasAsync();
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
