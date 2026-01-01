using System;

namespace API.DbRepository.BaseLayer;

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

    public async Task<Result<List<E>, StatusInfo>> GetAllAsync()
    {
        Result<List<E>, StatusInfo> result = new();
        try
        {
            List<E>? list = await _dbSet.ToListAsync();

            bool isSuccess = list != null && list.Any();

            if (isSuccess)
            {
                return list;
            }

            result.Status = StatusHelper.PrepareStatusInfo(CodeLibrary.NotFound.Code, CodeLibrary.NotFound.Message);
            return result;

        }
        catch (Exception ex)
        {
            result.Status = StatusHelper.PrepareStatusInfo(CodeLibrary.InternalServerError.Code, ex.Message);
            return result;
        }
    }

    public async Task<Result<E, StatusInfo>> GetByIdAsync(K id)
    {
        Result<E, StatusInfo> result = new();

        try
        {
            E? entity = await _dbSet.FindAsync(id);

            if (entity == null)
            {
                result.Status = StatusHelper.PrepareStatusInfo(CodeLibrary.NotFound.Code, CodeLibrary.NotFound.Message);
                return result;
            }

            return entity;
        }
        catch (Exception ex)
        {
            result.Status = StatusHelper.PrepareStatusInfo(CodeLibrary.InternalServerError.Code, ex.Message);
            return result;
        }
    }

    public async Task<Result<E, StatusInfo>> AddAsync(E entity)
    {
        Result<E, StatusInfo> result = new();
        try
        {
            await _dbSet.AddAsync(entity);
            int affectedRows = await _context.SaveChangesAsync();

            if (affectedRows > 0)
            {
                return entity;
            }

            result.Status = StatusHelper.PrepareStatusInfo(CodeLibrary.FailedToInsert.Code, CodeLibrary.FailedToInsert.Message);
            return result;
        }
        catch (Exception ex)
        {
            result.Status = StatusHelper.PrepareStatusInfo(CodeLibrary.InternalServerError.Code, ex.Message);
            return result;
        }
    }

    public async Task<Result<E, StatusInfo>> UpdateAsync(K Id, E entity)
    {
        Result<E, StatusInfo> result = new();
        try
        {
            var existingEntity = await _dbSet.FindAsync(Id);

            if (existingEntity == null)
            {
                result.Status = StatusHelper.PrepareStatusInfo(CodeLibrary.NotFound.Code, CodeLibrary.NotFound.Message);
                return result;
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            int affectedRows = await _context.SaveChangesAsync();

            if (affectedRows > 0)
            {
                return entity;
            }

            result.Status = StatusHelper.PrepareStatusInfo(CodeLibrary.FailedToUpdate.Code, CodeLibrary.FailedToUpdate.Message);
            return result;
        }
        catch (Exception ex)
        {
            result.Status = StatusHelper.PrepareStatusInfo(CodeLibrary.InternalServerError.Code, ex.Message);
            return result;
        }
    }

    public async Task<Result<E, StatusInfo>> DeleteAsync(K id)
    {
        Result<E, StatusInfo> result = new();
        try
        {
            E? entity = await _dbSet.FindAsync(id);

            if (entity == null)
            {
                result.Status = StatusHelper.PrepareStatusInfo(CodeLibrary.NotFound.Code, CodeLibrary.NotFound.Message);
                return result;
            }

            _dbSet.Remove(entity);
            int affectedRows = await _context.SaveChangesAsync();

            if (affectedRows > 0)
            {
                return entity;
            }

            result.Status = StatusHelper.PrepareStatusInfo(CodeLibrary.FailedToDelete.Code, CodeLibrary.FailedToDelete.Message);
            return result;
        }
        catch (Exception ex)
        {
            result.Status = StatusHelper.PrepareStatusInfo(CodeLibrary.InternalServerError.Code, ex.Message);
            return result;
        }
    }
}
