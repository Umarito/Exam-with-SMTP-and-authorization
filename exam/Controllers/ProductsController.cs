using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService ProductService) : ControllerBase
{
    [Authorize(Roles = "Admin,Manager")]
    [HttpPost]
    public async Task<Response<string>> AddProductAsync(Product Product)
    {
        return await ProductService.AddProductAsync(Product);
    }
    [Authorize(Roles = "Admin")]
    [HttpPut("{ProductId}")]
    public async Task<Response<string>> UpdateAsync(int ProductId,Product Product)
    {
        return await ProductService.UpdateAsync(ProductId,Product);
    }
    [Authorize(Roles = "Admin")]
    [HttpDelete("{ProductId}")]
    public async Task<Response<string>> DeleteAsync(int ProductId)
    {
        return await ProductService.DeleteAsync(ProductId);
    }
    [HttpGet]
    public async Task<Response<List<Product>>> GetAllProducts()
    {
        return await ProductService.GetAllProductsAsync();   
    }
    [HttpGet("{ProductId}")]
    public async Task<Response<Product>> GetProductByIdAsync(int ProductId)
    {
        return await ProductService.GetProductByIdAsync(ProductId);
    }
}