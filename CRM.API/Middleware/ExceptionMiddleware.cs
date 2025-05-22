using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CRM.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException validationException)
            {
                await HandleValidationExceptionAsync(context, validationException);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment()
                    ? new ProblemDetails
                    {
                        Status = context.Response.StatusCode,
                        Detail = ex.Message,
                        Title = ex.Message
                    }
                    : new ProblemDetails
                    {
                        Status = context.Response.StatusCode,
                        Detail = "Internal Server Error",
                        Title = "Internal Server Error"
                    };

                var options = new JsonSerializerSettings
                    { ContractResolver = new CamelCasePropertyNamesContractResolver() };
                var json = JsonConvert.SerializeObject(response, options);

                await context.Response.WriteAsync(json);
            }
        }


        private static Task HandleValidationExceptionAsync(HttpContext context,
            ValidationException validationException)
        {
            var errors = validationException.Errors.Select(err => new
            {
                PropertyName = err.PropertyName,
                ErrorMessage = err.ErrorMessage
            });

            var detail = new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "errors", errors.Select(e => $"Property: {e.PropertyName}, Error: {e.ErrorMessage}").ToArray() }
            })
            {
                Status = (int)HttpStatusCode.BadRequest,
                Type = "https://httpstatuses.com/400",
                Title = "One or more validation errors occurred."
            };

            var result = JsonConvert.SerializeObject(detail);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return context.Response.WriteAsync(result);
        }
    }
}