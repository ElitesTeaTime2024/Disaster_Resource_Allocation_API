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
    public class TrucksController : ControllerBase
    {
        private readonly ITruckService _truckService;

        public TrucksController(ITruckService truckService)
        {
            _truckService = truckService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<ResourceTruck>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<ErrorResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddTrucks([FromBody] List<ResourceTruck> trucks)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var createdTrucks = await _truckService.CreateTrucksAsync(trucks);
                return CreatedAtAction(nameof(GetAllTrucks), createdTrucks);
            }
            catch (Exception ex) {
                ErrorResponse errorResponse = new ErrorResponse();
                errorResponse.errorMsg = ex.Message;
                return StatusCode(500,errorResponse);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ResourceTruck>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<ErrorResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTrucks()
        {
            try
            {
                var trucks = await _truckService.GetAllTrucksAsync();
                return Ok(trucks);
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
                await _truckService.DeleteAllTrucksAsync();
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
