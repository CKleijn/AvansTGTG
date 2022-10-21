namespace Core.DomainServices.Interfaces.Repositories
{
    public interface IPacketRepository
    {
        Task<Packet> GetPacketByIdAsync(int packetId);
        Task<IEnumerable<Packet>> GetPacketsAsync();
        Task<Packet> CreatePacketAsync(Packet packet);
        Task<bool> UpdatePacketAsync(int packetId);
        Task<bool> DeletePacketAsync(int packetId);
    }
}
