using JogoRpg.Domain.Interface.Repositories;
using JogoRpg.Domain.Interface.Services;

namespace JogoRpg.Services.Services
{
    public class BaseService<TEntity> : IDisposable, IBaseService<TEntity> where TEntity : class
    {
        private readonly IBaseRepository<TEntity> _repository;

        public BaseService(IBaseRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public virtual async Task<IEnumerable<TEntity>> Get()
        {
            return await _repository.Get();
        }

        public virtual async Task<TEntity> Get(long id)
        {
            return await _repository.Get(id);
        }

        public virtual async Task<TEntity> Add(TEntity obj)
        {
            return await _repository.Add(obj);
        }

        public virtual async Task<TEntity> Update(TEntity obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj), "O objeto para atualização não pode ser nulo.");
            }

            return await _repository.Update(obj);
        }

        public virtual async Task<TEntity> Remove(long id)
        {
            TEntity entityToRemove = await _repository.Get(id);
            if (entityToRemove != null)
            {
                return await _repository.Remove(entityToRemove);
            }
            return null;
        }
        public void Dispose()
        {
            // Implemente conforme necessário
        }
    }
}