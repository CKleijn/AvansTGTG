namespace Core.DomainServices.Interfaces.Repositories
{
    public interface ICanteenRepository
    {
        Task<Canteen> GetCanteenByLocationAsync(Location canteenLocation);
        Task<IEnumerable<Canteen>> GetCanteensAsync();
    }
}
