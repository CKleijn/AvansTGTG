namespace Core.DomainServices.Interfaces.Repositories
{
    public interface ICanteenEmployeeRepository
    {
        Task<CanteenEmployee> GetCanteenEmployeeByIdAsync(int canteenEmployeeId);
        Task CreateCanteenEmployeeAsync(CanteenEmployee CanteenEmployee);
    }
}
