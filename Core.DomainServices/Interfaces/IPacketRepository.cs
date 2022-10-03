namespace Core.DomainServices.Interfaces
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
