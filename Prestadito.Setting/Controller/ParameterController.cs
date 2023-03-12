using Prestadito.Setting.Application.Dto.Parameter;
using Prestadito.Setting.Application.Manager.Interfaces;
using Prestadito.Setting.Application.Manager.Models;
using Prestadito.Setting.Application.Manager.Utilities;
using Prestadito.Setting.Application.Services.Interfaces;
using Prestadito.Setting.Domain.MainModule.Entities;
using System.Linq.Expressions;

namespace Prestadito.Setting.API.Controller
{
    public class ParameterController: IParametersController
    {
        private readonly IParameterRepository ParameterRepository;
        public ParameterController(IDataService dataService)
        {
            ParameterRepository = dataService.Parameters;
        }

        public async ValueTask<IResult> CreateParameter(CreateParameterDTO dto, string path)
        {
            ResponseModel<ParameterModel> responseModel;

            Expression<Func<ParameterEntity, bool>> filter = f => f.StrCode == dto.StrCode;
            var ParameterExist = await ParameterRepository.GetAllAsync(filter);
            if (ParameterExist is not null && ParameterExist.Count > 0)
            {
                responseModel = ResponseModel<ParameterModel>.GetResponse($"Code is already register");
                return Results.NotFound(responseModel);
            }

            var entity = new ParameterEntity
            {
                StrCode = dto.StrCode,
                StrDescription = dto.StrDescription,
                StrName = dto.StrName,
                StrValue = dto.StrValue,
                StrType = dto.StrType,
                StrParentCode = dto.StrParentCode,
                StrCreateUser = dto.strCreateUser,
                DteCreatedAt = DateTime.UtcNow,
                BlnActive = true
            };

            var newParameter = await ParameterRepository.InsertOneAsync(entity);
            if (newParameter is null)
            {
                responseModel = ResponseModel<ParameterModel>.GetResponse("Entity not created");
                return Results.UnprocessableEntity(responseModel);
            }

            var ParameterModelItem = new ParameterModel
            {
                Id = newParameter.Id,
                StrCode = newParameter.StrCode,
                StrName = newParameter.StrName,
                StrValue = newParameter.StrValue,
                StrParentCode = newParameter.StrParentCode,
                StrDescription = newParameter.StrDescription,
                StrType = newParameter.StrType,
                BlnActive = newParameter.BlnActive,
                StrCreateUser = newParameter.StrCreateUser,
                DteCreatedAt = newParameter.DteCreatedAt
            };
            responseModel = ResponseModel<ParameterModel>.GetResponse(ParameterModelItem);
            return Results.Created($"{path}/{responseModel.Item.Id}", responseModel);
        }

        public async ValueTask<IResult> GetAllParameters()
        {
            ResponseModel<ParameterModel> responseModel;

            Expression<Func<ParameterEntity, bool>> filter = f => true;
            var entities = await ParameterRepository.GetAllAsync(filter);

            var ParameterModelItems = entities.Select(u => new ParameterModel
            {
                Id = u.Id,
                StrCode = u.StrCode,
                StrDescription = u.StrDescription,
                StrName = u.StrName,
                StrParentCode = u.StrParentCode,
                StrType = u.StrType,
                StrValue = u.StrValue,
                BlnActive = u.BlnActive,
                DteCreatedAt = u.DteCreatedAt,
                StrCreateUser = u.StrCreateUser,
                DteUpdatedAt = u.DteUpdatedAt,
                StrUpdateUSer = u.StrUpdateUser
            }).ToList();

            responseModel = ResponseModel<ParameterModel>.GetResponse(ParameterModelItems);
            return Results.Json(responseModel);
        }

        public async ValueTask<IResult> GetActiveParameters()
        {
            ResponseModel<ParameterModel> responseModel;

            Expression<Func<ParameterEntity, bool>> filter = f => f.BlnActive;
            var entities = await ParameterRepository.GetAllAsync(filter);

            var ParameterModelItems = entities.Select(u => new ParameterModel
            {
                Id = u.Id,
                StrCode = u.StrCode,
                StrDescription = u.StrDescription,
                StrName = u.StrName,
                StrParentCode = u.StrParentCode,
                StrType = u.StrType,
                StrValue = u.StrValue,
                BlnActive = u.BlnActive,
                StrCreateUser = u.StrCreateUser,
                DteCreatedAt = u.DteCreatedAt,
                StrUpdateUSer = u.StrUpdateUser,
                DteUpdatedAt = u.DteUpdatedAt
            }).ToList();

            responseModel = ResponseModel<ParameterModel>.GetResponse(ParameterModelItems);
            return Results.Json(responseModel);
        }

        public async ValueTask<IResult> GetParameterById(string id)
        {
            ResponseModel<ParameterModel> responseModel;

            if (string.IsNullOrWhiteSpace(id))
            {
                responseModel = ResponseModel<ParameterModel>.GetResponse("Id is empty");
                return Results.BadRequest(responseModel);
            }

            Expression<Func<ParameterEntity, bool>> filter = f => f.Id == id;
            var entity = await ParameterRepository.GetAsync(filter);
            if (entity is null)
            {
                responseModel = ResponseModel<ParameterModel>.GetResponse("Parameter not found");
                return Results.NotFound(responseModel);
            }

            var ParameterModelItem = new ParameterModel
            {
                Id = entity.Id,
                StrCode = entity.StrCode,
                StrDescription = entity.StrDescription,
                StrName = entity.StrName,
                StrParentCode = entity.StrParentCode,
                StrType = entity.StrType,
                StrValue = entity.StrValue,
                BlnActive = entity.BlnActive,
                StrCreateUser = entity.StrCreateUser,
                DteCreatedAt = entity.DteCreatedAt,
                StrUpdateUSer = entity.StrUpdateUser,
                DteUpdatedAt = entity.DteUpdatedAt
            };

            responseModel = ResponseModel<ParameterModel>.GetResponse(ParameterModelItem);
            return Results.Json(responseModel);
        }

