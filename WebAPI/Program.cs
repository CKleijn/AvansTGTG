using Microsoft.AspNetCore.Identity;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebAPI.GraphQL;
using Infrastructure.Repositories;
using Microsoft.OpenApi.Models;
using Core.DomainServices.Interfaces.Services;
using Core.DomainServices.Services;
using Core.DomainServices.Interfaces.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(v =>
{
    v.SwaggerDoc("v1", new OpenApiInfo { Title = "TGTG-Avans REST API", Version = "v1" });
});

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationConnectionString")));
builder.Services.AddDbContext<SecurityDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SecurityConnectionString")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<SecurityDbContext>().AddDefaultTokenProviders();

builder.Services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters.ValidateAudience = false;
    options.TokenValidationParameters.ValidateIssuer = false;
    options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["BearerTokens:Key"]));
});

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ICanteenEmployeeRepository, CanteenEmployeeRepository>();
builder.Services.AddScoped<IPacketRepository, PacketRepository>();
builder.Services.AddScoped<ICanteenRepository, CanteenRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ICanteenEmployeeService, CanteenEmployeeService>();
builder.Services.AddScoped<IPacketService, PacketService>();
builder.Services.AddScoped<ICanteenService, CanteenService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddGraphQLServer().RegisterService<IPacketService>().AddQueryType<Query>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();

app.UseSwaggerUI(v =>
{
    v.SwaggerEndpoint("/swagger/v1/swagger.json", "TGTG-Avans REST API V1");
});

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.MapGraphQL();

app.Run();
