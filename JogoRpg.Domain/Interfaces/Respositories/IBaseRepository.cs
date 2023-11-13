namespace JogoRpg.Domain.Interface.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAsync();
    Task<TEntity> GetAsync (long id);
    Task<TEntity> AddAsync (TEntity obj);
    Task<TEntity> UpdateAsync (TEntity obj);
    Task<TEntity> RemoveAsync (TEntity obj);

    void Dispose();

}
