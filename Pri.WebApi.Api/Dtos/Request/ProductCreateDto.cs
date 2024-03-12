using System.ComponentModel.DataAnnotations;

namespace Pri.WebApi.Food.Api.Dtos.Request
{
    public class ProductCreateDto
    {
        [Required(ErrorMessage = "Name required!")]
        public string Name { get; set; }
        [Range(0,double.MaxValue)]
        public decimal Price { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Category required!")]
        public int CategoryId{ get; set; }
        [Required]
        public IEnumerable<int> PropertyIds { get; set; }
    }
}
