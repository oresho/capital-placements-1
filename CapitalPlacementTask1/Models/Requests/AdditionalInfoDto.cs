using System.ComponentModel.DataAnnotations;

namespace CapitalPlacementTask1.Models.Requests
{
    public class AdditionalInfoDto
    {
        [Required(ErrorMessage = "Question is required")]
        public string Question { get; set; }
        [Required(ErrorMessage = "Answer is required")]
        public string Answer { get; set; }
    }
}
