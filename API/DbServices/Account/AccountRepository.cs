using API.Data;
using API.DbServices.BaseLayer;
using API.Entities;

namespace API.DbServices.Account
{
    public class AccountRepository(AppDBContext context) : Repository<AppDBContext, AppUser, string>(context)
    {
    }
}
