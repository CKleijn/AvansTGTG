namespace Core.Domain.Entities
{
    public class CanteenEmployee
    {
        public int CanteenEmployeeId { get; set; }
        [Required(ErrorMessage = "Naam is verplicht!")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Personeelsnummer is verplicht!")]
        public string? EmployeeNumber { get; set; }
        [Required(ErrorMessage = "Locatie is verplicht!")]
        public string? Location { get; set; }
    }
}
