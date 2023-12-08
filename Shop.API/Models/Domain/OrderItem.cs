using System.ComponentModel.DataAnnotations;

namespace Shop.API.Models.Domain;

public class OrderItem
{
    [Key]
    public Guid Id { get; set; }
    public required string FeaturedImageUrl { get; set; }
    public required int Quantity { get; set; }
    public required decimal UnitPrice { get; set; }
    public required Guid ProductId { get; set; }

    public required Order Order { get; set; }
}