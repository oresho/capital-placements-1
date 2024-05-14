using CapitalPlacementTask1.ExceptionHandler.CustomExceptions;
using CapitalPlacementTask1.Models.Entities;
using CapitalPlacementTask1.Models.Requests;
using CapitalPlacementTask1.Models.Responses;
using CapitalPlacementTask1.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CapitalPlacementTask1.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IRepositoryService<ProgramEntity> _programRepository;
        private readonly IRepositoryService<ApplicationEntity> _applicationRepository;
        public ApplicationService(IRepositoryService<ProgramEntity> programRepository, IRepositoryService<ApplicationEntity> applicationRepository)
        {

            _programRepository = programRepository;
            _applicationRepository = applicationRepository;
        }
        public async Task<GenericApiResponse> SubmitApplicationAsync(SubmitApplicationRequest submitApplicationRequest)
        {
            var program = await _programRepository.FindByCondition(x => (x.ProgramId == submitApplicationRequest.ProgramId)).FirstOrDefaultAsync();
            if (program == null)
            {
                throw new ResourceNotFoundException("Current program no longer exists.");
            }
            var application = MapToApplicationEntity(submitApplicationRequest);
            await _applicationRepository.CreateAsync(application);
            return new GenericApiResponse { Success = true, StatusCode = 200, Message = "Successfully Created new Application" };
        }

        public static ApplicationEntity MapToApplicationEntity(SubmitApplicationRequest request)
        {
            return new ApplicationEntity
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                Nationality = request.Nationality,
                CurrentResidence = request.CurrentResidence,
                IdNumber = request.IdNumber,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                ProgramId = request.ProgramId,
                AdditionalInfo = request.AdditionalInfo.Select(info => new AdditionalInfoDto
                {
                    Question = info.Question,
                    Answer = info.Answer
                }).ToList()
            };
        }

        public async Task<GenericApiResponse> GetAllApplicationsAsync()
        {
            var applications = await _applicationRepository.GetAllAsync();
            applications.OrderByDescending(x => x.DateCreated);
            return new GenericApiResponse<IEnumerable<ApplicationEntity>> { Success = true, StatusCode = 200, Message = "Successfully Gotten all Applications", Data = applications };
        }
    }
}
