
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AvansDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AvansAppConnectionString")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
