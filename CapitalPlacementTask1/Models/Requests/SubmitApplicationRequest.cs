using System.ComponentModel.DataAnnotations;

namespace CapitalPlacementTask1.Models.Requests
{
    public class SubmitApplicationRequest : IValidatableObject
    {
        [Required(ErrorMessage = "Firstname is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Lastname is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Nationality { get; set; }
        public string? CurrentResidence { get; set; }
        public string? IdNumber { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        [Required(ErrorMessage = "Program Id is required")]
        public string ProgramId { get; set; }
        public IEnumerable<AdditionalInfoDto> AdditionalInfo { get; set; } = new List<AdditionalInfoDto>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            // Validate AdditionalInfo
            if (AdditionalInfo != null)
            {
                foreach (var info in AdditionalInfo)
                {
                    var context = new ValidationContext(info, null, null);
                    Validator.TryValidateObject(info, context, results, true);
                }
            }

            return results;
        }
    }
}
