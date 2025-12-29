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


        [HttpGet("GetMembers")]
        public async Task<ApiResposne<List<AppUser>>> GetMembers()
        {
            var result = new Result<List<AppUser>, StatusInfo>();
            result = await _membersService.GetMembers().ConfigureAwait(false);
            var response = ApiResposne<List<AppUser>>.PrepareResponse(result);
            return response;
        }
        [HttpGet("GetMemberById")]
        public async Task<ApiResposne<AppUser>> GetMemberById([FromQuery] string Id)
        {
            var result = new Result<AppUser, StatusInfo>();

            if (string.IsNullOrEmpty(Id))
            {
                result = CodeLibrary.BadRequest;
                return ApiResposne<AppUser>.PrepareResponse(result);
            }

            result = await _membersService.GetMemberById(Id).ConfigureAwait(false);
            return ApiResposne<AppUser>.PrepareResponse(result);
        }
        [HttpPost("InsertMember")]
        public async Task<ApiResposne<AppUser>> InsertMember([FromBody] AppUser appUser)
        {
            var result = new Result<AppUser, StatusInfo>();

            if (appUser == null)
            {
                result = CodeLibrary.BadRequest;
                return ApiResposne<AppUser>.PrepareResponse(result);
            }

            appUser.Id = Guid.NewGuid().ToString();
            result = await _membersService.InsertMember(appUser).ConfigureAwait(false);
            return ApiResposne<AppUser>.PrepareResponse(result);
        }
        [HttpPut("UpdateMember")]
        public async Task<ApiResposne<AppUser>> UpdateMember([FromBody] AppUser appUser)
        {
            var result = new Result<AppUser, StatusInfo>();

            if (appUser == null)
            {
                result = CodeLibrary.BadRequest;
                return ApiResposne<AppUser>.PrepareResponse(result);
            }
            if (string.IsNullOrEmpty(appUser.Id))
            {
                result = CodeLibrary.BadRequest;
                result.Status.Message = "Invalid Id.";
                return ApiResposne<AppUser>.PrepareResponse(result);
            }

            result = await _membersService.UpdateMember(appUser.Id, appUser).ConfigureAwait(false);
            return ApiResposne<AppUser>.PrepareResponse(result);
        }
        [HttpDelete("DeleteMember")]
        public async Task<ApiResposne<AppUser>> DeleteMember([FromQuery] string Id)
        {
            var result = new Result<AppUser, StatusInfo>();

            if (string.IsNullOrEmpty(Id))
            {
                result = CodeLibrary.BadRequest;
                return ApiResposne<AppUser>.PrepareResponse(result);
            }

            result = await _membersService.GetMemberById(Id).ConfigureAwait(false);
            return ApiResposne<AppUser>.PrepareResponse(result);
        }
    }
}
