using API.Data;
using API.DbRepository.BaseLayer;
using API.Entities;
using API.Responses;

namespace API.DbRepository.Members;

public class MembersRepository(AppDBContext context) : Repository<AppDBContext, AppUser, string>(context)
{
    public async Task<Result<List<AppUser>, StatusInfo>> GetMembers()
    {
        var result = new Result<List<AppUser>, StatusInfo>();
        try
        {
            result = await base.GetAllAsync().ConfigureAwait(false);

            if (result.IsSuccess)
            {
                result.Status = StatusHelper.PrepareStatusInfo(CodeLibrary.Success.Code, "Members fetched successfully.");
                return result;
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
    public async Task<Result<AppUser, StatusInfo>> GetMemberById(string Id)
    {
        var result = new Result<AppUser, StatusInfo>();
        try
        {
            result = await base.GetByIdAsync(Id).ConfigureAwait(false);

            if (result.IsSuccess)
            {
                result.Status = StatusHelper.PrepareStatusInfo(CodeLibrary.Success.Code, "Member fetched successfully.");
                return result;
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
    public async Task<Result<UserIdResponse, StatusInfo>> InsertMember(AppUser appUser)
    {

        var result = new Result<UserIdResponse, StatusInfo>();
        try
        {
            var (isExist, entity) = await IsMemberExists(appUser).ConfigureAwait(false);

            if (isExist)
            {
                result.IsSuccess = false;
                result = CodeLibrary.AlreadyExists;
                result.Value = new UserIdResponse()
                {
                    Id = entity.Id,
                };
                return result;
            }

            var response = await base.AddAsync(appUser).ConfigureAwait(false);

            if (response.IsSuccess)
            {
                result.IsSuccess = true;
                result.Value = new UserIdResponse()
                {
                    Id = appUser.Id,
                }; 
                result.Status = StatusHelper.PrepareStatusInfo(CodeLibrary.Success.Code, "Member inserted successfully.");
                return result;
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
    public async Task<Result<UserIdResponse, StatusInfo>> UpdateMember(string Id, AppUser appUser)
    {
        var result = new Result<UserIdResponse, StatusInfo>();
        try
        {
            result.Value = new UserIdResponse()
            {
                Id = appUser.Id,
            };
            var response = await base.UpdateAsync(Id, appUser).ConfigureAwait(false);

            if (response.IsSuccess)
            {
                result.IsSuccess = true;
                result.Status = StatusHelper.PrepareStatusInfo(CodeLibrary.Success.Code, "Member updated successfully.");
                return result;
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
    public async Task<Result<UserIdResponse, StatusInfo>> DeleteMember(string Id)
    {
        var result = new Result<UserIdResponse, StatusInfo>();
        try
        {
            var response = await base.DeleteAsync(Id).ConfigureAwait(false);
            result.Value = new UserIdResponse()
            {
                Id = Id,
            };
            if (response.IsSuccess)
            {
                result.IsSuccess = true;
                
                result.Status = StatusHelper.PrepareStatusInfo(CodeLibrary.Success.Code, "Members deleted successfully.");
                return result;
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
    public async Task<(bool, AppUser)> IsMemberExists(AppUser appUser)
    {

        var response = await base.GetAllAsync().ConfigureAwait(false);

        if (response.IsSuccess && response.Value != null)
        {
            var entity = response.Value
                .FirstOrDefault(x => x.Name == appUser.Name && x.Email == appUser.Email);

            if (entity != null)
            {
                return (true, entity);
            }
        }

        return (false, null);
    }
    public async Task<bool> IsMemberExistsById(string Id)
    {
        var response = await base.GetByIdAsync(Id).ConfigureAwait(false);

        if (response.IsSuccess)
        {
            return true;
        }

        return false;
    }
}
