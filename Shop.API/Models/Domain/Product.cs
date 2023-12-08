using System.ComponentModel.DataAnnotations;

namespace Shop.API.Models.Domain;

public class Product
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

    public required Brand Brand{ get; set; }
    public required Category Category{ get; set; }
}