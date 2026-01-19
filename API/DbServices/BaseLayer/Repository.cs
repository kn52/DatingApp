namespace API.DbServices.BaseLayer;

using API.Responses;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Result<E, StatusInfo>> GetByIdAsync(K id)
    {
        try
        {
            E? entity = await _dbSet.FindAsync(id);

            if (entity == null)
                return RepositoryWrapper<E>.PrepareErrorResponse(CodeLibrary.NotFound);

            return RepositoryWrapper<E>.PrepareSuccessResponse(CodeLibrary.Success, entity);
        }
        catch (Exception ex)
        {
            return RepositoryWrapper<E>.PrepareErrorResponse(CodeLibrary.InternalServerError, default, ex);
        }
    }


    public async Task<Result<List<E>, StatusInfo>> GetAllAsync()
    {
        try
        {
            List<E>? list = await _dbSet.ToListAsync();

            bool isSuccess = list != null && list.Any();

            if (!isSuccess)
            {
                return RepositoryWrapper<List<E>>.PrepareErrorResponse(CodeLibrary.NotFound);
            }

            return RepositoryWrapper<List<E>>.PrepareSuccessResponse(CodeLibrary.Success, list);
        }
        catch (Exception ex)
        {
            return RepositoryWrapper<List<E>>.PrepareErrorResponse(CodeLibrary.InternalServerError, default, ex);
        }
    }


    public async Task<Result<E, StatusInfo>> AddAsync(E entity)
    {
        try
        {
            await _dbSet.AddAsync(entity);
            int affectedRows = await _context.SaveChangesAsync();

            if (!(affectedRows > 0))
            {
                return RepositoryWrapper<E>.PrepareErrorResponse(CodeLibrary.FailedToInsert, entity);
            }
            return RepositoryWrapper<E>.PrepareSuccessResponse(CodeLibrary.Success, entity);
        }
        catch (Exception ex)
        {
            return RepositoryWrapper<E>.PrepareErrorResponse(CodeLibrary.InternalServerError, entity, ex);
        }
    }

    public async Task<Result<E, StatusInfo>> UpdateAsync(K Id, E entity)
    {
        try
        {
            var existingEntity = await _dbSet.FindAsync(Id);

            if (existingEntity == null)
            {
                return RepositoryWrapper<E>.PrepareErrorResponse(CodeLibrary.NotFound, entity);
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            int affectedRows = await _context.SaveChangesAsync();

            if (!(affectedRows > 0))
            {
                return RepositoryWrapper<E>.PrepareErrorResponse(CodeLibrary.FailedToUpdate, entity);
            }
            return RepositoryWrapper<E>.PrepareSuccessResponse(CodeLibrary.Success, entity);
        }
        catch (Exception ex)
        {
            return RepositoryWrapper<E>.PrepareErrorResponse(CodeLibrary.InternalServerError, entity, ex);
        }
    }

    public async Task<Result<E, StatusInfo>> DeleteAsync(K id)
    {
        try
        {
            E? entity = await _dbSet.FindAsync(id);

            if (entity == null)
                return RepositoryWrapper<E>.PrepareErrorResponse(CodeLibrary.NotFound);

            _dbSet.Remove(entity);
            int affectedRows = await _context.SaveChangesAsync();

            if (!(affectedRows > 0))
            {
                return RepositoryWrapper<E>.PrepareErrorResponse(CodeLibrary.FailedToDelete, entity);
            }
            return RepositoryWrapper<E>.PrepareSuccessResponse(CodeLibrary.Success, entity);
        }
        catch (Exception ex)
        {
            return RepositoryWrapper<E>.PrepareErrorResponse(CodeLibrary.InternalServerError, null, ex);
        }
    }
}
