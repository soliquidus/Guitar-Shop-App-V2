using System.ComponentModel.DataAnnotations;
using Shop.API.Models.Domain;

namespace Shop.API.Models.DTO;

public class ProductDto
{
    [Key]
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string ShortDescription { get; set; }
    public required string FullDescription { get; set; }
    public required string FeaturedImageUrl { get; set; }
    public required string Slug { get; set; }
    public DateTime EntryDate { get; set; }
    public required decimal Price { get; set; }
    public required int StockQuantity { get; set; }
    public required bool IsInStock { get; set; }

    public required string BrandName{ get; set; }
    public required string CategoryName{ get; set; }
}