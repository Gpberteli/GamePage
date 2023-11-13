namespace JogoRpg.Domain.Interface.Services;

public interface IBaseService<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> Get();
    Task<TEntity> Get(long id);
    Task<TEntity> Add(TEntity obj);
    Task<TEntity> Update(TEntity obj);
    Task<TEntity> Remove(TEntity obj);

    void Dispose();
}
