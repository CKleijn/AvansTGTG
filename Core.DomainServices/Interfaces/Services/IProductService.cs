namespace Core.DomainServices.Interfaces.Services
{
    public interface IProductService
    {
        Task<Product> GetProductByNameAsync(string name);
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<List<SelectListItem>> GetAllProductsInSelectListAsync();
        List<string> GetProductsNamesFromPacketInList(Packet packet);
        Task<List<Product>> GetProductsFromStringProductsAsync(IList<string> products);
        bool CheckAlcoholReturnBoolean(List<Product> products);
    }
}
