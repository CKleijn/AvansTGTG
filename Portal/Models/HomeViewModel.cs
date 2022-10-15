namespace Portal.Models
{
    public class HomeViewModel
    {
        public bool Student { get; set; }
        public bool CanteenEmployee { get; set; }
        public IEnumerable<Packet>? AllPackets { get; set; }
        public IEnumerable<Packet>? AllCanteenPackets { get; set; }
        public IEnumerable<Packet>? MyReservedPackets { get; set; }
    }
}
