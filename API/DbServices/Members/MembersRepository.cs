using System;
using System.Threading.Tasks;
using API.Data;
using API.DbServices.BaseLayer;
using API.Entities;
using API.Responses;
using Microsoft.EntityFrameworkCore;

namespace API.DbServices.Members;

public class MembersRepository(AppDBContext context) : Repository<AppDBContext, AppUser>(context)
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
}
