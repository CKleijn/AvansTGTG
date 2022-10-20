namespace Core.Domain.Entities
{
    public class Packet
    {
        public int PacketId { get; set; }
        [Required(ErrorMessage = "Naam is verplicht!")]
        [DisplayName("Naam")]
        public string? Name { get; set; }
        [DisplayName("Producten")]
        public ICollection<Product>? Products { get; set; }
        [DisplayName("Stad")]
        public Cities? City { get; set; }
        [DisplayName("Kantine")]
        public Canteen? Canteen { get; set; }
        [Required(ErrorMessage = "Ophaaldatum en tijdstip is verplicht!")]
        [DisplayName("Ophaaldatum en tijdstip")]
        public DateTime? PickUpDateTime { get; set; }
        [Required(ErrorMessage = "Uiterlijke ophaal tijdstip is verplicht!")]
        [DisplayName("Uiterlijke ophaaldatum en tijdstip")]
        public DateTime? LatestPickUpTime { get; set; }
        [DisplayName("18+")]
        public bool? IsEightteenPlusPacket { get; set; }
        [Required(ErrorMessage = "Prijs is verplicht!")]
        [DisplayName("Prijs")]
        public double? Price { get; set; }
        [Required(ErrorMessage = "Maaltijd type is verplicht!")]
        [DisplayName("Maaltijd type")]
        public MealTypes? MealType { get; set; }
        [DisplayName("Gereserveerd door")]
        public Student? ReservedBy { get; set; }
    }
}
