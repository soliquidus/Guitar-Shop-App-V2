using System.ComponentModel.DataAnnotations;

namespace Shop.API.Models.Domain;

public class Address
{
    [Key]
    public Guid Id { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
    public required string State { get; set; }
    public required string Street { get; set; }
    public required string ZipCode { get; set; }

    public required Order Order { get; set; }
}