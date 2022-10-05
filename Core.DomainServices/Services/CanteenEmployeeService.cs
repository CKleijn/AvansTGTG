namespace Core.DomainServices.Services
{
    public class CanteenEmployeeService : ICanteenEmployeeService
    {
        private readonly ICanteenEmployeeRepository _canteenEmployeeRepository;

        public CanteenEmployeeService(ICanteenEmployeeRepository canteenEmployeeRepository)
        {
            _canteenEmployeeRepository = canteenEmployeeRepository;
        }
        public async Task<CanteenEmployee> GetCanteenEmployeeByIdAsync(int canteenEmployeeId)
        {
            return await _canteenEmployeeRepository.GetCanteenEmployeeByIdAsync(canteenEmployeeId);
        }

        public async Task CreateCanteenEmployeeAsync(CanteenEmployee CanteenEmployee)
        {
            await _canteenEmployeeRepository.CreateCanteenEmployeeAsync(CanteenEmployee);
        }
    }
}
