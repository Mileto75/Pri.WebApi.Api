using Pri.WebApi.Food.Api.Dtos.Request;
using System.ComponentModel.DataAnnotations;

namespace Pri.WebApi.Food.Api.Dtos.Request
{
    public class ProductCreateWithImageDto : ProductCreateDto
    {
        //image file
        public IFormFile Image { get; set; }
    }
}
