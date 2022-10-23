using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebAPI.Models
{
    public class AuthCredentials
    {
        [Required(ErrorMessage = "Identificatienummer is verplicht!")]
        public string? IdentificationNumber { get; set; }
        [Required(ErrorMessage = "Wachtwoord is verplicht!")]
        public string? Password { get; set; }
    }
}