        public async ValueTask<IResult> UpdateParameter(UpdateParameterDTO dto)
        {
            ResponseModel<ParameterModel> responseModel;

            Expression<Func<ParameterEntity, bool>> filter = f => f.Id == dto.Id;
            var entity = await ParameterRepository.GetAsync(filter);
            if (entity is null)
            {
                responseModel = ResponseModel<ParameterModel>.GetResponse("Parameter not exist");
                return Results.NotFound(responseModel);
            }

            entity.StrCode = dto.StrCode;
            entity.StrName = dto.StrName;
            entity.StrValue = dto.StrValue;
            entity.StrType = dto.StrType;
            entity.StrDescription = dto.StrDescription;
            entity.StrUpdateUser = dto.StrUpdateUser;
            entity.DteUpdatedAt = dto.dteUpdatedAt;

            var isParameterUpdated = await ParameterRepository.UpdateOneAsync(entity);

            if (!isParameterUpdated)
            {
                responseModel = ResponseModel<ParameterModel>.GetResponse("Parameter not updated");
                return Results.UnprocessableEntity(responseModel);
            }

            var ParameterModelItem = new ParameterModel
            {
                Id = entity.Id,
                StrCode = entity.StrCode,
                StrDescription = entity.StrDescription,
                StrName = entity.StrName,
                StrParentCode = entity.StrParentCode,
                StrType = entity.StrType,
                StrValue = entity.StrValue,
                BlnActive = entity.BlnActive,
                StrCreateUser = entity.StrCreateUser,
                DteCreatedAt = entity.DteCreatedAt,
                StrUpdateUSer = entity.StrUpdateUser,
                DteUpdatedAt = entity.DteUpdatedAt
            };
            responseModel = ResponseModel<ParameterModel>.GetResponse(ParameterModelItem);
            return Results.Json(responseModel);
        }

        public async ValueTask<IResult> DisableParameter(string id)
        {
            ResponseModel<ParameterModel> responseModel;

            Expression<Func<ParameterEntity, bool>> filter = f => f.Id == id;
            var entity = await ParameterRepository.GetAsync(filter);
            if (entity is null)
            {
                responseModel = ResponseModel<ParameterModel>.GetResponse("Parameter not exist");
                return Results.NotFound(responseModel);
            }

            entity.BlnActive = false;
            var isParameterUpdated = await ParameterRepository.UpdateOneAsync(entity);
            if (!isParameterUpdated)
            {
                responseModel = ResponseModel<ParameterModel>.GetResponse("Parameter not deleted");
                return Results.UnprocessableEntity(responseModel);
            }

            var ParameterModelItem = new ParameterModel
            {
                Id = entity.Id,
                StrCode = entity.StrCode,
                StrDescription = entity.StrDescription,
                StrName = entity.StrName,
                StrParentCode = entity.StrParentCode,
                StrType = entity.StrType,
                StrValue = entity.StrValue,
                BlnActive = entity.BlnActive,
                StrCreateUser = entity.StrCreateUser,
                DteCreatedAt = entity.DteCreatedAt,
                StrUpdateUSer = entity.StrUpdateUser,
                DteUpdatedAt = entity.DteUpdatedAt
            };
            responseModel = ResponseModel<ParameterModel>.GetResponse(ParameterModelItem);
            return Results.Json(responseModel);
        }

        public async ValueTask<IResult> DeleteParameter(string id)
        {
            ResponseModel<ParameterModel> responseModel;

            Expression<Func<ParameterEntity, bool>> filter = f => f.Id == id;
            var entity = await ParameterRepository.GetAsync(filter);
            if (entity is null)
            {
                responseModel = ResponseModel<ParameterModel>.GetResponse("Parameter not exist");
                return Results.NotFound(responseModel);
            }

            var isParameterUpdated = await ParameterRepository.DeleteOneAsync(filter);
            if (!isParameterUpdated)
            {
                responseModel = ResponseModel<ParameterModel>.GetResponse("Parameter not deleted");
                return Results.UnprocessableEntity(responseModel);
            }

            var ParameterModelItem = new ParameterModel
            {
                Id = entity.Id,
                StrCode = entity.StrCode,
                StrDescription = entity.StrDescription,
                StrName = entity.StrName,
                StrParentCode = entity.StrParentCode,
                StrType = entity.StrType,
                StrValue = entity.StrValue,
                BlnActive = entity.BlnActive,
                StrCreateUser = entity.StrCreateUser,
                DteCreatedAt = entity.DteCreatedAt,
                StrUpdateUSer = entity.StrUpdateUser,
                DteUpdatedAt = entity.DteUpdatedAt
            };
            responseModel = ResponseModel<ParameterModel>.GetResponse(ParameterModelItem);
            return Results.Json(responseModel);
        }
    }
}
