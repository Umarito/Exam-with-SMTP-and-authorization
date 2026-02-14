public interface IProductService
{
    Task<Response<string>> AddProductAsync(Product Product);
    Task<Response<List<Product>>> GetAllProductsAsync();
    Task<Response<Product>> GetProductByIdAsync(int ProductId);
    Task<Response<string>> DeleteAsync(int ProductId);
    Task<Response<string>> UpdateAsync(int ProductId,Product Product);
}