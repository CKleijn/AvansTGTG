namespace Infrastructure.Repositories
{
    public class CanteenRepository : ICanteenRepository
    {
        private readonly AvansDbContext _context;
        public CanteenRepository(AvansDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Canteen>> GetCanteensAsync() => await _context.Canteens.ToListAsync();
    }
}
