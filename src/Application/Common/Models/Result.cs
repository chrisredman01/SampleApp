using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SampleApp.Application.Common.Models;

public class Result
{
    public bool IsSuccessful { get; }
    public bool IsFailure => !IsSuccessful;
    public Error Error { get; }

    public Result(bool successful, Error error)
    {
        if (successful && error != Error.None || !successful && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }
        IsSuccessful = successful;
        Error = error;
    }

    public static Result Success() => new(true, Error.None);
    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);

    public static Result Failure(Error error) => new(false, error);
    public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    protected internal Result(TValue? value, bool isSuccessful, Error error) : base(isSuccessful, error) =>
        _value = value;

    public TValue Value => IsSuccessful
        ? _value!
        : throw new InvalidOperationException("A failure has no value");
}