namespace Core.DomainServices.Interfaces.Services
{
    public interface IPacketService
    {
        Task<Packet> GetPacketByIdAsync(int packetId);
        Task<IEnumerable<Packet>> GetPacketsAsync();
        Task<IEnumerable<Packet>> GetMyReservedPacketsAsync(string studentNumber);
        Task<IEnumerable<Packet>> GetMyCanteenOfferedPacketsAsync(string employeeNumber);
        Task<IEnumerable<Packet>> GetSpecificCanteenPacketsAsync(Canteen canteen);
        Task<Packet> CreatePacketAsync(Packet packet, string employeeNumber, IList<string> products);
        Task<bool> ReservePacketAsync(int packetId, string studentNumber);
        //Task<bool> UpdatePacketAsync(int packetId, string employeeNumber, Packet newPacket);
        Task<bool> DeletePacketAsync(int packetId, string employeeNumber);
    }
}
