namespace SampleApp.Application.Common.Models;

public sealed record Error(ErrorType Type, string? Description)
{
    public static readonly Error None = new(ErrorType.None, null);
};

public enum ErrorType
{
    None,
    BadRequest,
    NotFound,
    ServerError
}