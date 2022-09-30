namespace Core.Domain.Entities
{
    public class Canteen
    {
        public int CanteenId { get; set; }
        [Required(ErrorMessage = "Stad is verplicht!")]
        public Cities? City { get; set; }
        [Required(ErrorMessage = "Locatie is verplicht!")]
        public string? Location { get; set; }
        [Required(ErrorMessage = "Aangeven ofdat een kantine warme maaltijden verkoopt is verplicht!")]
        public bool? OfferingHotMeals { get; set; }
    }
}
