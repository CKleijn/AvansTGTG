namespace Infrastructure.Repositories
{
    public class PacketRepository : IPacketRepository
    {
        private readonly AvansDbContext _context;
        public PacketRepository(AvansDbContext context)
        {
            _context = context;
        }
        public async Task<Packet> GetPacketByIdAsync(int packetId)
        {
            var packet = await _context.Packets.FirstOrDefaultAsync(p => p.PacketId == packetId);

            if (packet != null)
            {
                return packet;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task<IEnumerable<Packet>> GetPacketsAsync() => await _context.Packets.ToListAsync();

        public async Task CreatePacketAsync(Packet packet)
        {
            var newPacket = await _context.Packets.AddAsync(packet);

            if (newPacket != null)
            {
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public async Task UpdatePacketAsync(Packet newPacket)
        {
            var packet = _context.Packets.Update(newPacket);

            if (packet != null)
            {
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException();
            }

        }
        public async Task DeletePacketAsync(int packetId)
        {
            var packet = await _context.Packets.FirstOrDefaultAsync(p => p.PacketId == packetId);

            if (packet != null)
            {
                _context.Packets.Remove(packet);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
