using System.Net;
using Microsoft.Extensions.Logging;
using Dapper;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

public class ProductService(ApplicationDbContext applicationDBContext,ILogger<ProductService> logger) : IProductService
{
    private readonly ApplicationDbContext _context = applicationDBContext;
    private readonly ILogger<ProductService> _logger = logger;

    public async Task<Response<string>> AddProductAsync(Product Product)
    {
        try
        {
            if(Product == null)
            {
                return new Response<string>(HttpStatusCode.InternalServerError,"You must to type in something that you want to add");
            }
            else
            {
                _context.Products.Add(Product);
                await _context.SaveChangesAsync();
                return new Response<string>(HttpStatusCode.OK, "Product was added successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> DeleteAsync(int ProductId)
    {
        try
        {
            var delete = await _context.Products.FindAsync(ProductId);
            if(delete == null)
            {
                return new Response<string>(HttpStatusCode.NotFound,$"There is no such product with this {ProductId} id");
            }
            else
            { 
                _context.RemoveRange(delete);
                await _context.SaveChangesAsync();
                return new Response<string>(HttpStatusCode.OK, "Product was deleted successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<Product>> GetProductByIdAsync(int id)
    {
        try
        {
            var res = await _context.Products.FindAsync(id);
            if(res == null)
            {
                return new Response<Product>(HttpStatusCode.NotFound,$"There is no such product with this id {id}");
            }
            else
            {
                return new Response<Product>(HttpStatusCode.OK,"The data that you were searching for:",res);
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<Product>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> UpdateAsync(int ProductId,Product Product)
    {
        try
        {
            var res = _context.Products.FindAsync(ProductId);

            if (res == null)
            {   
                return new Response<string>(HttpStatusCode.NotFound,"Product not found");
            }
            else
            {
                _context.Products.Update(Product);
                await _context.SaveChangesAsync();
                return new Response<string>(HttpStatusCode.OK,"Product updated successfully");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError,"Internal Server Error");
        }
    }

    public async Task<Response<List<Product>>> GetAllProductsAsync()
    {
        var result = await _context.Products.ToListAsync();
        return result == null
        ? new Response<List<Product>>(HttpStatusCode.NotFound,"Didn't found anything")
        : new Response<List<Product>>(HttpStatusCode.OK,"ok",result);
    }
}
