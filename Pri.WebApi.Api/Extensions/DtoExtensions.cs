using Pri.CleanArchitecture.Core.Entities;
using Pri.WebApi.Food.Api.Dtos;
using Pri.WebApi.Food.Api.Dtos.Response;
using System.Runtime.CompilerServices;

namespace Pri.WebApi.Food.Api.Extensions
{
    public static class DtoExtensions
    {
        public static ProductsDto MapToDto(this IEnumerable<Product> products)
        {
            return new ProductsDto
            {
                Products = products.Select(pr => new BaseDto
                {
                    Id = pr.Id,
                    Name = pr.Name,
                })
            };
        }
        public static ProductsDetailDto MapToDto(this Product product)
        {
            return new ProductsDetailDto 
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Category = new BaseDto 
                {
                    Id = product.Category.Id,
                    Name = product.Category.Name,
                },
                Description = product.Description,
                Properties = product.Properties.Select(p =>
                new BaseDto 
                {
                    Id = p.Id,
                    Name = p.Name,
                })
            };

        }
    }
}
