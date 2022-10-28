namespace Core.DomainServices.Interfaces.Services
{
    public interface IPacketService
    {
        Task<Packet> GetPacketByIdAsync(int packetId);
        Task<IEnumerable<Packet>> GetPacketsAsync();
        Task<IEnumerable<Packet>> GetAllAvailablePacketsAsync();
        Task<IEnumerable<Packet>> GetMyReservedPacketsAsync(string studentNumber);
        Task<IEnumerable<Packet>> GetMyCanteenOfferedPacketsAsync(string employeeNumber);
        Task<IEnumerable<Packet>> GetOtherCanteenOfferedPacketsAsync(string employeeNumber);
        Task<Packet> CreatePacketAsync(Packet packet, string employeeNumber, IList<string> products);
        Task<bool> ReservePacketAsync(int packetId, string studentNumber);
        Task<bool> UpdatePacketAsync(int packetId, Packet newPacket, string employeeNumber, IList<string> products);
        Task<bool> DeletePacketAsync(int packetId, string employeeNumber);
        Task<bool> CheckReservedPickUpDate(Student student, Packet packet);
    }
}
