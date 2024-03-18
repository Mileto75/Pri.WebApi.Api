using Microsoft.AspNetCore.Mvc;
using Pri.Cleanarchitecture.Web.ViewModels;
using Pri.CleanArchitecture.Core.Interfaces.Services;

namespace Pri.Cleanarchitecture.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _productService.GetAllAsync();
            if(result.IsSuccess)
            {
                ProductsIndexViewModel productsIndexViewModel = new ProductsIndexViewModel();
                productsIndexViewModel.Products
                    = result.Result.Select(p =>
                    new BaseViewModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                    });
                return View(productsIndexViewModel);
            }
            return View("Error",result.Errors);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var result = await _productService.GetByIdAsync(id);
            if(result.IsSuccess)
            {
                ProductsDetailviewModel productsDetailViewModel 
                    = new ProductsDetailviewModel
                    {
                        Id = result.Result.Id,
                        Name = result.Result.Name,
                        Price = result.Result.Price,
                        Properties = result.Result.Properties
                        .Select(p => p.Name),
                        Category = result.Result.Category.Name,
                    };
                return View(productsDetailViewModel);
            }
            Response.StatusCode = 404;
            return View("Error",result.Errors); 
        }
        public async Task<IActionResult> Create()
        {
            var result = await _productService.CreateAsync
                ("Pandoro",1,"very gooy dessert",9.00M,new List<int>() {1,2});
            if(result.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            return View("Error", result.Errors);
        }
    }
}
