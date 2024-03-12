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
    }
}
