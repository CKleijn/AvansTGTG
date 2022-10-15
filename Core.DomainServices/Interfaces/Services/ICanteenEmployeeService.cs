namespace Core.DomainServices.Interfaces.Services
{
    public interface ICanteenEmployeeService
    {
        Task<CanteenEmployee> GetCanteenEmployeeByEmployeeNumberAsync(string employeeNumber);
        Task CreateCanteenEmployeeAsync(CanteenEmployee CanteenEmployee);
    }
}
