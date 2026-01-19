using API.Responses;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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

    public async Task<RepositoryWrapper<List<E>>> GetAllAsync()
    {
        try
        {
            List<E>? list = await _dbSet.ToListAsync();

            bool isSuccess = list != null && list.Any();

            if (isSuccess)
            {
                return RepositoryWrapper<List<E>>.PrepareSuccessResponse(ConstantKeys.SuccessKey, list);
            }

            return RepositoryWrapper<List<E>>.PrepareErrorResponse(ConstantKeys.NotFoundKey, list);

        }
        catch (Exception ex)
        {
            return RepositoryWrapper<List<E>>.PrepareErrorResponse(ConstantKeys.InternalServerErrorKey, null, ex);
        }
    }

    public async Task<RepositoryWrapper<E>> GetByIdAsync(K id)
    {
        try
        {
            E? entity = await _dbSet.FindAsync(id);

            if (entity == null)
            {
                return RepositoryWrapper<E>.PrepareErrorResponse(ConstantKeys.NotFoundKey);
            }

            return RepositoryWrapper<E>.PrepareSuccessResponse(ConstantKeys.SuccessKey, entity);
        }
        catch (Exception ex)
        {
            return RepositoryWrapper<E>.PrepareErrorResponse(ConstantKeys.InternalServerErrorKey, null, ex);
        }
    }

    public async Task<RepositoryWrapper<E>> AddAsync(E entity)
    {
        Result<E, StatusInfo> result = new();
        try
        {
            await _dbSet.AddAsync(entity);
            int affectedRows = await _context.SaveChangesAsync();

            if (affectedRows > 0)
            {
                return RepositoryWrapper<E>.PrepareSuccessResponse(ConstantKeys.SuccessKey, entity);
            }

            return RepositoryWrapper<E>.PrepareErrorResponse(ConstantKeys.FailedToInsertKey, default);
        }
        catch (Exception ex)
        {
            return RepositoryWrapper<E>.PrepareErrorResponse(ConstantKeys.InternalServerErrorKey, default, ex);
        }
    }

    public async Task<RepositoryWrapper<K>> UpdateAsync(K Id, E entity)
    {
        try
        {
            var existingEntity = await _dbSet.FindAsync(Id);

            if (existingEntity == null)
            {
                return RepositoryWrapper<K>.PrepareErrorResponse(ConstantKeys.NotFoundKey);
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            int affectedRows = await _context.SaveChangesAsync();

            if (affectedRows > 0)
            {
                return RepositoryWrapper<K>.PrepareSuccessResponse(ConstantKeys.SuccessKey, Id);
            }

            return RepositoryWrapper<K>.PrepareErrorResponse(ConstantKeys.FailedToInsertKey, Id);
        }
        catch (Exception ex)
        {
            return RepositoryWrapper<K>.PrepareErrorResponse(ConstantKeys.InternalServerErrorKey, Id, ex);
        }
    }

    public async Task<RepositoryWrapper<K>> DeleteAsync(K id)
    {
        Result<E, StatusInfo> result = new();
        try
        {
            E? entity = await _dbSet.FindAsync(id);

            if (entity == null)
            {
                return RepositoryWrapper<K>.PrepareErrorResponse(ConstantKeys.NotFoundKey, id);
            }

            _dbSet.Remove(entity);
            int affectedRows = await _context.SaveChangesAsync();

            if (affectedRows > 0)
            {
                return RepositoryWrapper<K>.PrepareSuccessResponse(ConstantKeys.SuccessKey, id);
            }

            return RepositoryWrapper<K>.PrepareErrorResponse(ConstantKeys.FailedToDeleteKey, id);
        }
        catch (Exception ex)
        {
            return RepositoryWrapper<K>.PrepareErrorResponse(ConstantKeys.InternalServerErrorKey, id, ex);
        }
    }
}
