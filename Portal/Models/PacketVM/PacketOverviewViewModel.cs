using Portal.Models.AccountVM;

namespace Portal.Models.PacketVM
{
    public class PacketOverviewViewModel
    {
        public UserViewModel? User { get; set; }
        public IEnumerable<PacketDetailViewModel>? AllPackets { get; set; }
        public IEnumerable<PacketDetailViewModel>? AllCanteenPackets { get; set; }
        public IEnumerable<PacketDetailViewModel>? MyReservedPackets { get; set; }
    }
}
