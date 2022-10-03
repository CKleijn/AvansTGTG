namespace Infrastructure.Repositories
{
    public class CanteenRepository : ICanteenRepository
    {
        private readonly ApplicationDbContext _context;
        public CanteenRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Canteen>> GetCanteensAsync() => await _context.Canteens.ToListAsync();
    }
}
