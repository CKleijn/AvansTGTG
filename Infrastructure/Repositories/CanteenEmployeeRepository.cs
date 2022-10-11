namespace Infrastructure.Repositories
{
    public class CanteenEmployeeRepository : ICanteenEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        public CanteenEmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CanteenEmployee> GetCanteenEmployeeByEmployeeNumberAsync(string employeeNumber)
        {
            var canteenEmployee = await _context.CanteenEmployees.FirstOrDefaultAsync(c => c.EmployeeNumber == employeeNumber);

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
