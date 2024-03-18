using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pri.CleanArchitecture.Core.Interfaces.Services;
using Pri.WebApi.Api.Dtos.Request;
using Pri.WebApi.Food.Api.Dtos;
using Pri.WebApi.Food.Api.Dtos.Request;
using Pri.WebApi.Food.Api.Dtos.Response;
using Pri.WebApi.Food.Api.Extensions;
using System.Net;
using System.Xml.Linq;

namespace Pri.WebApi.Food.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _webHostingEnvironment;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, IWebHostEnvironment webHostingEnvironment, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _webHostingEnvironment = webHostingEnvironment;
            _logger = logger;
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
        //update
        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
        {
            //check if exists
            if(!await _productService.ExistsAsync(productUpdateDto.Id))
            {
                return NotFound("Product not found!");
            }
            //update
            var result = await _productService
                .UpdateAsync(
                productUpdateDto.Id, productUpdateDto.Name,
                productUpdateDto.CategoryId,
                productUpdateDto.Description,
                productUpdateDto.Price,
                productUpdateDto.PropertyIds
                );
            if (!result.IsSuccess)
            {
                foreach(var error in  result.Errors)
                {
                    ModelState.AddModelError("",error);
                }
                return BadRequest(ModelState.Values);
            }
            return Ok(result.Result.MapToDto());
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if(!await _productService.ExistsAsync(id))
            {
                return NotFound("Product does not exist!");
            }
            var result = await _productService.DeleteAsync(id);
            if(result.IsSuccess)
            {
                return Ok("Product Deleted");
            };
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("",error);
            }
            return BadRequest(ModelState.Values);
        }
        [HttpPost("WithImage")]
        public async Task<IActionResult> CreateWithImage([FromForm]ProductCreateWithImageDto productCreateWithImageDto)
        {
            //create a unique filename
            var filename = $"{Guid.NewGuid()}_{productCreateWithImageDto.Image.FileName}";
            //create the path to file/image folder
            var pathToFolder = Path.Combine(_webHostingEnvironment.ContentRootPath,"wwwroot","images");
            //check if directory
            if(!Directory.Exists(pathToFolder))
            {
                try
                {
                    Directory.CreateDirectory(pathToFolder);
                }
                catch(DirectoryNotFoundException directoryNotFoundException)
                {
                    //log the error
                    _logger.LogCritical(directoryNotFoundException.Message);
                    Response.StatusCode = 500;
                    return Content("File error");
                }
            }
            //create the full path
            var fullPath = Path.Combine(pathToFolder,filename);
            //copy to folder
            using(FileStream fileStream = new FileStream(fullPath,FileMode.Create))
            {
                await productCreateWithImageDto.Image.CopyToAsync(fileStream);
            }
            return Ok();
        }
    }
}