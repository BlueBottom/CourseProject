using System.Net;
using System.Text.Json;
using AdvertBoard.Application.AppServices.Exceptions;
using AdvertBoard.Contracts.Common;
using FluentValidation;

namespace AdvertBoard.Hosts.Api.Middlewares;

/// <summary>
/// Промежуточное ПО для обработки ошибок.
/// </summary>
public class ExceptionHandlingMiddleware
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All),
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private const string LogTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode}";
    private readonly RequestDelegate _next;

    /// <summary>
    /// Инициализирует экземпляр <see cref="ExceptionHandlingMiddleware"/>.
    /// </summary>
    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    /// <summary>
    /// Выполняет операцию промежуточного ПО и передаёт управление
    /// </summary>
    public async Task Invoke(
        HttpContext context
        , IHostEnvironment environment
        , IServiceProvider serviceProvider
        , ILogger<ExceptionHandlingMiddleware> logger)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            var statusCode = GetStatusCode(e);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var apiError = CreateApiError(e, context, environment);
            await context.Response.WriteAsync(JsonSerializer.Serialize(apiError, JsonSerializerOptions));
        }
    }

    private object CreateApiError(Exception exception, HttpContext context, IHostEnvironment environment)
    {
        if (!environment.IsDevelopment())
        {
            return new ApiError
            {
                Code = ((int)HttpStatusCode.InternalServerError).ToString(),
                Message = exception.Message,
                Description = exception.StackTrace,
                TraceId = context.TraceIdentifier,
            };
        }

        return exception switch
        {
            HumanReadableException humanReadableException => new HumanReadableError
            {
                Code = context.Response.StatusCode.ToString(),
                HumanReadableErrorMessage = humanReadableException.HumanReadableMessage,
                Message = humanReadableException.Message,
                TraceId = context.TraceIdentifier,
            },
            ForbiddenException forbiddenException => new ApiError()
            {
                Code = context.Response.StatusCode.ToString(),
                Message = forbiddenException.Message,
                TraceId = context.TraceIdentifier
            },
            EntityNotFoundException entityNotFoundException => new ApiError
            {
                Code = context.Response.StatusCode.ToString(),
                Message = entityNotFoundException.Message,
                TraceId = context.TraceIdentifier,
            },
            ValidationException validationException => new ValidationApiError()
            {
                Message = validationException.Message,
                Description = string.Join("\n", validationException.Errors.Select(s => $"{s.PropertyName}: {s.ErrorMessage}")),
                Code = context.Response.StatusCode.ToString(),
                Errors = validationException.Errors
                    .GroupBy(
                        x => x.PropertyName, 
                        x => x.ErrorMessage)
                    .ToDictionary(k => k.Key, v => v.ToArray())
            },
            _ => new ApiError
            {
                Code = ((int)HttpStatusCode.InternalServerError).ToString(),
                Message = "Произошла непредвиденая ошибка.",
                TraceId = context.TraceIdentifier,
            }
        };
    }

    private HttpStatusCode GetStatusCode(Exception exception)
    {
        return exception switch
        {
            EntityNotFoundException => HttpStatusCode.NotFound,
            ForbiddenException => HttpStatusCode.Forbidden,
            ValidationException => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError,
        };
    }
}