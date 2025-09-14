namespace СinemaSchedule.Domen.Generic;

public class CustomResult<T>
{
    public bool IsSuccess { get; }
    public T Value { get; }
    public string? Error { get; }

    private CustomResult(T value, bool isSuccess, string? error)
    {
        Value = value;
        IsSuccess = isSuccess;
        Error = error;
    }

    public static CustomResult<T> Success(T value) => new(value, true, null);
    public static CustomResult<T> Failure(string error) => new(default!, false, error);
}