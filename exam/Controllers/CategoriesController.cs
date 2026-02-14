using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CategoriesController(ICategoryService CategoryService) : ControllerBase
{
    [Authorize(Roles = "Admin,Manager")]
    [HttpPost]
    public async Task<Response<string>> AddCategoryAsync(Category Category)
    {
        return await CategoryService.AddCategoryAsync(Category);
    }
    [Authorize(Roles = "Admin")]
    [HttpPut("{CategoryId}")]
    public async Task<Response<string>> UpdateAsync(int CategoryId,Category Category)
    {
        return await CategoryService.UpdateAsync(CategoryId,Category);
    }
    [Authorize(Roles = "Admin")]
    [HttpDelete("{CategoryId}")]
    public async Task<Response<string>> DeleteAsync(int CategoryId)
    {
        return await CategoryService.DeleteAsync(CategoryId);
    }
    [HttpGet]
    public async Task<Response<List<Category>>> GetAllCategories()
    {
        return await CategoryService.GetAllCategoriesAsync();   
    }
    [HttpGet("{CategoryId}")]
    public async Task<Response<Category>> GetCategoryByIdAsync(int CategoryId)
    {
        return await CategoryService.GetCategoryByIdAsync(CategoryId);
    }
}