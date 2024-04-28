namespace SampleApp.API.Models.Requests;

public class CreateCustomerRequest
{
    public string Name { get; set; } = string.Empty; 
    public string? Telephone { get; set; } 
    public string? Email { get; set; }
}


