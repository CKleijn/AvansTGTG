namespace Infrastructure.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Canteen>? Canteens { get; set; }
        public DbSet<CanteenEmployee>? CanteenEmployees { get; set; }
        public DbSet<Packet>? Packets { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Student>? Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed here
        }
    }
}
