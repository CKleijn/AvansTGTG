namespace Core.DomainServices.Services
{
    public class PacketService : IPacketService
    {
        private readonly IPacketRepository _packetRepository;

        public PacketService(IPacketRepository packetRepository)
        {
            _packetRepository = packetRepository;
        }

        public async Task<Packet> GetPacketByIdAsync(int packetId)
        {
            return await _packetRepository.GetPacketByIdAsync(packetId);
        }

        public async Task<IEnumerable<Packet>> GetPacketsAsync()
        {
            return await _packetRepository.GetPacketsAsync();
        }
        public async Task<IEnumerable<Packet>> GetCanteenSpecificPacketsAsync(Canteen canteen)
        {
            var allPackets = await _packetRepository.GetPacketsAsync();

            return allPackets.Where(p => p.Canteen == canteen);
        }

        public async Task<IEnumerable<Packet>> GetMyOfferedPacketsAsync(Canteen canteen)
        {
            var allPackets = await _packetRepository.GetPacketsAsync();

            return allPackets.Where(p => p.Canteen == canteen);
        }

        public async Task<IEnumerable<Packet>> GetMyReservedPacketsAsync(Student student)
        {
            var allPackets = await _packetRepository.GetPacketsAsync();

            return allPackets.Where(p => p.ReservedBy == student);
        }

        public async Task CreatePacketAsync(Packet packet)
        {
            await _packetRepository.CreatePacketAsync(packet);
        }

        public async Task ReservePacketAsync(Packet packet, Student student)
        {
            packet.ReservedBy = student;

            await _packetRepository.UpdatePacketAsync(packet);
        }

        public async Task UpdatePacketAsync(Packet newPacket)
        {
            await _packetRepository.UpdatePacketAsync(newPacket);
        }

        public async Task DeletePacketAsync(int packetId)
        {
            await _packetRepository.DeletePacketAsync(packetId);
        }
    }
}
