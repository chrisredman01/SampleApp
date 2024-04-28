namespace SampleApp.API.Models.Responses;

public class SearchCustomerResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Telephone { get; set; }
    public string? Email { get; set; }
}
