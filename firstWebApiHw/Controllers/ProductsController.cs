using AutoMapper;
using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;



namespace webApiShopSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductService _productService;
        IMapper _mapper;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService,IMapper mapper,ILogger<ProductsController> logger)
        {
            _logger = logger;
            _productService = productService;
            _mapper = mapper;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> Get(string? desc, int? minPrice, int? maxPrice, [FromQuery] int?[] categoryIds)
        {
            _logger.LogInformation("The application  go up");
            List<Product> products = await _productService.getAllProductsAsync(desc,  minPrice,maxPrice,  categoryIds);
            List<ProductDto> productsDtos = _mapper.Map<List<Product>, List<ProductDto>>(products);
            return products != null ? Ok(productsDtos) : BadRequest();
        }

    }
}
