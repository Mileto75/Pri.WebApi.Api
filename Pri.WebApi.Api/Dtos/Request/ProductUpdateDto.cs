using Pri.WebApi.Food.Api.Dtos.Request;
using System.ComponentModel.DataAnnotations;

namespace Pri.WebApi.Api.Dtos.Request
{
    public class ProductUpdateDto : ProductCreateDto
    {
        [Required]
        public int Id { get; set; }
    }
}
