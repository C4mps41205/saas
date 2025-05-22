using System.Reflection;
using Application.Dto.Request;
using Application.Validators;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace Infra.Ioc.DependencyInjection;

public static class FluentValidatorDependencyInjection
{
    public static void AddFluentValidator(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        services.AddTransient<IValidator<ClientRequest>, ClientRequestValidator>();
        services.AddTransient<IValidator<GetClientByIdRequest>, GetClientByIdRequestValidator>();
        services.AddTransient<IValidator<GetClientRequest>, GetClientRequestValidator>();
    }
}