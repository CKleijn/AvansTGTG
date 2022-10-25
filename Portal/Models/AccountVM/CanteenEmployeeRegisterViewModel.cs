namespace Portal.Models.AccountVM
{
    public class CanteenEmployeeRegisterViewModel
    {
        [Required(ErrorMessage = "Voornaam is verplicht!")]
        [DisplayName("Voornaam")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Achternaam is verplicht!")]
        [DisplayName("Achternaam")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Personeelsnummer is verplicht!")]
        [DisplayName("Personeelsnummer")]
        public string? EmployeeNumber { get; set; }
        [Required(ErrorMessage = "Kantine is verplicht!")]
        [DisplayName("Kantine")]
        public Location? Location { get; set; }
        [Required(ErrorMessage = "Wachtwoord is verplicht!")]
        [RegularExpression("^.*(?=.{8,})(?=.*[\\d])(?=.*[\\W]).*$", ErrorMessage = "Je wachtwoord moet minimaal bestaan uit 8 karakters waarvan 1 cijfer en 1 speciale karakter!")]
        [DisplayName("Wachtwoord")]
        public string? Password { get; set; }
    }
}
