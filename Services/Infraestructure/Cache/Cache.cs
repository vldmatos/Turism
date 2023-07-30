using Microsoft.Extensions.Caching.Memory;
using Shared.Entities;

namespace Services.Infraestructure.Cache
{
    public interface ICache
    {
        #region Cache Members

        Task<TEntity> Find<TEntity>(string key) where TEntity : Entity;

        Task<TEntities> FindAll<TEntities>(string key) where TEntities : IEnumerable<Entity>;

        Task Set<TEntity>(string key, TEntity entity, TimeSpan expiration) where TEntity : Entity;

        Task SetAll<TEntities>(string key, TEntities entities, TimeSpan expiration) where TEntities : IEnumerable<Entity>;

        Task Remove(string key);

        #endregion Cache Members
    }

    public class Cache : ICache
    {
        private readonly IMemoryCache _memoryCache;

        public Cache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        #region Cache Members

        public Task<TEntity> Find<TEntity>(string key) where TEntity : Entity
        {
            return (string.IsNullOrEmpty(key)) ? null : Task.FromResult(_memoryCache.Get<TEntity>(key));
        }

        public Task<TEntities> FindAll<TEntities>(string key) where TEntities : IEnumerable<Entity>
        {
            return (string.IsNullOrEmpty(key)) ? null : Task.FromResult(_memoryCache.Get<TEntities>(key));
        }

        public Task Remove(string key)
        {
            if (string.IsNullOrEmpty(key))
                return default;

            return Task.Factory.StartNew(() => _memoryCache.Remove(key));
        }

        public Task Set<TEntity>(string key, TEntity entity, TimeSpan expiration) where TEntity : Entity
        {
            if (string.IsNullOrEmpty(key) || entity is null)
                return default;

            return Task.Factory.StartNew(() => _memoryCache.Set(key, entity, expiration));
        }

        public Task SetAll<TEntities>(string key, TEntities entities, TimeSpan expiration) where TEntities : IEnumerable<Entity>
        {
            if (string.IsNullOrEmpty(key) || entities is null || !entities.Any())
                return Task.FromResult(Enumerable.Empty<Entity>);

            return Task.Factory.StartNew(() => _memoryCache.Set(key, entities, expiration));
        }

        #endregion Cache Members
    }
}