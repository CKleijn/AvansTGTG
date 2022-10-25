using Core.DomainServices.Interfaces.Services;

namespace Core.DomainServices.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> GetProductByNameAsync(string name) => await _productRepository.GetProductByNameAsync(name);

        public async Task<IEnumerable<Product>> GetProductsAsync() => await _productRepository.GetProductsAsync();

        public async Task<List<SelectListItem>> GetAllProductsInSelectListAsync()
        {
            var products = new List<SelectListItem>();

            foreach (var product in await GetProductsAsync())
                products.Add(new SelectListItem { Text = product.Name, Value = product.Name });

            return products;
        }
        public List<string> GetProductsFromPacketInList(Packet packet)
        {
            var products = new List<string>();

            foreach (var product in packet.Products!)
                products.Add(product.Name!);

            return products;
        }

        public async Task<object> CheckAlcoholReturnProductList(IList<string> products)
        {
            var productList = new List<Product>();

            var containsAlchohol = false;

            foreach (var product in products)
            {
                var fullProduct = await GetProductByNameAsync(product);

                if ((bool)fullProduct.IsAlcoholic!)
                    containsAlchohol = true;

                productList.Add(fullProduct);
            }

            return new
            {
                containsAlchohol,
                productList
            };
        }
    }
}
