namespace Domain.Results;

public class Result
{
    public Result(ResultStatus status = ResultStatus.Ok, string? errorMessage = null)
    {
        Status = status;
        ErrorMessage = errorMessage;
    }

    public Result(Result result)
        : this(result.Status, result.ErrorMessage) { }

    public string? ErrorMessage { get; set; }

    public ResultStatus Status { get; set; }

    public bool IsSuccess => (int)Status >= 200 && (int)Status <= 299;
}
