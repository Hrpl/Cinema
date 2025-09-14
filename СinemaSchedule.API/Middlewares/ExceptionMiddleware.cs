namespace СinemaSchedule.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger<ExceptionMiddleware> logger)
    {
        object? exceptionResponseBody = null;
        Exception? exception = null;
        int? statusCode = null;
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            statusCode = 500;
            exceptionResponseBody = ex.Message;
            exception = ex;
        }

        if (exception is not null)
        {
            context.Response.StatusCode = (int)statusCode!;
            logger.LogError(exception, "Вызвано исключение:");

            await context.Response.WriteAsJsonAsync(exceptionResponseBody!);
        }
    }
}