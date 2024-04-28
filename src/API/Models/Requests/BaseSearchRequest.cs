namespace SampleApp.API.Models.Requests;

public class BaseSearchRequest
{
    public int PageSize { get; set; } = 30;
    public int Page { get; set; } = 1;
}
