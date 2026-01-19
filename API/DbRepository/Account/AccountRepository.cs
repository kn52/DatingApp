using API.Data;
using API.DbRepository.BaseLayer;
using API.Entities;

namespace API.DbRepository.Account
{
    public class AccountRepository(AppDBContext context) : Repository<AppDBContext, AppUser, string>(context)
    {
    }
}
