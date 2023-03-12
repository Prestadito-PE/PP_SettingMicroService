using MongoDB.Driver;
using Prestadito.Setting.Application.Services.Interfaces;
using Prestadito.Setting.Application.Services.Utilities;
using Prestadito.Setting.Domain.MainModule.Entities;
using System.Linq.Expressions;

namespace Prestadito.Setting.Application.Services.Repositories
{
    public class ParameterRepository: IParameterRepository
    {
        private readonly IMongoCollection<ParameterEntity> collection;

        public ParameterRepository(IMongoDatabase database)
        {
            collection = database.GetCollection<ParameterEntity>(CollectionsName.Parameters);
        }

        public async ValueTask<List<ParameterEntity>> GetAllAsync(Expression<Func<ParameterEntity, bool>> filter)
        {
            var result = await collection.FindAsync(filter);
            return await result.ToListAsync();
        }

        public async ValueTask<ParameterEntity> GetAsync(Expression<Func<ParameterEntity, bool>> filter)
        {
            var result = await collection.FindAsync(filter);
            return await result.SingleOrDefaultAsync();
        }

        public async ValueTask<ParameterEntity> InsertOneAsync(ParameterEntity entity)
        {
            await collection.InsertOneAsync(entity);
            return entity;
        }

        public async ValueTask<bool> UpdateOneAsync(ParameterEntity entity)
        {
            var result = await collection.ReplaceOneAsync(u => u.Id == entity.Id, entity);
            return result.IsAcknowledged;
        }

        public async ValueTask<bool> DeleteOneAsync(Expression<Func<ParameterEntity, bool>> filter)
        {
            var result = await collection.DeleteOneAsync(filter);
            return result.IsAcknowledged;
        }
    }
}
