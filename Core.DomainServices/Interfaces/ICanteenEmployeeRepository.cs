namespace Core.DomainServices.Interfaces
{
    public interface ICanteenEmployeeRepository
    {
        Task<CanteenEmployee> GetCanteenEmployeeByIdAsync(int canteenEmployeeId);
        Task CreateCanteenEmployeeAsync(CanteenEmployee CanteenEmployee);
    }
}
