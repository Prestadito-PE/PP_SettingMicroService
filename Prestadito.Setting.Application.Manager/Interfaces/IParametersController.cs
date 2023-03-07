using Microsoft.AspNetCore.Http;
using Prestadito.Setting.Application.Dto.Parameter;

namespace Prestadito.Setting.Application.Manager.Interfaces
{
    public interface IParametersController
    {
        ValueTask<IResult> CreateParameter(CreateParameterDTO dto, string path);
        ValueTask<IResult> GetAllParameters();
        ValueTask<IResult> GetActiveParameters();
        ValueTask<IResult> GetParameterById(string id);
        ValueTask<IResult> GetParameterByCode(string code);
        ValueTask<IResult> UpdateParameter(UpdateParameterDTO dto);
        ValueTask<IResult> DisableParameter(string id);
        ValueTask<IResult> DeleteParameter(string id);
    }
}
