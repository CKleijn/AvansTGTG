namespace Infrastructure.Repositories
{
    public class PacketRepository : IPacketRepository
    {
        private readonly ApplicationDbContext _context;
        public PacketRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Packet> GetPacketByIdAsync(int packetId)
        {
            var packet = await _context.Packets.Include(p => p.Products).Include(p => p.Canteen).Include(p => p.ReservedBy).FirstOrDefaultAsync(p => p.PacketId == packetId);

            if (packet != null)
            {
                return packet;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task<IEnumerable<Packet>> GetPacketsAsync() => await _context.Packets.Include(p => p.Products).Include(p => p.Canteen).Include(p => p.ReservedBy).ToListAsync();

        public async Task<Packet> CreatePacketAsync(Packet packet)
        {
            var newPacket = await _context.Packets.AddAsync(packet);

            if (newPacket != null)
            {
                await _context.SaveChangesAsync();
                return packet;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public async Task<bool> UpdatePacketAsync(Packet newPacket)
        {
            var packet = _context.Packets.Update(newPacket);

            if (packet != null)
            {
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<bool> DeletePacketAsync(int packetId)
        {
            var packet = await _context.Packets.Include(p => p.Products).Include(p => p.Canteen).Include(p => p.ReservedBy).FirstOrDefaultAsync(p => p.PacketId == packetId);

            if (packet != null)
            {
                _context.Packets.Remove(packet);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
