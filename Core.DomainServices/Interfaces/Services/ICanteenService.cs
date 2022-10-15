namespace Core.DomainServices.Interfaces.Services
{
    public interface ICanteenService
    {
        Task<Canteen> GetCanteenByLocationAsync(string canteenLocation);
        Task<IEnumerable<Canteen>> GetCanteensAsync();
    }
}
