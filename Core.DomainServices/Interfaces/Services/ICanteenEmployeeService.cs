namespace Core.DomainServices.Interfaces.Services
{
    public interface ICanteenEmployeeService
    {
        Task<CanteenEmployee> GetCanteenEmployeeByIdAsync(int canteenEmployeeId);
        Task CreateCanteenEmployeeAsync(CanteenEmployee CanteenEmployee);
    }
}
