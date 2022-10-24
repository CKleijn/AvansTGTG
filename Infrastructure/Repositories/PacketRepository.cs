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
                return packet;

            throw new Exception($"Er bestaat geen pakket met de id {packetId}!");
        }

        public async Task<IEnumerable<Packet>> GetPacketsAsync() => await _context.Packets.Include(p => p.Products).Include(p => p.Canteen).Include(p => p.ReservedBy).OrderBy(p => p.PickUpDateTime).ToListAsync();

        public async Task<Packet> CreatePacketAsync(Packet packet)
        {
            if (packet != null)
            {
                await _context.Packets.AddAsync(packet);
                await _context.SaveChangesAsync();
                return packet;
            }

            throw new Exception("Het meegegeven pakket is fout!");
        }

        public async Task<bool> UpdatePacketAsync(int packetId)
        {
            var packet = await GetPacketByIdAsync(packetId);

            if (packet != null)
            {
                _context.Attach(packet).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
        public async Task<bool> DeletePacketAsync(int packetId)
        {
            var packet = await GetPacketByIdAsync(packetId);

            if (packet != null)
            {
                _context.Packets.Remove(packet);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
