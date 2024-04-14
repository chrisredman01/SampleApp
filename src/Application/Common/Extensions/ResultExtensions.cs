using SampleApp.Application.Common.Models;

namespace SampleApp.Application.Common.Extensions;

public static class ResultExtensions
{
    public static T Match<T>(this Result result, Func<T> onSuccess, Func<Error, T> onFailure) =>
        result.IsSuccessful ? onSuccess() : onFailure(result.Error);
}
