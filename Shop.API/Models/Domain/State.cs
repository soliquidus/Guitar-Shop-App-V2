using System.ComponentModel.DataAnnotations;

namespace Shop.API.Models.Domain;

public class State
{
    [Key]
    public Guid Id { get; set; }
    public required string Name { get; set; }

    public required Country Country { get; set; }
}