using Microsoft.EntityFrameworkCore;
using Shop.API.Data;
using Shop.API.Models.Domain;
using Shop.API.Repositories.Interface;

namespace Shop.API.Repositories.Implementation;

public class ImageRepository(
    IWebHostEnvironment webHostEnvironment,
    IHttpContextAccessor httpContextAccessor,
    ApplicationDbContext dbContext) : IImageRepository
{

    public async Task<IEnumerable<ProductImage>> GetAllAsync()
    {
        return await dbContext.ProductImages.ToListAsync();
    }

    public async Task<ProductImage> UploadAsync(IFormFile file, ProductImage productImage)
    {
        var localPath = Path.Combine(webHostEnvironment.ContentRootPath, "Images/Products/Guitars", $"{productImage.FileName}{productImage.FileExtension}");

        await using var stream = new FileStream(localPath, FileMode.Create);
        await file.CopyToAsync(stream);

        var httpRequest = httpContextAccessor.HttpContext?.Request;

        if (httpRequest != null)
        {
            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/images/products/guitars/{productImage.FileName}{productImage.FileExtension}";

            productImage.Url = urlPath;
        }

        await dbContext.ProductImages.AddAsync(productImage);
        await dbContext.SaveChangesAsync();

        return productImage;
    }
}