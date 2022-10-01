namespace Core.DomainServices.Interfaces
{
    public interface ICanteenEmployeeRepository
    {
        Task<CanteenEmployee> GetCanteenEmployeeByIdAsync(int CanteenEmployeeId);
        Task CreateCanteenEmployeeAsync(CanteenEmployee CanteenEmployee);
    }
}
