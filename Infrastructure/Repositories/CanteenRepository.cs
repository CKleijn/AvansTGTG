namespace Infrastructure.Repositories
{
    public class CanteenRepository : ICanteenRepository
    {
        private readonly ApplicationDbContext _context;
        public CanteenRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Canteen> GetCanteenByLocationAsync(Location canteenLocation)
        {
            var canteen = await _context.Canteens.FirstOrDefaultAsync(c => c.Location == canteenLocation);

            if (canteen != null)
                return canteen;

            throw new Exception($"Er bestaat geen kantine met de locatie {canteenLocation}!");
        }
        public async Task<IEnumerable<Canteen>> GetCanteensAsync() => await _context.Canteens.ToListAsync();
    }
}
