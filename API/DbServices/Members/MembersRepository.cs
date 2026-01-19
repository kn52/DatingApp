using API.Data;
using API.DbServices.BaseLayer;
using API.Entities;
using API.Responses;

namespace API.DbServices.Members;

public class MembersRepository(AppDBContext context) : Repository<AppDBContext, AppUser, string>(context)
{
    public async Task<Result<List<AppUser>, StatusInfo>> GetMembers()
    {
        try
        {

            var result = await base.GetAllAsync().ConfigureAwait(false);

            if (result.IsSuccess)
            {
                result.Status = StatusHelper.PrepareStatusInfo(CodeLibrary.Success.Code, "Members fetched successfully.");
            }

            return result;
        }
        catch (Exception ex)
        {
            return StatusHelper.PrepareStatusInfo(CodeLibrary.InternalServerError.Code, ex.Message);
        }
    }
    public async Task<Result<AppUser, StatusInfo>> GetMemberById(string Id)
    {
        try
        {
            var result = await base.GetByIdAsync(Id).ConfigureAwait(false);

            if (result.IsSuccess)
            {
                result.Status = StatusHelper.PrepareStatusInfo(CodeLibrary.Success.Code, "Member fetched successfully.");
            }

            return result;
        }
        catch (Exception ex)
        {
            return StatusHelper.PrepareStatusInfo(CodeLibrary.InternalServerError.Code, ex.Message);
        }
    }
    public async Task<Result<AppUser, StatusInfo>> InsertMember(AppUser appUser)
    {
        try
        {
            var result = new Result<AppUser, StatusInfo>();
            var isExist = await IsMemberExists(appUser).ConfigureAwait(false);


            if (isExist)
            {
                result = CodeLibrary.AlreadyExists;
                return result;
            }

            result = await base.AddAsync(appUser).ConfigureAwait(false);

            if (result.IsSuccess)
            {
                result.Status = StatusHelper.PrepareStatusInfo(CodeLibrary.Success.Code, "Member inserted successfully.");
            }

            return result;
        }
        catch (Exception ex)
        {
            return StatusHelper.PrepareStatusInfo(CodeLibrary.InternalServerError.Code, ex.Message);
        }
    }
    public async Task<Result<AppUser, StatusInfo>> UpdateMember(string Id, AppUser appUser)
    {
        try
        {
            var result = await base.UpdateAsync(Id, appUser).ConfigureAwait(false);

            if (result.IsSuccess)
            {
                result.Status = StatusHelper.PrepareStatusInfo(CodeLibrary.Success.Code, "Member updated successfully.");
            }

            return result;
        }
        catch (Exception ex)
        {
            return StatusHelper.PrepareStatusInfo(CodeLibrary.InternalServerError.Code, ex.Message);
        }
    }
    public async Task<Result<AppUser, StatusInfo>> DeleteMember(string Id)
    {
        try
        {
            var result = await base.DeleteAsync(Id).ConfigureAwait(false);

            if (result.IsSuccess)
            {
                result.Status = StatusHelper.PrepareStatusInfo(CodeLibrary.Success.Code, "Members deleted successfully.");
            }

            return result;
        }
        catch (Exception ex)
        {
            return StatusHelper.PrepareStatusInfo(CodeLibrary.InternalServerError.Code, ex.Message);
        }
    }
    public async Task<bool> IsMemberExists(AppUser appUser)
    {
        try
        {

            var response = await base.GetAllAsync().ConfigureAwait(false);

            if (response.IsSuccess)
            {
                var isExists = response.Value.Where(x => x.Name == appUser.Name && x.Email == appUser.Email).Any();

                if (isExists)
                {
                    return true;
                }
            }

            return false;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public async Task<bool> IsMemberExistsById(string Id)
    {
        try
        {
            var response = await base.GetByIdAsync(Id).ConfigureAwait(false);

            if (response.IsSuccess)
            {
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
