public interface ICategoryService
{
    Task<Response<string>> AddCategoryAsync(Category Category);
    Task<Response<List<Category>>> GetAllCategoriesAsync();
    Task<Response<Category>> GetCategoryByIdAsync(int CategoryId);
    Task<Response<string>> DeleteAsync(int CategoryId);
    Task<Response<string>> UpdateAsync(int CategoryId,Category Category);
}