using CapitalPlacementTask1.ExceptionHandler.CustomExceptions;
using CapitalPlacementTask1.Models.Entities;
using CapitalPlacementTask1.Models.Requests;
using CapitalPlacementTask1.Models.Responses;
using CapitalPlacementTask1.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CapitalPlacementTask1.Services
{
    public class ProgramService : IProgramService
    {
        private readonly IRepositoryService<ProgramEntity> _programRepository;
        public ProgramService(IRepositoryService<ProgramEntity> programRepository)
        {

            _programRepository = programRepository;

        }
        public async Task<GenericApiResponse> CreateProgramAsync(CreateProgramRequest createProgramRequest)
        {
            var program = await _programRepository.FindByCondition(x => (x.ProgramTitle == createProgramRequest.ProgramTitle) && (x.EmployerId == createProgramRequest.EmployerId)).FirstOrDefaultAsync();
            if (program != null)
            {
                throw new BadRequestException("A program with this title already exists.");
            }
            var programEntity = MapToProgramEntity(createProgramRequest); 
            await _programRepository.CreateAsync(programEntity);
            return new GenericApiResponse { Success = true, StatusCode = 200, Message = "Successfully Created new Program" };
        }

        public async Task<GenericApiResponse> UpdateProgramAsync(string programId, CreateProgramRequest updateProgramRequest)
        {
            var program = await _programRepository.FindByCondition(x => (x.ProgramId == programId) && (x.EmployerId == updateProgramRequest.EmployerId)).FirstOrDefaultAsync();
            if (program == null)
            {
                throw new ResourceNotFoundException("This program does not exist.");
            }

            program.ProgramTitle = updateProgramRequest.ProgramTitle;
            program.ProgramDescription = updateProgramRequest.ProgramDescription;
            program.DateUpdated = DateTime.Now;

            if (updateProgramRequest.PersonalInformation != null)
            {
                program.PersonalInformation = new PersonalInformationEntity
                {
                    ShowPhone = updateProgramRequest.PersonalInformation.ShowPhone,
                    PhoneIsInternal = updateProgramRequest.PersonalInformation.PhoneIsInternal,
                    ShowNationality = updateProgramRequest.PersonalInformation.ShowNationality,
                    NationalityIsInternal = updateProgramRequest.PersonalInformation.NationalityIsInternal,
                    ShowCurrentResidence = updateProgramRequest.PersonalInformation.ShowCurrentResidence,
                    CurrentResidenceIsInternal = updateProgramRequest.PersonalInformation.CurrentResidenceIsInternal,
                    ShowIDNumber = updateProgramRequest.PersonalInformation.ShowIDNumber,
                    IDNumberIsInternal = updateProgramRequest.PersonalInformation.IDNumberIsInternal,
                    ShowDateOfBirth = updateProgramRequest.PersonalInformation.ShowDateOfBirth,
                    DateOfBirthIsInternal = updateProgramRequest.PersonalInformation.DateOfBirthIsInternal,
                    ShowGender = updateProgramRequest.PersonalInformation.ShowGender,
                    GenderIsInternal = updateProgramRequest.PersonalInformation.GenderIsInternal,
                    CustomQuestions = updateProgramRequest.PersonalInformation.CustomQuestions.Select(cq => new CustomQuestionEntity
                    {
                        QuestionType = cq.QuestionType,
                        Question = cq.Question,
                        DropDownChoices = cq.DropDownChoices.ToList(),
                        EnableOtherDropDownChoice = cq.EnableOtherDropDownChoice,
                        MultipleChoices = cq.MultipleChoices.ToList(),
                        EnableOtherMultipleChoice = cq.EnableOtherMultipleChoice,
                        MaxChoice = cq.MaxChoice
                    }).ToList()
                };
            }
            program.ProgramId = programId;
            await _programRepository.UpdateAsync(program);
            return new GenericApiResponse<ProgramEntity> { Success = true, StatusCode = 200, Message = "Successfully Updated Program", Data = program };
        }

        public static ProgramEntity MapToProgramEntity(CreateProgramRequest request)
        {
            if (request == null)
                return null;

            var programEntity = new ProgramEntity
            {
                ProgramTitle = request.ProgramTitle,
                ProgramDescription = request.ProgramDescription,
                EmployerId = request.EmployerId,
                PersonalInformation = MapToPersonalInformationEntity(request.PersonalInformation)
            };

            return programEntity;
        }

        private static PersonalInformationEntity MapToPersonalInformationEntity(PersonalInformationDto personalInformationDto)
        {
            if (personalInformationDto == null)
                return null;

            var personalInformationEntity = new PersonalInformationEntity
            {
                ShowPhone = personalInformationDto.ShowPhone,
                PhoneIsInternal = personalInformationDto.PhoneIsInternal,
                ShowNationality = personalInformationDto.ShowNationality,
                NationalityIsInternal = personalInformationDto.NationalityIsInternal,
                ShowCurrentResidence = personalInformationDto.ShowCurrentResidence,
                CurrentResidenceIsInternal = personalInformationDto.CurrentResidenceIsInternal,
                ShowIDNumber = personalInformationDto.ShowIDNumber,
                IDNumberIsInternal = personalInformationDto.IDNumberIsInternal,
                ShowDateOfBirth = personalInformationDto.ShowDateOfBirth,
                DateOfBirthIsInternal = personalInformationDto.DateOfBirthIsInternal,
                ShowGender = personalInformationDto.ShowGender,
                GenderIsInternal = personalInformationDto.GenderIsInternal
            };


            // Map custom questions
            personalInformationEntity.CustomQuestions = personalInformationDto.CustomQuestions
                .Select(q => new CustomQuestionEntity
                {
                    QuestionType = q.QuestionType,
                    Question = q.Question,
                    DropDownChoices = q.DropDownChoices.ToList(),
                    EnableOtherDropDownChoice = q.EnableOtherDropDownChoice,
                    MultipleChoices = q.MultipleChoices.ToList(),
                    EnableOtherMultipleChoice = q.EnableOtherMultipleChoice,
                    MaxChoice = q.MaxChoice
                })
                .ToList();

            return personalInformationEntity;
        }

        public async Task<GenericApiResponse> GetAllProgramsAsync()
        {
            var applications = await _programRepository.GetAllAsync();
            return new GenericApiResponse<IEnumerable<ProgramEntity>> { Success = true, StatusCode = 200, Message = "Successfully Gotten all Programs", Data = applications };
        }

        public async Task<GenericApiResponse> GetProgramsByProgramTitle(string title)
        {
            var programs = await _programRepository.FindByCondition(x => x.ProgramTitle == title).ToListAsync();
            if(programs == null)
            {
                throw new ResourceNotFoundException("No programs with this title.");
            }
            return new GenericApiResponse<IEnumerable<ProgramEntity>> { Success = true, StatusCode = 200, Message = "Successfully Gotten Programs", Data = programs };
        }

        public async Task<GenericApiResponse> GetProgramById(string programId)
        {
            var program = await _programRepository.FindByCondition(x => x.ProgramId == programId).FirstOrDefaultAsync();
            if (program == null)
            {
                throw new ResourceNotFoundException("No program with this title.");
            }
            return new GenericApiResponse<ProgramEntity> { Success = true, StatusCode = 200, Message = "Successfully Gotten Program", Data = program };
        }

        public async Task<GenericApiResponse> DeleteProgramAsync(string programId)
        {
            var program = await _programRepository.FindByCondition(x => (x.ProgramId == programId)).FirstOrDefaultAsync();
            if (program == null)
            {
                throw new ResourceNotFoundException("This program does not exist.");
            }
            await _programRepository.DeleteAsync(program);
            return new GenericApiResponse { Success = true, StatusCode = 200, Message = "Successfully Deleted Program" };
        }
    }
}
