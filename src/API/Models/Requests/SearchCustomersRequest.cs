namespace SampleApp.API.Models.Requests;

public class SearchCustomersRequest : BaseSearchRequest
{
    public string? SearchTerm { get; set; }
}
