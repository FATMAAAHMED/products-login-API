using Microsoft.AspNetCore.Mvc;
using ProdductApplication;

namespace productsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProuctController:ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProuctController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productRepository.GetAllProducts();


            return Ok(products);
        }

    }
}
