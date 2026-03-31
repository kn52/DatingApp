using API.Data;
using API.DbRepository.BaseLayer;
using API.Entities;
using API.Responses;
using Microsoft.EntityFrameworkCore;

namespace API.DbRepository.Members;

public class MembersRepository(AppDBContext context)
    : Repository<AppDBContext, AppUser, string>(context)
{
    public async Task<ApiResposne<List<AppUser>>> GetMembers()
    {
        ApiResposne<List<AppUser>> response;

        try
        {
            var data = await base.GetAllAsync();

            response = new ApiResposne<List<AppUser>>
            {
                Success = data.Any(),
                Message = data.Any() ? "Members fetched successfully." : "No members found.",
                StatusCode = 200,
                Data = data
            };
        }
        catch (Exception ex)
        {
            response = new ApiResposne<List<AppUser>>
            {
                Success = false,
                Message = ex.Message,
                StatusCode = 500,
                Data = []
            };
        }

        return response;
    }

    public async Task<ApiResposne<AppUser>> GetMemberById(string id)
    {
        ApiResposne<AppUser> response;

        try
        {
            var data = await base.GetByIdAsync(id);

            response = new ApiResposne<AppUser>
            {
                Success = data != null,
                Message = data != null ? "Member fetched successfully." : "Member not found.",
                StatusCode = data != null ? 200 : 404,
                Data = data
            };
        }
        catch (Exception ex)
        {
            response = new ApiResposne<AppUser>
            {
                Success = false,
                Message = ex.Message,
                StatusCode = 500,
                Data = null
            };
        }

        return response;
    }

    public async Task<ApiResposne<AppUser>> InsertMember(AppUser appUser)
    {
        ApiResposne<AppUser> response;

        try
        {
            var exists = await context.Set<AppUser>()
                .AnyAsync(x => x.Name == appUser.Name && x.Email == appUser.Email);

            if (exists)
            {
                response = new ApiResposne<AppUser>
                {
                    Success = false,
                    Message = "Member already exists.",
                    StatusCode = 409,
                    Data = null
                };

                return response;
            }

            var data = await base.AddAsync(appUser);

            response = new ApiResposne<AppUser>
            {
                Success = data != null,
                Message = data != null ? "Member inserted successfully." : "Insert failed.",
                StatusCode = data != null ? 201 : 500,
                Data = data
            };
        }
        catch (Exception ex)
        {
            response = new ApiResposne<AppUser>
            {
                Success = false,
                Message = ex.Message,
                StatusCode = 500,
                Data = null
            };
        }

        return response;
    }

    public async Task<ApiResposne<string>> UpdateMember(string id, AppUser appUser)
    {
        ApiResposne<string> response;

        try
        {
            var updated = await base.UpdateAsync(id, appUser);

            response = new ApiResposne<string>
            {
                Success = updated != null,
                Message = updated != null ? "Member updated successfully." : "Member not found.",
                StatusCode = updated != null ? 200 : 404,
                Data = id
            };
        }
        catch (Exception ex)
        {
            response = new ApiResposne<string>
            {
                Success = false,
                Message = ex.Message,
                StatusCode = 500,
                Data = id
            };
        }

        return response;
    }

    public async Task<ApiResposne<string>> DeleteMember(string id)
    {
        ApiResposne<string> response;

        try
        {
            var deleted = await base.DeleteAsync(id);

            response = new ApiResposne<string>
            {
                Success = deleted,
                Message = deleted ? "Member deleted successfully." : "Member not found.",
                StatusCode = deleted ? 200 : 404,
                Data = id
            };
        }
        catch (Exception ex)
        {
            response = new ApiResposne<string>
            {
                Success = false,
                Message = ex.Message,
                StatusCode = 500,
                Data = id
            };
        }

        return response;
    }
}