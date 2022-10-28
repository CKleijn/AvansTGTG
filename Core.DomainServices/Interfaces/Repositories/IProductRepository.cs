namespace Core.DomainServices.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> GetProductByNameAsync(string name);
        Task<IEnumerable<Product>> GetProductsAsync();
    }
}
