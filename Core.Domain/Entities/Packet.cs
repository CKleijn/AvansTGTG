namespace Core.Domain.Entities
{
    public class Packet
    {
        public int PacketId { get; set; }
        [Required(ErrorMessage = "Naam is verplicht!")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Producten zijn verplicht!")]
        public ICollection<Product>? Products { get; set; }
        [Required(ErrorMessage = "Stad is verplicht!")]
        public Cities? City { get; set; }
        [Required(ErrorMessage = "Kantine is verplicht!")]
        public Canteen? Canteen { get; set; }
        [Required(ErrorMessage = "Ophaaldatum en tijdstip is verplicht!")]
        public DateTime? PickUpDateTime { get; set; }
        [Required(ErrorMessage = "Uiterlijke ophaal tijdstip is verplicht!")]
        public DateTime? LatestPickUpTime { get; set; }
        [Required(ErrorMessage = "Het aangeven van ofdat het pakket 18+ is is verplicht!")]
        public bool? IsEightteenPlusPacket { get; set; }
        [Required(ErrorMessage = "Prijs is verplicht!")]
        public double? Price { get; set; }
        [Required(ErrorMessage = "Maaltijd type is verplicht!")]
        public MealTypes? MealType { get; set; }
        public Student? ReservedBy { get; set; }
    }
}
