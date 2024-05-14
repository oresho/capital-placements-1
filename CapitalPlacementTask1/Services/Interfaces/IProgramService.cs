using CapitalPlacementTask1.Models.Requests;
using CapitalPlacementTask1.Models.Responses;

namespace CapitalPlacementTask1.Services.Interfaces
{
    public interface IProgramService
    {
        Task<GenericApiResponse> CreateProgramAsync(CreateProgramRequest createProgramRequest);
        Task<GenericApiResponse> UpdateProgramAsync(string programId, CreateProgramRequest updateProgramRequest);
        Task<GenericApiResponse> GetAllProgramsAsync();
        Task<GenericApiResponse> GetProgramsByProgramTitle(string title);
        Task<GenericApiResponse> GetProgramById(string programId);
        Task<GenericApiResponse> DeleteProgramAsync(string programId);
    }
}
