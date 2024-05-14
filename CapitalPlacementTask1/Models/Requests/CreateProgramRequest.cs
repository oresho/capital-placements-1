using System.ComponentModel.DataAnnotations;

namespace CapitalPlacementTask1.Models.Requests
{
    public class CreateProgramRequest : IValidatableObject
    {
        [Required(ErrorMessage = "Program Title is required")]
        public string ProgramTitle { get; set; } = string.Empty;
        [Required(ErrorMessage = "Program Description is required")]
        public string ProgramDescription { get; set; } = string.Empty;
        [Required(ErrorMessage = "EmployerId is required")]
        public string EmployerId { get; set; }
        public PersonalInformationDto PersonalInformation { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            // Validate PersonalInformationDto
            if (PersonalInformation != null)
            {
                var context = new ValidationContext(PersonalInformation, null, null);
                Validator.TryValidateObject(PersonalInformation, context, results, true);
            }

            return results;
        }
    }
}
