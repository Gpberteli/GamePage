using JogoRpg.Data.Context;
using JogoRpg.Domain.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace JogoRpg.Data.Repositories;

public abstract class BaseRepository<TEntity> : IDisposable, IBaseRepository<TEntity> where TEntity : class
{
    private readonly EntityContext _context;
    private readonly DapperContext _dapperContext;

    protected BaseRepository(EntityContext context, DapperContext dapperContext)
    {

        _context = context;
        _dapperContext = dapperContext;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAsync()
    {
        return _context.Set<TEntity>().ToList();
    }

    public virtual async Task<TEntity> GetAsync(long id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public virtual async Task<TEntity> AddAsync(TEntity obj)
    {
        try
        {
            await _context.Set<TEntity>().AddAsync(obj);
            _context.SaveChanges();
            return obj;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity obj)
    {
        try
        {
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return obj;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<TEntity> RemoveAsync(TEntity obj)
    {
        try
        {
            await this.UpdateAsync(obj);
            _context.Set<TEntity>().Remove(obj);
            _context.SaveChanges();
            return obj;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public virtual void Dispose()
    {
        _context.Dispose();
    }

}

