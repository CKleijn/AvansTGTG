namespace Infrastructure.Repositories
{
    public class PacketRepository : IPacketRepository
    {
        private readonly ApplicationDbContext _context;
        public PacketRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Packet?> GetPacketByIdAsync(int packetId) => await _context.Packets.Include(p => p.Products).Include(p => p.Canteen).Include(p => p.ReservedBy).FirstOrDefaultAsync(p => p.PacketId == packetId);

        public async Task<IEnumerable<Packet>> GetPacketsAsync() => await _context.Packets.Include(p => p.Products).Include(p => p.Canteen).Include(p => p.ReservedBy).OrderBy(p => p.PickUpDateTime).ToListAsync();

        public async Task<bool> CreatePacketAsync(Packet packet)
        {
            if (packet == null)
                return false;

            await _context.Packets.AddAsync(packet);
            await _context.SaveChangesAsync();
            
            return true;
        }

        public async Task<bool> UpdatePacketAsync(int packetId)
        {
            var packet = await GetPacketByIdAsync(packetId);

            if (packet == null)
                return false;

            _context.Attach(packet).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> DeletePacketAsync(int packetId)
        {
            var packet = await GetPacketByIdAsync(packetId);

            if (packet == null)
                return false;

            _context.Packets.Remove(packet);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
