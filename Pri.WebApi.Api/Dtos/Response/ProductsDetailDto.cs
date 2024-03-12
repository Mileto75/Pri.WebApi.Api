namespace Pri.WebApi.Food.Api.Dtos.Response
{
    public class ProductsDetailDto : BaseDto
    {
        public BaseDto Category { get; set; }
        public IEnumerable<BaseDto> Properties { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
