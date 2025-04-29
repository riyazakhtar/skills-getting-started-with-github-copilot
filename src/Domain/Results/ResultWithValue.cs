namespace Domain.Results;

public class Result<T> : Result
{
    public Result(
        T? value = default,
        ResultStatus status = ResultStatus.Ok,
        string? errorMessage = null
    )
        : base(status, errorMessage)
    {
        Value = value;
    }

    public Result(Result result)
        : this(default, result.Status, result.ErrorMessage) { }

    public Result(Result<T> result)
        : this(result.Value, result.Status, result.ErrorMessage) { }

    public T? Value { get; set; }

    public static implicit operator Result<T>(T value) => new(value);
}
