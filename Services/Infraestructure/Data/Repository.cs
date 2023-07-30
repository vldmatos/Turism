using Microsoft.EntityFrameworkCore;
using Shared.Entities;

namespace Services.Infrastructure.Data
{
    public interface IRepository
    {
        Task<TEntity> Save<TEntity>(TEntity entity) where TEntity : Entity;

        Task Delete<TEntity>(TEntity entity) where TEntity : Entity;

        ValueTask<TEntity> Find<TEntity>(TEntity entity) where TEntity : Entity;

        ValueTask<IEnumerable<TEntity>> GetAll<TEntity>(TEntity entity) where TEntity : Entity;
    }

    public class Repository : IRepository
    {
        private readonly DataContext _dataContext;

        public Repository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #region Repository Members

        public async Task Delete<TEntity>(TEntity entity) where TEntity : Entity
        {
            var find = await _dataContext.Set<TEntity>()
                                         .FindAsync(entity.Id);

            if (find is not null)
            {
                _dataContext.Set<TEntity>()
                            .Remove(find);

                await _dataContext.SaveChangesAsync();
            }
        }

        public ValueTask<TEntity> Find<TEntity>(TEntity entity) where TEntity : Entity
        {
            return _dataContext.Set<TEntity>()
                               .FindAsync(entity.Id);
        }

        public async ValueTask<IEnumerable<TEntity>> GetAll<TEntity>(TEntity entity) where TEntity : Entity
        {
            return await _dataContext.Set<TEntity>()
                                     .AsNoTracking()
                                     .ToListAsync();
        }

        public async Task<TEntity> Save<TEntity>(TEntity entity) where TEntity : Entity
        {
            var find = await _dataContext.Set<TEntity>()
                                         .FindAsync(entity.Id);
            if (find is not null)
            {
                _dataContext.Remove(find);
                entity.UpdateAt = DateTime.UtcNow;

                _dataContext.Entry(entity)
                            .State = EntityState.Modified;

                _dataContext.Update(entity);

                await _dataContext.SaveChangesAsync();
                return entity;
            }
            else
            {
                _dataContext.Set<TEntity>()
                            .Add(entity);

                await _dataContext.SaveChangesAsync();
                return entity;
            }
        }

        #endregion Repository Members
    }
}