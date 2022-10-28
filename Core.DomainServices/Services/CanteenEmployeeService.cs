namespace Core.DomainServices.Services
{
    public class CanteenEmployeeService : ICanteenEmployeeService
    {
        private readonly ICanteenEmployeeRepository _canteenEmployeeRepository;

        public CanteenEmployeeService(ICanteenEmployeeRepository canteenEmployeeRepository)
        {
            _canteenEmployeeRepository = canteenEmployeeRepository;
        }

        public async Task<CanteenEmployee> GetCanteenEmployeeByEmployeeNumberAsync(string employeeNumber)
        {
            var canteenEmployee = await _canteenEmployeeRepository.GetCanteenEmployeeByEmployeeNumberAsync(employeeNumber);

            if(canteenEmployee == null)
                throw new Exception("Er bestaat geen kantine medewerker met dit personeelsnummer!");

            return canteenEmployee;
        }

        public async Task<CanteenEmployee> CreateCanteenEmployeeAsync(CanteenEmployee canteenEmployee)
        {
            var succeeded = await _canteenEmployeeRepository.CreateCanteenEmployeeAsync(canteenEmployee);

            if (!succeeded)
                throw new Exception("De kantine medewerker is niet aangemaakt!");

            return canteenEmployee;
        }
    }
}
