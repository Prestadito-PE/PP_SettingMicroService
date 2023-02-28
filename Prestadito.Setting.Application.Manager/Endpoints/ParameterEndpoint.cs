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
        public static WebApplication UseParameterEndpoint(this WebApplication app, string cors, string basePath)
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
                    return await controller.CreateParameter(dto, $"~/{path}");
                }).RequireCors(cors);

            app.MapGet(path + "/all",
                async (IParametersController controller) =>
                {
                    return await controller.GetAllParameters();
                }).RequireCors(cors);

            app.MapGet(path,
                async (IParametersController controller) =>
                {
                    return await controller.GetActiveParameters();
                }).RequireCors(cors);

            app.MapGet(path + "/{id}",
                async (string id, IParametersController controller) =>
                {
                    return await controller.GetParameterById(id);
                }).RequireCors(cors);

            app.MapPut(path,
                async (IValidator<UpdateParameterDTO> validator, UpdateParameterDTO dto, IParametersController controller) =>
                {
                    var validationResult = await validator.ValidateAsync(dto);
                    if (!validationResult.IsValid)
                    {
                        return Results.ValidationProblem(validationResult.ToDictionary());
                    }
                    return await controller.UpdateParameter(dto);
                }).RequireCors(cors);

            app.MapPut(path + "/disable/{id}",
                async (string id, IParametersController controller) =>
                {
                    var response = await controller.DisableParameter(id);
                    return response != null ? Results.Ok(response) : Results.UnprocessableEntity(response);
                }).RequireCors(cors);

            app.MapDelete(path + "/delete/{id}",
                async (string id, IParametersController controller) =>
                {
                    var response = await controller.DeleteParameter(id);
                    return response != null ? Results.Ok(response) : Results.UnprocessableEntity(response);
                }).RequireCors(cors);

            return app;
        }
    }
}