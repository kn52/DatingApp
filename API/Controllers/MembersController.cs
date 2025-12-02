using API.DbServices.Members;
using API.Entities;
using API.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly MembersService  _membersService;

        public MembersController(
             MembersService membersService)
        {
            _membersService = membersService;
        }


        [HttpPost("GetMembers")]
        public async Task<ApiResposne<List<AppUser>>> GetMembers()
        {
            var result = await _membersService.GetMembers().ConfigureAwait(false);
            var response = ApiResposne<List<AppUser>>.PrepareResponse(result);
            return response;
        }
    }
}
