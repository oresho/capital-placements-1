using CapitalPlacementTask1.Models.Requests;
using System.ComponentModel.DataAnnotations;

namespace CapitalPlacementTask1.Models.Entities
{
    public class ApplicationEntity
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required(ErrorMessage = "Firstname is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Lastname is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Nationality { get; set; }
        public string? CurrentResidence { get; set; }
        public string? IdNumber { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        [Required(ErrorMessage = "Program Id is required")]
        public string ProgramId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; }
        public IEnumerable<AdditionalInfoDto> AdditionalInfo { get; set; } = new List<AdditionalInfoDto>();
    }
}
