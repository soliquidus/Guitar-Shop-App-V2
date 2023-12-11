using Microsoft.EntityFrameworkCore;
using Shop.API.Data;
using Shop.API.Models.Domain;
using Shop.API.Repositories.Interface;

namespace Shop.API.Repositories.Implementation;

public class BrandRepository(ApplicationDbContext dbContext) : IBrandRepository
{
    public async Task<Brand?> GetByNameAsync(string name)
    {
        return await dbContext.Brands.FirstOrDefaultAsync(b => b.BrandName == name);
    }
}