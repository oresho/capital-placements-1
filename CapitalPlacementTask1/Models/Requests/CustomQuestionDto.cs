using CapitalPlacementTask1.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace CapitalPlacementTask1.Models.Requests
{
    public class CustomQuestionDto
    {
        [Required(ErrorMessage = "Question Type is required")]
        [EnumDataType(typeof(QuestionType), ErrorMessage = "Invalid Question Type, Accepted types : 'PARAGRPH', 'YES_NO', 'DROPDOWN', 'DATE', 'NUMBER', 'MULTIPLE_CHOICE'")]
        public QuestionType QuestionType { get; set; } = QuestionType.PARAGRAPH;
        [Required(ErrorMessage = "Question is required")]
        public string Question { get; set; }
        // Drop-down meta data
        public IEnumerable<string> DropDownChoices { get; set; } = new List<string>();
        public bool EnableOtherDropDownChoice { get; set; }
        // Multiple-choice meta data
        public IEnumerable<string> MultipleChoices { get; set; } = new List<string>();
        public bool EnableOtherMultipleChoice { get; set; }
        public int MaxChoice { get; set; } = 0;
    }
}
