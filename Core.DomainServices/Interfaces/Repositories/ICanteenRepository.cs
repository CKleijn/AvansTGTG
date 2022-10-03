namespace Core.DomainServices.Interfaces.Repositories
{
    public interface ICanteenRepository
    {
        Task<IEnumerable<Canteen>> GetCanteensAsync();
    }
}
