using Portal.Models.AccountVM;

namespace Portal.Models.PacketVM
{
    public class PacketListViewModel
    {
        public UserViewModel? User { get; set; }
        public IEnumerable<PacketDetailViewModel>? PacketList { get; set; }
    }
}
