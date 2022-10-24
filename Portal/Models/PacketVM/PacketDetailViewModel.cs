namespace Portal.Models.PacketVM
{
    public class PacketDetailViewModel
    {
        public int PacketId { get; set; }
        [DisplayName("Naam")]
        public string? Name { get; set; }
        [DisplayName("Alle producten")]
        public ICollection<ProductViewModel>? Products { get; set; }
        [DisplayName("Stad")]
        public string? City { get; set; }
        [DisplayName("Kantine")]
        public string? Canteen { get; set; }
        [DisplayName("Ophaaldatum en tijdstip")]
        public string? PickUpDateTime { get; set; }
        [DisplayName("Uiterlijke ophaaldatum en tijdstip")]
        public string? LatestPickUpTime { get; set; }
        [DisplayName("18+ pakket?")]
        public string? IsEightteenPlusPacket { get; set; }
        [DisplayName("Prijs")]
        public string? Price { get; set; }
        [DisplayName("Maaltijd type")]
        public string? MealType { get; set; }
        [DisplayName("Gereserveerd door")]
        public string? ReservedBy { get; set; }
        public string? StudentName { get; set; }
    }
}
