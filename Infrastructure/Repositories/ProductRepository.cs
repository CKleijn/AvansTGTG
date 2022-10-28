namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetProductByNameAsync(string name) => await _context.Products.FirstOrDefaultAsync(p => p.Name == name);

        public async Task<IEnumerable<Product>> GetProductsAsync() => await _context.Products.ToListAsync();
    }
}
