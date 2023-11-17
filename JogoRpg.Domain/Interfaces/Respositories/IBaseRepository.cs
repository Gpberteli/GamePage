namespace JogoRpg.Domain.Interface.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> Get();
    Task<TEntity> Get(long id);
    Task<TEntity> Add(TEntity obj);
    Task<TEntity> Update(TEntity obj);
    Task<TEntity> Remove(TEntity obj);

    void Dispose();

}
