namespace WebAPI.GraphQL
{
    public class Query
    {
        public async Task<IEnumerable<Packet>> Packets(IPacketService packetService) => await packetService.GetPacketsAsync();
    }
}
