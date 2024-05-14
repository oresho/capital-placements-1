using System.ComponentModel.DataAnnotations;

namespace CapitalPlacementTask1.Models.Requests
{
    public class PersonalInformationDto : IValidatableObject
    {
        public bool ShowPhone { get; set; } = true;
        public bool PhoneIsInternal { get; set; }
        public bool ShowNationality { get; set; } = true;
        public bool NationalityIsInternal { get; set; }
        public bool ShowCurrentResidence { get; set; } = true;
        public bool CurrentResidenceIsInternal { get; set; }
        public bool ShowIDNumber { get; set; } = true; 
        public bool IDNumberIsInternal { get; set; }
        public bool ShowDateOfBirth { get; set; } = true;
        public bool DateOfBirthIsInternal { get; set; }
        public bool ShowGender { get; set; } = true;
        public bool GenderIsInternal { get; set; }
        public IEnumerable<CustomQuestionDto> CustomQuestions { get; set; } = new List<CustomQuestionDto>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            // Validate CustomQuestions
            if (CustomQuestions != null)
            {
                foreach(var customQuestion in CustomQuestions)
                {
                    var context = new ValidationContext(customQuestion, null, null);
                    Validator.TryValidateObject(customQuestion, context, results, true);
                }
            }

            return results;
        }

    }
}
