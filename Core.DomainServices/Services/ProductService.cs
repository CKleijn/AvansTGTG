namespace Core.DomainServices.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> GetProductByNameAsync(string name)
        {
            var product = await _productRepository.GetProductByNameAsync(name);

            if (product == null)
                throw new Exception("Er bestaat geen product met deze naam!");

            return product;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync() => await _productRepository.GetProductsAsync();

        public async Task<List<SelectListItem>> GetAllProductsInSelectListAsync() => (await GetProductsAsync()).Select(product => new SelectListItem { Text = product.Name, Value = product.Name }).ToList();

        public List<string> GetProductsNamesFromPacketInList(Packet packet) => packet.Products!.Select(product => product.Name!).ToList();

        public async Task<List<Product>> GetProductsFromStringProductsAsync(IList<string> products)
        {
            var productList = new List<Product>();

            foreach (var product in products)
                productList.Add(await GetProductByNameAsync(product));

            return productList;
        }

        public bool CheckAlcoholReturnBoolean(List<Product> products) => products.Any(p => p.IsAlcoholic == true);
    }
}
