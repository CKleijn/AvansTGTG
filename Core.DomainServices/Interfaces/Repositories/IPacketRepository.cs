namespace Core.DomainServices.Interfaces.Repositories
{
    public interface IPacketRepository
    {
        Task<Packet> GetPacketByIdAsync(int packetId);
        Task<IEnumerable<Packet>> GetPacketsAsync();
        Task CreatePacketAsync(Packet packet);
        Task UpdatePacketAsync(Packet newPacket);
        Task DeletePacketAsync(int packetId);
    }
}
