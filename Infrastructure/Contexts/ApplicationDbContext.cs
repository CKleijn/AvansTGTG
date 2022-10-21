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
                    Location = Core.Domain.Enums.Location.LA,
                    OfferingHotMeals = false
                },
                new Canteen
                {
                    CanteenId = 2,
                    City = Core.Domain.Enums.Cities.Breda,
                    Location = Core.Domain.Enums.Location.LD,
                    OfferingHotMeals = true
                },
                new Canteen
                {
                    CanteenId = 3,
                    City = Core.Domain.Enums.Cities.Tilburg,
                    Location = Core.Domain.Enums.Location.PA,
                    OfferingHotMeals = true
                },
                new Canteen
                {
                    CanteenId = 4,
                    City = Core.Domain.Enums.Cities.Tilburg,
                    Location = Core.Domain.Enums.Location.PC,
                    OfferingHotMeals = false
                },
                new Canteen
                {
                    CanteenId = 5,
                    City = Core.Domain.Enums.Cities.DenBosch,
                    Location = Core.Domain.Enums.Location.OA,
                    OfferingHotMeals = true
                },
                new Canteen
                {
                    CanteenId = 6,
                    City = Core.Domain.Enums.Cities.DenBosch,
                    Location = Core.Domain.Enums.Location.OB,
                    OfferingHotMeals = false
                },
                new Canteen
                {
                    CanteenId = 7,
                    City = Core.Domain.Enums.Cities.Breda,
                    Location = Core.Domain.Enums.Location.HA,
                    OfferingHotMeals = true
                },
                new Canteen
                {
                    CanteenId = 8,
                    City = Core.Domain.Enums.Cities.Breda,
                    Location = Core.Domain.Enums.Location.HD,
                    OfferingHotMeals = false
                }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    Name = "Glaasje melk",
                    IsAlcoholic = false,
                    Picture = webClient.DownloadData("https://img.freepik.com/free-vector/sticker-glass-orange-juice-white-background_1308-62908.jpg")
                },
                new Product
                {
                    ProductId = 2,
                    Name = "Heineken biertje",
                    IsAlcoholic = true,
                    Picture = webClient.DownloadData("https://cdn.dribbble.com/users/1574371/screenshots/4513279/media/b4efc27d1e4fcf0312956628bfe6c0d5.png")
                },
                new Product
                {
                    ProductId = 3,
                    Name = "Rode wijn",
                    IsAlcoholic = true,
                    Picture = webClient.DownloadData("https://img.freepik.com/free-vector/red-wine-glasses-isolated_1308-119314.jpg")
                },
                new Product
                {
                    ProductId = 4,
                    Name = "Toast",
                    IsAlcoholic = false,
                    Picture = webClient.DownloadData("https://img.freepik.com/free-vector/toasted-bread-slice-cartoon-sticker_1308-62854.jpg")
                },
                new Product
                {
                    ProductId = 5,
                    Name = "Appel",
                    IsAlcoholic = false,
                    Picture = webClient.DownloadData("https://img.freepik.com/free-vector/sticker-design-with-apple-isolated_1308-66383.jpg")
                },
                new Product
                {
                    ProductId = 6,
                    Name = "Banaan",
                    IsAlcoholic = false,
                    Picture = webClient.DownloadData("https://img.freepik.com/free-vector/sticker-design-with-banana-isolated_1308-77292.jpg")
                },
                new Product
                {
                    ProductId = 7,
                    Name = "Zakje chips",
                    IsAlcoholic = false,
                    Picture = webClient.DownloadData("https://img.freepik.com/premium-vector/pakket-en-bord-chips-in-trendy-cartoon-stijl-stapel-chips-in-een-kom-verpakkingsmalplaatje-op-witte-achtergrond-wordt-geisoleerd-die_168129-955.jpg")
                },
                new Product
                {
                    ProductId = 8,
                    Name = "Tomatensoep",
                    IsAlcoholic = false,
                    Picture = webClient.DownloadData("https://justtheforkingrecipe.com/wp-content/uploads/2020/06/soup2.png")
                },
                new Product
                {
                    ProductId = 9,
                    Name = "Spaghetti",
                    IsAlcoholic = false,
                    Picture = webClient.DownloadData("https://img.freepik.com/free-vector/top-view-paghetti-carbonara-dish-sticker-white_1308-60923.jpg")
                },
                new Product
                {
                    ProductId = 10,
                    Name = "Bakje kippenvleugels",
                    IsAlcoholic = false,
                    Picture = webClient.DownloadData("https://img.freepik.com/free-vector/flying-fried-chicken-with-bucket-cartoon_138676-2081.jpg")
                },
                new Product
                {
                    ProductId = 11,
                    Name = "Hotdog",
                    IsAlcoholic = false,
                    Picture = webClient.DownloadData("https://img.freepik.com/free-vector/fast-food-sticker-design-with-hot-dog-isolated_1308-67129.jpg")
                },
                new Product
                {
                    ProductId = 12,
                    Name = "Broodje hamburger",
                    IsAlcoholic = false,
                    Picture = webClient.DownloadData("https://img.freepik.com/free-vector/sticker-design-with-hamburger-isolated_1308-62485.jpg")
                },
                new Product
                {
                    ProductId = 13,
                    Name = "Kersenijsje",
                    IsAlcoholic = false,
                    Picture = webClient.DownloadData("https://img.freepik.com/free-vector/ice-cream-cone-cartoon-icon-illustration-sweet-food-icon-concept-isolated-flat-cartoon-style_138676-2924.jpg")
                },
                new Product
                {
                    ProductId = 14,
                    Name = "Chocoladeijsje",
                    IsAlcoholic = false,
                    Picture = webClient.DownloadData("https://img.freepik.com/free-vector/vector-illustration-ice-cream-colorful-style_341269-946.jpg")
                },
                new Product
                {
                    ProductId = 15,
                    Name = "Appeltaart",
                    IsAlcoholic = false,
                    Picture = webClient.DownloadData("https://img.freepik.com/free-vector/homemade-apple-pie-white-background_1308-65205.jpg")
                }
            );
        }
    }
}
