using JogoRpg.Data.Context;
using JogoRpg.Domain.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

public abstract class BaseRepository<TEntity> : IDisposable, IBaseRepository<TEntity> where TEntity : class
{
    private readonly EntityContext _context;
    private readonly DapperContext _dapperContext;

    protected BaseRepository(EntityContext context, DapperContext dapperContext)
    {
        _context = context;
        _dapperContext = dapperContext;
    }

    public virtual async Task<IEnumerable<TEntity>> Get()
    {
        return _context.Set<TEntity>().ToList();
    }

    public virtual async Task<TEntity> Get(long id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public virtual async Task<TEntity> Add(TEntity obj)
    {
        using (var transaction = _context.Database.BeginTransaction())
        {
            try
            {
                await _context.Set<TEntity>().AddAsync(obj);
                _context.SaveChanges();
                transaction.Commit();
                return obj;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }
    }

    public virtual async Task<TEntity> Update(TEntity obj)
    {
        if (obj == null)
        {
            throw new ArgumentNullException(nameof(obj), "Objeto de atualização não pode ser nulo.");
        }

        using (var transaction = _context.Database.BeginTransaction())
        {
            try
            {
                _context.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                transaction.Commit();
                return obj;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }
    }

    public async Task<TEntity> Remove(TEntity obj)
    {
        using (var transaction = _context.Database.BeginTransaction())
        {
            try
            {
                await this.Update(obj);
                _context.Set<TEntity>().Remove(obj);
                _context.SaveChanges();
                transaction.Commit();
                return obj;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }
    }

    public virtual void Dispose()
    {
        _context.Dispose();
    }
}
