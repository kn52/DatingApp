using Microsoft.EntityFrameworkCore;

namespace API.DbRepository.BaseLayer;

public class Repository<T, E, K>
    where T : DbContext
    where E : class
{
    private readonly DbContext _context;
    private readonly DbSet<E> _dbSet;

    public Repository(T context)
    {
        _context = context;
        _dbSet = _context.Set<E>();
    }

    // ✅ GET ALL
    public async Task<List<E>> GetAllAsync()
    {
        try
        {
            return await _dbSet.ToListAsync() ?? [];
        }
        catch
        {
            return [];
        }
    }

    // ✅ GET BY ID
    public async Task<E?> GetByIdAsync(K id)
    {
        try
        {
            return await _dbSet.FindAsync(id);
        }
        catch
        {
            return null;
        }
    }

    // ✅ ADD
    public async Task<E?> AddAsync(E entity)
    {
        try
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch
        {
            return null;
        }
    }

    // ✅ UPDATE
    public async Task<E?> UpdateAsync(K id, E entity)
    {
        try
        {
            var existingEntity = await _dbSet.FindAsync(id);

            if (existingEntity == null)
                return null;

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

            return existingEntity;
        }
        catch
        {
            return null;
        }
    }

    // ✅ DELETE
    public async Task<bool> DeleteAsync(K id)
    {
        try
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity == null)
                return false;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }
        catch
        {
            return false;
        }
    }
}