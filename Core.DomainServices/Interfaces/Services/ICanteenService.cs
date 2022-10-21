namespace Core.DomainServices.Interfaces.Services
{
    public interface ICanteenService
    {
        Task<Canteen> GetCanteenByLocationAsync(Location canteenLocation);
        Task<IEnumerable<Canteen>> GetCanteensAsync();
    }
}
