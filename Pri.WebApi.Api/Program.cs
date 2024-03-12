using Microsoft.EntityFrameworkCore;
using Pri.CleanArchitecture.Core.Interfaces.Repositories;
using Pri.CleanArchitecture.Core.Interfaces.Services;
using Pri.CleanArchitecture.Core.Services;
using Pri.CleanArchitecture.Infrastructure.Data;
using Pri.CleanArchitecture.Infrastructure.Repositories;

namespace Pri.WebApi.Food.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Add services to the container.
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            //Register database service
            builder.Services.AddDbContext<ApplicationDbContext>(options
                => options
                .UseSqlServer(builder.Configuration.GetConnectionString("DefaultDatabase")).EnableSensitiveDataLogging());

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
