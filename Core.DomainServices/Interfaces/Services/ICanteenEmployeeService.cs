namespace Core.DomainServices.Interfaces.Services
{
    public interface ICanteenEmployeeService
    {
        Task<CanteenEmployee> GetCanteenEmployeeByEmployeeNumberAsync(string employeeNumber);
        Task<CanteenEmployee> CreateCanteenEmployeeAsync(CanteenEmployee canteenEmployee);
    }
}
