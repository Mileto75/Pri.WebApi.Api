using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pri.CleanArchitecture.Core.Interfaces.Services;
using Pri.WebApi.Food.Api.Dtos;
using Pri.WebApi.Food.Api.Dtos.Request;
using Pri.WebApi.Food.Api.Dtos.Response;
using Pri.WebApi.Food.Api.Extensions;
using System.Xml.Linq;

namespace Pri.WebApi.Food.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _productService.GetAllAsync();
            if(result.IsSuccess)
            {
                return Ok(result.Result.MapToDto());
            }
            return BadRequest(result.Errors);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _productService
                .GetByIdAsync(id);
            if(result.IsSuccess)
            {
                return Ok(new ProductsDetailDto
                {
                    Id = result.Result.Id,
                    Name = result.Result.Name,
                    Price = result.Result.Price,
                    Description = result.Result.Description,
                    Category = new BaseDto
                    {
                        Id = result.Result.Category.Id,
                        Name = result.Result.Category.Name,
                    },
                    Properties = result.Result.Properties
                    .Select(pr => new BaseDto
                    {
                        Id = pr.Id,
                        Name = pr.Name,
                    })
                });
            }
            return NotFound(result.Errors);
        }
        [HttpGet("Search/ByName/{name}")]
        public async Task<IActionResult> Search(string name)
        {
            var result = await _productService.SearchByNameAsync(name);
            if (result.IsSuccess)
            {
                return Ok(new ProductsDto
                {
                    Products = result.Result.Select(pr => new BaseDto
                    {
                        Id = pr.Id,
                        Name = pr.Name,
                    })
                });
            }
            return Ok(result.Errors);
        }
        [HttpGet("Search/ByCategory/{id}")]
        public async Task<IActionResult> ByCategoryId(int id)
        {
            var result = await _productService.SearchByCategoryIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(new ProductsDto
                {
                    Products = result.Result.Select(pr => new BaseDto
                    {
                        Id = pr.Id,
                        Name = pr.Name,
                    })
                });
            }
            return Ok(result.Errors);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDto productCreateDto)
        {
            var result = await _productService.CreateAsync(
                productCreateDto.Name,productCreateDto.CategoryId,
                productCreateDto.Description,
                productCreateDto.Price,
                productCreateDto.PropertyIds
                );
            if(!result.IsSuccess)
            {
                foreach(var error in  result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
                return BadRequest(ModelState.Values);
            }
            return CreatedAtAction(nameof(Get),new { Id = result.Result.Id },new ProductsDetailDto 
            {
                Id = result.Result.Id,
                Name= result.Result.Name,
                Price = result.Result.Price,
                Description = result.Result.Description,
                Category = new BaseDto 
                {
                    Id = result.Result.Category.Id,
                    Name = result.Result.Category.Name,
                },
                Properties = result.Result.Properties
                    .Select(pr => new BaseDto
                    {
                        Id = pr.Id,
                        Name = pr.Name,
                    })
            });
        }
    }
}

