namespace Infrastructure.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Canteen> Canteens { get; set; } = null!;
        public DbSet<CanteenEmployee> CanteenEmployees { get; set; } = null!;
        public DbSet<Packet> Packets { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed here
        }
    }
}
