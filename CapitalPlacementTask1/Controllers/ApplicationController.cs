using CapitalPlacementTask1.Models.Requests;
using CapitalPlacementTask1.Services;
using CapitalPlacementTask1.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CapitalPlacementTask1.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Creates a new job application", Description = "Creates a new job application with the specified details.")]
        public async Task<IActionResult> SubmitApplication([FromBody] SubmitApplicationRequest submitApplicationRequest)
        {
            var response = await _applicationService.SubmitApplicationAsync(submitApplicationRequest);
            return Ok(response);
        }

        [HttpGet("all")]
        [SwaggerOperation(Summary = "Retrieves all applications", Description = "Gets a list of all applications.")]
        public async Task<IActionResult> GetAllApplications()
        {
            var response = await _applicationService.GetAllApplicationsAsync();
            return Ok(response);
        }
    }
}
