using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Prestadito.Setting.Application.Dto.Parameter;
using Prestadito.Setting.Application.Manager.Validators;

namespace Prestadito.Setting.Application.Manager.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateParameterDTO>, CreateParameterValidator>();
            services.AddScoped<IValidator<UpdateParameterDTO>, UpdateParameterValidator>();

            return services;
        }
    }
}
