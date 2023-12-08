using System.ComponentModel.DataAnnotations;

namespace Shop.API.Models.Domain;

public class Country
{
    [Key]
    public Guid Id { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }

    public required ICollection<State> States { get; set; }
}