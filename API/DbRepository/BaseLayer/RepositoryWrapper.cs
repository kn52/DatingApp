using Microsoft.EntityFrameworkCore;

namespace API.DbRepository.BaseLayer;

public class Repository_Wrapper<T, E, K>
    where T : DbContext
    where E : class
{
    private readonly DbContext _context;
    private readonly DbSet<E> _dbSet;

    public Repository_Wrapper(T context)
    {
        _context = context;
        _dbSet = _context.Set<E>();
    }

    public async Task<RepositoryWrapper<E>> GetByIdAsync(K id)
    {
        try
        {
            E? entity = await _dbSet.FindAsync(id);

            if (entity == null)
                return RepositoryWrapper<E>.PrepareRepositoryWrapper(false);

            return RepositoryWrapper<E>.PrepareRepositoryWrapper(true, entity);
        }
        catch (Exception ex)
        {
            return RepositoryWrapper<E>.PrepareRepositoryWrapper(false, default, ex);
        }
    }

    public async Task<RepositoryWrapper<IEnumerable<E>>> GetAllAsync()
    {
        try
        {
            List<E>? list = await _dbSet.ToListAsync();

            bool isSuccess = list != null && list.Any();

            return RepositoryWrapper<IEnumerable<E>>.PrepareRepositoryWrapper(isSuccess, list);
        }
        catch (Exception ex)
        {
            return RepositoryWrapper<IEnumerable<E>>.PrepareRepositoryWrapper(false, default, ex);
        }
    }

    public async Task<RepositoryWrapper<E>> AddAsync(E entity)
    {
        try
        {
            await _dbSet.AddAsync(entity);
            int affectedRows = await _context.SaveChangesAsync();

            return RepositoryWrapper<E>.PrepareRepositoryWrapper(affectedRows > 0, entity);
        }
        catch (Exception ex)
        {
            return RepositoryWrapper<E>.PrepareRepositoryWrapper(false, entity, ex);
        }
    }

    public async Task<RepositoryWrapper<E>> UpdateAsync(K Id, E entity)
    {
        try
        {
            var existingEntity = await _dbSet.FindAsync(Id);

            if (existingEntity == null)
            {
                return RepositoryWrapper<E>.PrepareRepositoryWrapper(false, entity);
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            int affectedRows = await _context.SaveChangesAsync();

            return RepositoryWrapper<E>.PrepareRepositoryWrapper(affectedRows > 0, entity);
        }
        catch (Exception ex)
        {
            return RepositoryWrapper<E>.PrepareRepositoryWrapper(false, entity, ex);
        }
    }

    public async Task<RepositoryWrapper<E>> DeleteAsync(K id)
    {
        try
        {
            E? entity = await _dbSet.FindAsync(id);

            if (entity == null)
                return RepositoryWrapper<E>.PrepareRepositoryWrapper(false);

            _dbSet.Remove(entity);
            int affectedRows = await _context.SaveChangesAsync();
            return RepositoryWrapper<E>.PrepareRepositoryWrapper(affectedRows > 0);
        }
        catch (Exception ex)
        {
            return RepositoryWrapper<E>.PrepareRepositoryWrapper(false, null, ex);
        }
    }
}

public class RepositoryWrapper<T>
{
    public bool Success { get; set; }
    public T Data { get; set; }
    public Exception Exp { get; set; }

    public static RepositoryWrapper<T> PrepareRepositoryWrapper(bool success, T? data = default, Exception? e = null)
    {
        RepositoryWrapper<T> wrapperResponse = new RepositoryWrapper<T>();
        wrapperResponse.Success = success;
        wrapperResponse.Data = data;
        wrapperResponse.Exp = e;

        return wrapperResponse;
    }
}
