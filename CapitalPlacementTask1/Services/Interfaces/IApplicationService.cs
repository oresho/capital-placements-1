using CapitalPlacementTask1.Models.Requests;
using CapitalPlacementTask1.Models.Responses;

namespace CapitalPlacementTask1.Services.Interfaces
{
    public interface IApplicationService
    {
        Task<GenericApiResponse> SubmitApplicationAsync(SubmitApplicationRequest submitApplicationRequest);
        Task<GenericApiResponse> GetAllApplicationsAsync();
    }
}
