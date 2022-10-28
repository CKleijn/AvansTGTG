namespace Infrastructure.Repositories
{
    public class CanteenEmployeeRepository : ICanteenEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        public CanteenEmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CanteenEmployee?> GetCanteenEmployeeByEmployeeNumberAsync(string employeeNumber) => await _context.CanteenEmployees.FirstOrDefaultAsync(c => c.EmployeeNumber == employeeNumber);

        public async Task<bool> CreateCanteenEmployeeAsync(CanteenEmployee canteenEmployee)
        {
            if (canteenEmployee == null)
                return false;

            await _context.CanteenEmployees.AddAsync(canteenEmployee);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
