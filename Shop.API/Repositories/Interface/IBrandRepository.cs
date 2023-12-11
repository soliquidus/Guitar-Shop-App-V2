using Shop.API.Models.Domain;

namespace Shop.API.Repositories.Interface;

public interface IBrandRepository
{
    Task<Brand?> GetByNameAsync(string name);
}