using Pri.WebApi.Food.Api.Dtos.Request;

namespace Pri.WebApi.Api.Dtos.Request
{
    public class ProductUpdateDto : ProductCreateDto
    {
        public int Id { get; set; }
    }
}
