using Core.Domain.Enums;

namespace Portal.Models
{
    public class StudentRegisterViewModel
    {
        [Required(ErrorMessage = "Voornaam is verplicht!")]
        [DisplayName("Voornaam")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Achternaam is verplicht!")]
        [DisplayName("Achternaam")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Geboortedatum is verplicht!")]
        [DisplayName("Geboortedatum")]
        public DateTime? DateOfBirth { get; set; }
        [Required(ErrorMessage = "Studentennummer is verplicht!")]
        [DisplayName("Studentennummer")]
        public string? StudentNumber { get; set; }
        [Required(ErrorMessage = "Emailadres is verplicht!")]
        [EmailAddress(ErrorMessage = "Voer een geldig emailadres in!")]
        [DisplayName("Emailadres")]
        public string? EmailAddress { get; set; }
        [Required(ErrorMessage = "Studentenstad is verplicht!")]
        [DisplayName("Studentenstad")]
        public Cities? StudyCity { get; set; }
        [Required(ErrorMessage = "Telefoonnummer is verplicht!")]
        [Phone(ErrorMessage = "Voor een geldig telefoonnummer in!")]
        [DisplayName("Telefoonnummer")]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Wachtwoord is verplicht!")]
        [DisplayName("Wachtwoord")]
        public string? Password { get; set; }
    }
}
