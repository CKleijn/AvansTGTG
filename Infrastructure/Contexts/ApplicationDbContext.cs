using System.Net;

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

            var webClient = new WebClient();

            modelBuilder.Entity<Canteen>().HasData(
                new Canteen
                {
                    CanteenId = 1,
                    City = Core.Domain.Enums.Cities.Breda,
                    Location = "LA",
                    OfferingHotMeals = true
                },
                new Canteen
                {
                    CanteenId = 2,
                    City = Core.Domain.Enums.Cities.Breda,
                    Location = "LD",
                    OfferingHotMeals = false
                },
                new Canteen
                {
                    CanteenId = 3,
                    City = Core.Domain.Enums.Cities.Tilburg,
                    Location = "PC",
                    OfferingHotMeals = true
                }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    Name = "Glaasje melk",
                    IsAlcoholic = false,
                    Picture = webClient.DownloadData("https://howtodrawforkids.com/wp-content/uploads/2021/12/how-to-draw-cartoon-milk.jpg")
                },
                new Product
                {
                    ProductId = 2,
                    Name = "Tomatensoep",
                    IsAlcoholic = false,
                    Picture = webClient.DownloadData("https://justtheforkingrecipe.com/wp-content/uploads/2020/06/soup2.png")
                },
                new Product
                {
                    ProductId = 3,
                    Name = "Sandwich",
                    IsAlcoholic = false,
                    Picture = webClient.DownloadData("https://www.nicepng.com/png/detail/24-248536_complete-hamlogna-sandwich-cartoon-sandwich-png.png")
                },
                new Product
                {
                    ProductId = 4,
                    Name = "Heineken biertje",
                    IsAlcoholic = true,
                    Picture = webClient.DownloadData("https://cdn.dribbble.com/users/1574371/screenshots/4513279/media/b4efc27d1e4fcf0312956628bfe6c0d5.png")
                },
                new Product
                {
                    ProductId = 5,
                    Name = "Zakje chips",
                    IsAlcoholic = false,
                    Picture = webClient.DownloadData("https://img.freepik.com/premium-vector/pakket-en-bord-chips-in-trendy-cartoon-stijl-stapel-chips-in-een-kom-verpakkingsmalplaatje-op-witte-achtergrond-wordt-geisoleerd-die_168129-955.jpg")
                }
            );
        }
    }
}
