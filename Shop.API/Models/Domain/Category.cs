using System.ComponentModel.DataAnnotations;

namespace Shop.API.Models.Domain;

public class Category
{
    [Key]
    public Guid Id { get; set; }
    public required string CategoryName { get; set; }
    public required string Slug { get; set; }

    public ICollection<Product>? Products { get; set; }
}