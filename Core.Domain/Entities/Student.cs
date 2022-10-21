namespace Core.Domain.Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Naam is verplicht!")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Geboortedatum is verplicht!")]
        public DateTime? DateOfBirth { get; set; }
        [Required(ErrorMessage = "Studentennummer is verplicht!")]
        public string? StudentNumber { get; set; }
        [Required(ErrorMessage = "Emailadres is verplicht!")]
        [EmailAddress(ErrorMessage = "Voer een geldig emailadres in!")]
        public string? EmailAddress { get; set; }
        [Required(ErrorMessage = "Studentenstad is verplicht!")]
        public Cities? StudyCity { get; set; }
        [Required(ErrorMessage = "Telefoonnummer is verplicht!")]
        [Phone(ErrorMessage = "Voor een geldig telefoonnummer in!")]
        public string? PhoneNumber { get; set; }
    }
}
