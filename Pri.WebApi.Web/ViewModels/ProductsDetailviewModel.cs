namespace Pri.Cleanarchitecture.Web.ViewModels
{
    public class ProductsDetailviewModel : BaseViewModel
    {
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> Properties { get; set; }
    }
}
