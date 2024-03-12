using Microsoft.EntityFrameworkCore;
using Pri.CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.CleanArchitecture.Infrastructure.Data.Seeding
{
    public static class Seeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var categories = new Category[]
            {
                new Category{Id = 1, Name = "Asian" },
                new Category{Id = 2, Name = "Italian" },
                new Category{Id = 3, Name = "Belgian" },
                new Category{Id = 4, Name = "Greek" },
            };
            var properties = new Property[]
            {
                new Property{Id = 1,Name="Spicy" },
                new Property{Id = 2,Name="Sweet" },
                new Property{Id = 3,Name="Hot" },
                new Property{Id = 4,Name="Cold" },
            };
            var products = new Product[]
            {
                new Product{Id = 1,Name="Sushi",CategoryId=1,Price=8.90M,Description="Fresh fish"},
                new Product{Id = 2,Name="Spaghetti aglio e olio",CategoryId=2,Price=8.90M,Description="Garlic and oil"},
                new Product{Id = 3,Name="Frieten met stoverijsaus",CategoryId=3,Price=8.90M,Description="A classic!"},
                new Product{Id = 4,Name="Moussaka",CategoryId=4,Price=8.90M,Description="A classic!"},
            };
            var productsProperties = new[]
            {
                new {ProductsId=1,PropertiesId=1 },
                new {ProductsId=1,PropertiesId=2 },
                new {ProductsId=2,PropertiesId=3 },
                new {ProductsId=2,PropertiesId=4 },
                new {ProductsId=3,PropertiesId=1 },
                new {ProductsId=3,PropertiesId=2 },
                new {ProductsId=4,PropertiesId=3 },
                new {ProductsId=4,PropertiesId=4 },
            };
            modelBuilder.Entity<Category>().HasData(categories);
            modelBuilder.Entity<Product>().HasData(products);
            modelBuilder.Entity<Property>().HasData(properties);
            modelBuilder.Entity($"{nameof(Product)}{nameof(Property)}").HasData(productsProperties);
        }
    }
}
