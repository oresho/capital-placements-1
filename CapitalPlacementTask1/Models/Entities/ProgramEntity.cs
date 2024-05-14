using CapitalPlacementTask1.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace CapitalPlacementTask1.Models.Entities
{
    public class ProgramEntity
    {
        [Key]
        public string ProgramId { get; set; } = Guid.NewGuid().ToString();
        public string ProgramTitle { get; set; }
        public string ProgramDescription { get; set; } = string.Empty;
        public string EmployerId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; }

        public PersonalInformationEntity PersonalInformation { get; set; }
    }

    public class PersonalInformationEntity
    {
        public bool ShowPhone { get; set; }

        public bool PhoneIsInternal { get; set; }

        public bool ShowNationality { get; set; }

        public bool NationalityIsInternal { get; set; }

        public bool ShowCurrentResidence { get; set; }

        public bool CurrentResidenceIsInternal { get; set; }

        public bool ShowIDNumber { get; set; }

        public bool IDNumberIsInternal { get; set; }

        public bool ShowDateOfBirth { get; set; }

        public bool DateOfBirthIsInternal { get; set; }

        public bool ShowGender { get; set; } 

        public bool GenderIsInternal { get; set; }

        public List<CustomQuestionEntity> CustomQuestions { get; set; } = new List<CustomQuestionEntity>();
    }

    public class CustomQuestionEntity
    {
        public QuestionType QuestionType { get; set; } = QuestionType.PARAGRAPH;
        public string Question { get; set; }

        public List<string> DropDownChoices { get; set; } = new List<string>();

        public bool EnableOtherDropDownChoice { get; set; }

        public List<string> MultipleChoices { get; set; } = new List<string>();

        public bool EnableOtherMultipleChoice { get; set; }
        public int MaxChoice { get; set; }
    }
}
