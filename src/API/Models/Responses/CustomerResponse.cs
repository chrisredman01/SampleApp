namespace SampleApp.API.Models.Responses;

public class CustomerResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Telephone { get; set; }
    public string? Email { get; set; }
    public DateTime CreatedUtc { get; set; } 
    public DateTime ModifiedUtc { get; set; }
}
