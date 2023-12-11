namespace Shop.API.Models.Domain;

public class ProductImage
{
    public Guid Id { get; set; }
    public required string FileName { get; set; }
    public required string FileExtension { get; set; }
    public string Url { get; set; }
    public DateTime CreationDate { get; set; }
}