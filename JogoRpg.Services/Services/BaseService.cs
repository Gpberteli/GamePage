using JogoRpg.Domain.Interface.Repositories;
using JogoRpg.Domain.Interface.Services;

namespace JogoRpg.Services.Services;

public class BaseService<TEntity> : IDisposable, IBaseService<TEntity> where TEntity : class
{
    private readonly IBaseRepository<TEntity> _repository;

    public BaseService(IBaseRepository<TEntity> Repository)
    {
        _repository = Repository;
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
        return await _repository.Update(obj);
    }

    public virtual async Task<TEntity> Remove(TEntity obj)
    {
        return await _repository.Remove(obj);
    }

    public void Dispose()
    {

    }
}
