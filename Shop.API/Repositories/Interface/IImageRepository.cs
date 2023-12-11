using Shop.API.Models.Domain;

namespace Shop.API.Repositories.Interface;

public interface IImageRepository
{
    Task<IEnumerable<ProductImage>> GetAllAsync();
    Task<ProductImage> UploadAsync(IFormFile file, ProductImage productImage);
}