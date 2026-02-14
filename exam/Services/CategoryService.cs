using System.Net;
using Microsoft.Extensions.Logging;
using Dapper;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

public class CategoryService(ApplicationDbContext applicationDBContext,ILogger<CategoryService> logger) : ICategoryService
{
    private readonly ApplicationDbContext _context = applicationDBContext;
    private readonly ILogger<CategoryService> _logger = logger;

    public async Task<Response<string>> AddCategoryAsync(Category Category)
    {
        try
        {
            if(Category == null)
            {
                return new Response<string>(HttpStatusCode.InternalServerError,"You must to type in something that you want to add");
            }
            else
            {
                _context.Categories.Add(Category);
                await _context.SaveChangesAsync();
                return new Response<string>(HttpStatusCode.OK, "Category was added successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> DeleteAsync(int CategoryId)
    {
        try
        {
            var delete = await _context.Categories.FindAsync(CategoryId);
            if(delete == null)
            {
                return new Response<string>(HttpStatusCode.NotFound,$"There is no such Category with this {CategoryId} id");
            }
            else
            { 
                _context.RemoveRange(delete);
                await _context.SaveChangesAsync();
                return new Response<string>(HttpStatusCode.OK, "Category was deleted successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<Category>> GetCategoryByIdAsync(int id)
    {
        try
        {
            var res = await _context.Categories.FindAsync(id);
            if(res == null)
            {
                return new Response<Category>(HttpStatusCode.NotFound,$"There is no such Category with this id {id}");
            }
            else
            {
                return new Response<Category>(HttpStatusCode.OK,"The data that you were searching for:",res);
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<Category>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> UpdateAsync(int CategoryId,Category Category)
    {
        try
        {
            var res = _context.Categories.FindAsync(CategoryId);
            if (res == null)
            {   
                return new Response<string>(HttpStatusCode.NotFound,"Category not found");
            }
            else
            {
                _context.Categories.Update(Category);
                await _context.SaveChangesAsync();
                return new Response<string>(HttpStatusCode.OK,"Category updated successfully");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError,"Internal Server Error");
        }
    }

    public async Task<Response<List<Category>>> GetAllCategoriesAsync()
    {
        var result = await _context.Categories.ToListAsync();
        return result == null
        ? new Response<List<Category>>(HttpStatusCode.NotFound,"Didn't found anything")
        : new Response<List<Category>>(HttpStatusCode.OK,"ok",result);
    }
}
