namespace Core.DomainServices.Interfaces.Repositories
{
    public interface ICanteenEmployeeRepository
    {
        Task<CanteenEmployee?> GetCanteenEmployeeByEmployeeNumberAsync(string employeeNumber);
        Task<bool> CreateCanteenEmployeeAsync(CanteenEmployee canteenEmployee);
    }
}
