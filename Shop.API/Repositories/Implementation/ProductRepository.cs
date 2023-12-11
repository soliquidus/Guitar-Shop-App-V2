using Microsoft.EntityFrameworkCore;
using Shop.API.Data;
using Shop.API.Models.Domain;
using Shop.API.Repositories.Interface;

namespace Shop.API.Repositories.Implementation;

public class ProductRepository(ApplicationDbContext dbContext) : IProductRepository
{
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await dbContext.Products
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await dbContext.Products
            .Include(p => p.Category)
            .Include(p => p.Brand).
            FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Product?> GetBySlugAsync(string slug)
    {
        return await dbContext.Products
            .Include(p => p.Category)
            .Include(p => p.Brand).
            FirstOrDefaultAsync(p => p.Slug == slug);
    }

    public async Task<Product> CreateAsync(Product product)
    {
        await dbContext.Products.AddAsync(product);
        await dbContext.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> UpdateAsync(Product product)
    {
        var existingProduct = await dbContext.Products
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == product.Id);

        if (existingProduct is null) return null;
        
        dbContext.Entry(existingProduct).CurrentValues.SetValues(product);
        
        await dbContext.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> DeleteAsync(Guid id)
    {
        var product = await dbContext.Products
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product is null) return null;

        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync();
        return product;
    }
}