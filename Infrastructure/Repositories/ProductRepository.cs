namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AvansDbContext _context;
        public ProductRepository(AvansDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetProductsAsync() => await _context.Products.ToListAsync();
    }
}
