using System.ComponentModel.DataAnnotations;

namespace Shop.API.Models.Domain;

public class Order
{
    [Key]
    public Guid Id { get; set; }
    public required string TrackingNumber { get; set; }
    public required Decimal TotalPrice { get; set; }
    public required int TotalQuantity { get; set; }
}