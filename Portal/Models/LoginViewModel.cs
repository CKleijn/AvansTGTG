namespace Portal.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Identificatienummer is verplicht!")]
        [DisplayName("Identificatienummer")]
        public string? IdentificationNumber { get; set; }
        [Required(ErrorMessage = "Wachtwoord is verplicht!")]
        [DisplayName("Wachtwoord")]
        public string? Password { get; set; }
    }
}
