using Microsoft.AspNetCore.Mvc.Rendering;

namespace Portal.Models
{
    public class PacketViewModel
    {
        public Packet Packet { get; set; } = null!;
        public IList<string>? SelectedProducts { get; set; }
        public IList<SelectListItem>? AllProducts { get; set; } 

        public PacketViewModel()
        {
            SelectedProducts = new List<string>();
            AllProducts = new List<SelectListItem>();
        }
    }
}
