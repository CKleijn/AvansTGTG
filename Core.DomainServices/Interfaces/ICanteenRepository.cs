namespace Core.DomainServices.Interfaces
{
    public interface ICanteenRepository
    {
        Task<IEnumerable<Canteen>> GetCanteensAsync();
    }
}
