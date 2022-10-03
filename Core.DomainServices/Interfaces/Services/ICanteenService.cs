namespace Core.DomainServices.Interfaces.Services
{
    public interface ICanteenService
    {
        Task<IEnumerable<Canteen>> GetCanteensAsync();
    }
}
