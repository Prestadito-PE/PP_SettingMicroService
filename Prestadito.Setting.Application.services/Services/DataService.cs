using Prestadito.Setting.Application.Services.Interfaces;
using Prestadito.Setting.Application.Services.Repositories;
using Prestadito.Setting.Infrastructure.Data.Context;

namespace Prestadito.Setting.Application.Services.Services
{
    public class DataService: IDataService
    {
        private readonly MongoContext context;

        public DataService(MongoContext _context)
        {
            context = _context;
        }
        public IParameterRepository Parameters
        {
            get
            {
                return new ParameterRepository(context.Database);
            }
        }
    }
}
