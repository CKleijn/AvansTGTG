using Core.Domain.Entities;
using Infrastructure.Repositories;

namespace WebAPI.GraphQL
{
    public class Query
    {
        public async Task<IEnumerable<Packet>> Packets(PacketRepository packetRepository) => await packetRepository.GetPacketsAsync();
    }
}
