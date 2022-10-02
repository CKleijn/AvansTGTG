namespace Core.DomainServices.Interfaces
{
    public interface IPacketRepository
    {
        Task<Packet> GetPacketByIdAsync(int PacketId);
        Task<IEnumerable<Packet>> GetPacketsAsync();
        Task CreatePacketAsync(Packet packet);
        Task ReservePacketAsync(Packet packet, Student student);
        Task UpdatePacketAsync(Packet packet);
        Task DeletePacketAsync(int PacketId);
    }
}
