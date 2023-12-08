using System.ComponentModel.DataAnnotations;

namespace Shop.API.Models.Domain;

public class Brand
{
    [Key]
    public Guid Id { get; set; }
    public required string BrandName { get; set; }
    public required string Slug { get; set; }

    public ICollection<Product>? Products { get; set; }
}