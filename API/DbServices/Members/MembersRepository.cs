using API.Data;
using API.DbServices.BaseLayer;
using API.Entities;
using API.Responses;

namespace API.DbServices.Members;

public class MembersRepository(AppDBContext context) : Repository<AppDBContext, AppUser, string>(context)
{
    public async Task<Result<List<AppUser>, StatusInfo>> GetMembers()
    {

        var response = await base.GetAllAsync().ConfigureAwait(false);

        if (response.Success)
        {
            return response.Data.ToList();
        }

        return CodeLibrary.InternalServerError;
    }
    public async Task<Result<AppUser, StatusInfo>> GetMemberById(string Id)
    {

        var response = await base.GetByIdAsync(Id).ConfigureAwait(false);

        if (response.Success)
        {
            return response.Data;
        }

        return CodeLibrary.InternalServerError;
    }
    public async Task<Result<AppUser, StatusInfo>> InsertMember(AppUser appUser)
    {

        var response = await base.AddAsync(appUser).ConfigureAwait(false);

        if (response.Success)
        {
            return response.Data;
        }

        return CodeLibrary.InternalServerError;
    }
    public async Task<Result<AppUser, StatusInfo>> UpdateMember(string Id, AppUser appUser)
    {

        var response = await base.UpdateAsync(Id, appUser).ConfigureAwait(false);

        if (response.Success)
        {
            return response.Data;
        }

        return CodeLibrary.InternalServerError;
    }
    public async Task<Result<AppUser, StatusInfo>> DeleteMember(string Id)
    {

        var response = await base.DeleteAsync(Id).ConfigureAwait(false);

        if (response.Success)
        {
            return response.Data;
        }

        return CodeLibrary.InternalServerError;
    }
    public async Task<bool> IsMemberExists(AppUser appUser)
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
    public async Task<bool> IsMemberExistsById(string Id)
    {
        var response = await base.GetByIdAsync(Id).ConfigureAwait(false);

        if (response.Success)
        {
            return true;
        }

        return false;
    }
}
