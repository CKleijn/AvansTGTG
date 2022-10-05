namespace Core.DomainServices.Interfaces.Services
{
    public interface IPacketService
    {
        Task<Packet> GetPacketByIdAsync(int packetId);
        Task<IEnumerable<Packet>> GetPacketsAsync();
        Task<IEnumerable<Packet>> GetMyReservedPacketsAsync(Student student);
        Task<IEnumerable<Packet>> GetMyOfferedPacketsAsync(Canteen canteen);
        Task<IEnumerable<Packet>> GetCanteenSpecificPacketsAsync(Canteen canteen);
        Task CreatePacketAsync(Packet packet);
        Task ReservePacketAsync(Packet packet, Student student);
        Task UpdatePacketAsync(Packet newPacket);
        Task DeletePacketAsync(int packetId);
    }
}
