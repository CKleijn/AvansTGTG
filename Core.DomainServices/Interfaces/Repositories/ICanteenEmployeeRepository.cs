namespace Core.DomainServices.Interfaces.Repositories
{
    public interface ICanteenEmployeeRepository
    {
        Task<CanteenEmployee> GetCanteenEmployeeByEmployeeNumberAsync(string employeeNumber);
        Task CreateCanteenEmployeeAsync(CanteenEmployee CanteenEmployee);
    }
}
