namespace Core.DomainServices.Interfaces.Repositories
{
    public interface ICanteenRepository
    {
        Task<Canteen> GetCanteenByLocationAsync(string canteenLocation);
        Task<IEnumerable<Canteen>> GetCanteensAsync();
    }
}
