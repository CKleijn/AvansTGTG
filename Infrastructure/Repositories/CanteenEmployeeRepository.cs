namespace Infrastructure.Repositories
{
    public class CanteenEmployeeRepository : ICanteenEmployeeRepository
    {
        private readonly AvansDbContext _context;
        public CanteenEmployeeRepository(AvansDbContext context)
        {
            _context = context;
        }

        public async Task<CanteenEmployee> GetCanteenEmployeeByIdAsync(int canteenEmployeeId)
        {
            var canteenEmployee = await _context.CanteenEmployees.FirstOrDefaultAsync(c => c.CanteenEmployeeId == canteenEmployeeId);

            if (canteenEmployee != null)
            {
                return canteenEmployee;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task CreateCanteenEmployeeAsync(CanteenEmployee CanteenEmployee)
        {
            var newCanteenEmployee = await _context.CanteenEmployees.AddAsync(CanteenEmployee);

            if (newCanteenEmployee != null)
            {
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
