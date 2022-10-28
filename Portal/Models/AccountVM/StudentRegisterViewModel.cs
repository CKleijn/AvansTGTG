namespace Portal.Models.AccountVM
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
        [StudentExtensions.DateOfBirthValidation]
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
        [RegularExpression("^.*(?=.{8,})(?=.*[\\d])(?=.*[\\W]).*$", ErrorMessage = "Je wachtwoord moet minimaal bestaan uit 8 karakters waarvan 1 cijfer en 1 speciale karakter!")]
        [DisplayName("Wachtwoord")]
        public string? Password { get; set; }
    }
}
