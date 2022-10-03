namespace Core.DomainServices.Interfaces.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync();
    }
}
