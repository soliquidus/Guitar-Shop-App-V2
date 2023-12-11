using Microsoft.EntityFrameworkCore;
using Shop.API.Data;
using Shop.API.Models.Domain;
using Shop.API.Repositories.Interface;

namespace Shop.API.Repositories.Implementation;

public class CategoryRepository(ApplicationDbContext dbContext): ICategoryRepository
{
    public async Task<Category?> GetByNameAsync(string name)
    {
        return await dbContext.Categories.FirstOrDefaultAsync(c => c.CategoryName == name);
    }
}