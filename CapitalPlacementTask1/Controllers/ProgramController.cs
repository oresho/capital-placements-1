using CapitalPlacementTask1.Models.Requests;
using CapitalPlacementTask1.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CapitalPlacementTask1.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProgramController : ControllerBase
    {
        private readonly IProgramService _programService;
        public ProgramController(IProgramService programService)
        {
            _programService = programService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Creates a new program", Description = "Creates a new program with the provided details.")]
        public async Task<IActionResult> CreateProgram([FromBody] CreateProgramRequest programRequest)
        {
            var response = await _programService.CreateProgramAsync(programRequest);
            return Ok(response); 
        }

        [HttpPut("{programId}")]
        [SwaggerOperation(Summary = "Updates an existing program", Description = "Updates the details of an existing program.")]
        public async Task<IActionResult> UpdateProgram(string programId, [FromBody] CreateProgramRequest programRequest)
        {
            var response = await _programService.UpdateProgramAsync(programId, programRequest);
            return Ok(response);
        }

        [HttpDelete("{programId}")]
        [SwaggerOperation(Summary = "Deletes a program", Description = "Deletes the program with the specified ID.")]
        public async Task<IActionResult> DeleteProgram(string programId)
        {
            var response = await _programService.DeleteProgramAsync(programId);
            return Ok(response);
        }

        [HttpGet("all")]
        [SwaggerOperation(Summary = "Retrieves all programs", Description = "Gets a list of all programs.")]
        public async Task<IActionResult> GetAllPrograms()
        {
            var response = await _programService.GetAllProgramsAsync();
            return Ok(response);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Retrieves programs by title", Description = "Gets programs that match the specified title.")]
        public async Task<IActionResult> GetProgramByTitle([FromQuery]string programTitle)
        {
            var response = await _programService.GetProgramsByProgramTitle(programTitle);
            return Ok(response);
        }

        [HttpGet("{programId}")]
        [SwaggerOperation(Summary = "Retrieves a program by ID", Description = "Gets the program with the specified ID.")]
        public async Task<IActionResult> GetProgramById(string programId)
        {
            var response = await _programService.GetProgramById(programId);
            return Ok(response);
        }
    }
}
