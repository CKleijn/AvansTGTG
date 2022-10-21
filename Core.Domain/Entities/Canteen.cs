namespace Core.Domain.Entities
{
    public class Canteen
    {
        public int CanteenId { get; set; }
        [Required(ErrorMessage = "Stad is verplicht!")]
        [DisplayName("Stad")]
        public Cities? City { get; set; }
        [Required(ErrorMessage = "Locatie is verplicht!")]
        [DisplayName("Kantine")]
        public Location? Location { get; set; }
        [Required(ErrorMessage = "Aangeven ofdat een kantine warme maaltijden verkoopt is verplicht!")]
        public bool? OfferingHotMeals { get; set; }
    }
}
