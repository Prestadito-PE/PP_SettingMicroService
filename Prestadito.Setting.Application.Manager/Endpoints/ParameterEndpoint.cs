using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Prestadito.Setting.Application.Dto.Parameter;
using Prestadito.Setting.Application.Manager.Interfaces;

namespace Prestadito.Setting.Application.Manager.Endpoints
{
    public static class ParameterEndpoint
    {
        readonly static string collection = "parameters";
        public static WebApplication UseParameterEndpoint(this WebApplication app, string basePath)
        {
            string path = $"{basePath}/{collection}";

            app.MapPost(path,
                async (IValidator<CreateParameterDTO> validator, CreateParameterDTO dto, IParametersController controller) =>
                {
                    var validationResult = await validator.ValidateAsync(dto);
                    if (!validationResult.IsValid)
                    {
                        return Results.ValidationProblem(validationResult.ToDictionary());
                    }
                    return await controller.CreateParameter(dto, $"~{path}");
                });

            app.MapGet(path + "/all",
                async (IParametersController controller) =>
                {
                    return await controller.GetAllParameters();
                });

            app.MapGet(path,
                async (IParametersController controller) =>
                {
                    return await controller.GetActiveParameters();
                });

            app.MapGet(path + "/{id}",
                async (string id, IParametersController controller) =>
                {
                    return await controller.GetParameterById(id);
                });

            app.MapPut(path,
                async (IValidator<UpdateParameterDTO> validator, UpdateParameterDTO dto, IParametersController controller) =>
                {
                    var validationResult = await validator.ValidateAsync(dto);
                    if (!validationResult.IsValid)
                    {
                        return Results.ValidationProblem(validationResult.ToDictionary());
                    }
                    return await controller.UpdateParameter(dto);
                });

            app.MapPut(path + "/disable/{id}",
                async (string id, IParametersController controller) =>
                {
                    return await controller.DisableParameter(id);
                });

            app.MapDelete(path + "/delete/{id}",
                async (string id, IParametersController controller) =>
                {
                    return await controller.DeleteParameter(id);
                });

            return app;
        }

        public static WebApplication UseParameterEndpointInterservices(this WebApplication app, string basePath)
        {
            string path = $"{basePath}/interservices/{collection}";

            app.MapGet($"{path}/by-code/{{code}}",
                async (string code, IParametersController controller) =>
                {
                    var response = await controller.GetParameterByCode(code);
                    return response;
                });

            return app;
        }
    }
}