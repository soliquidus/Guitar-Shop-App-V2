using Shop.API.Models.Domain;

namespace Shop.API.Repositories.Interface;

public interface ICategoryRepository
{
    Task<Category?> GetByNameAsync(string name);
}