using Core.Domain.Enums;

namespace Portal.Models
{
    public class PacketDetailViewModel
    {
        public int PacketId { get; set; }
        public string? Name { get; set; }
        public ICollection<ProductViewModel>? Products { get; set; }
        [DisplayName("Stad")]
        public string? City { get; set; }
        [DisplayName("Kantine")]
        public string? Canteen { get; set; }
        [DisplayName("Ophaaldatum en tijdstip")]
        public string? PickUpDateTime { get; set; }
        [DisplayName("Uiterlijke ophaaldatum en tijdstip")]
        public string? LatestPickUpTime { get; set; }
        public string? IsEightteenPlusPacket { get; set; }
        public string? Price { get; set; }
        [DisplayName("Gereserveerd door")]
        public string? MealType { get; set; }
        [DisplayName("Gereserveerd door")]
        public string? ReservedBy { get; set; }
    }
}
