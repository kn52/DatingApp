using API.Data;
using API.DbRepository.BaseLayer;
using API.Entities;
using API.Responses;

namespace API.DbRepository.Members;

public class MembersRepository(AppDBContext context) : Repository<AppDBContext, AppUser, string>(context)
{
    public async Task<Result<List<AppUser>, StatusInfo>> GetMembers()
    {
        try
        {

            var response = await base.GetAllAsync().ConfigureAwait(false);

            if (response.Success)
            {
                var result = ResponseWrapper<List<AppUser>>.PrepareResponse(response.Success, response.StatusCode, response.Data, response.exception);
                result.Status.Message = "Members fetched successfully.";
                return result;

            }

            return ResponseWrapper<List<AppUser>>.PrepareResponse(response.Success, response.StatusCode, response.Data, response.exception);
        }
        catch (Exception ex)
        {
            return ResponseWrapper<List<AppUser>>.PrepareResponse(false, ConstantKeys.InternalServerErrorKey, null, ex);
        }
    }
    public async Task<Result<AppUser, StatusInfo>> GetMemberById(string Id)
    {
        try
        {
            var response = await base.GetByIdAsync(Id).ConfigureAwait(false);

            if (response.Success)
            {
                var result = ResponseWrapper<AppUser>.PrepareResponse(response.Success, response.StatusCode, response.Data, response.exception);
                result.Status.Message =  "Member fetched successfully.";
                return result;
            }

            return ResponseWrapper<AppUser>.PrepareResponse(response.Success, response.StatusCode, response.Data, response.exception);
        }
        catch (Exception ex)
        {
            return ResponseWrapper<AppUser>.PrepareResponse(false, ConstantKeys.InternalServerErrorKey, null, ex);
        }
    }
    public async Task<Result<AppUser, StatusInfo>> InsertMember(AppUser appUser)
    {
        try
        {
            var response = new RepositoryWrapper<AppUser>();
            var isExist = await IsMemberExists(appUser).ConfigureAwait(false);


            if (isExist)
            {
                var result = ResponseWrapper<AppUser>.PrepareResponse(response.Success, ConstantKeys.AlreadyExistsKey);
                return result;
            }

            response = await base.AddAsync(appUser).ConfigureAwait(false);

            if (response.Success)
            {
                var result = ResponseWrapper<AppUser>.PrepareResponse(response.Success, response.StatusCode, response.Data, response.exception);
                result.Status.Message = "Member inserted successfully.";
                return result;
            }

            return ResponseWrapper<AppUser>.PrepareResponse(response.Success, response.StatusCode, response.Data, response.exception);
        }
        catch (Exception ex)
        {
            return ResponseWrapper<AppUser>.PrepareResponse(false, ConstantKeys.InternalServerErrorKey, null, ex);
        }
    }
    public async Task<Result<UserIdResponse, StatusInfo>> UpdateMember(string Id, AppUser appUser)
    {
        try
        {
            var response = await base.UpdateAsync(Id, appUser).ConfigureAwait(false);

            if (response.Success)
            {
                var result = ResponseWrapper<UserIdResponse>.PrepareResponse(response.Success, response.StatusCode, new UserIdResponse { Id = response.Data }, response.exception);
                result.Status.Message = "Member updated successfully.";
                return result;
            }

            return ResponseWrapper<UserIdResponse>.PrepareResponse(response.Success, response.StatusCode, new UserIdResponse { Id = response.Data }, response.exception);
        }
        catch (Exception ex)
        {
            return ResponseWrapper<UserIdResponse>.PrepareResponse(false, ConstantKeys.InternalServerErrorKey, new UserIdResponse { Id = Id }, ex);
        }
    }
    public async Task<Result<UserIdResponse, StatusInfo>> DeleteMember(string Id)
    {
        try
        {
            var response = await base.DeleteAsync(Id).ConfigureAwait(false);

            if (response.Success)
            {
                var result = ResponseWrapper<UserIdResponse>.PrepareResponse(response.Success, response.StatusCode, new UserIdResponse { Id = response.Data }, response.exception);
                result.Status.Message = "Members deleted successfully.";
                return result;
            }

            return ResponseWrapper<UserIdResponse>.PrepareResponse(response.Success, response.StatusCode, new UserIdResponse { Id = response.Data }, response.exception);
        }
        catch (Exception ex)
        {
            return ResponseWrapper<UserIdResponse>.PrepareResponse(false, ConstantKeys.InternalServerErrorKey, new UserIdResponse { Id = Id }, ex);
        }
    }
    public async Task<bool> IsMemberExists(AppUser appUser)
    {
        try
        {

            var response = await base.GetAllAsync().ConfigureAwait(false);

            if (response.Success)
            {
                var isExists = response.Data.Where(x => x.Name == appUser.Name && x.Email == appUser.Email).Any();

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

            if (response.Success)
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
