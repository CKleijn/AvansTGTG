namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Product> GetProductByNameAsync(string name)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Name == name);

            if (product != null)
                return product;

            throw new Exception($"Er bestaat geen product met de naam {name}!");
        }
        public async Task<IEnumerable<Product>> GetProductsAsync() => await _context.Products.ToListAsync();
    }
}
