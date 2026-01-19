using API.DbServices.Members;
using API.Entities;
using API.Responses;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly MembersRepository _membersRepository;

        public MembersController(
             MembersRepository membersRepository)
        {
            _membersRepository = membersRepository;
        }


        [HttpGet("GetMembers")]
        public async Task<ApiResposne<List<AppUser>>> GetMembers()
        {
            var result = new Result<List<AppUser>, StatusInfo>();
            result = await _membersRepository.GetMembers().ConfigureAwait(false);
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

            result = await _membersRepository.GetMemberById(Id).ConfigureAwait(false);
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
            result = await _membersRepository.InsertMember(appUser).ConfigureAwait(false);
            return ApiResposne<AppUser>.PrepareResponse(result);
        }
        [HttpPost("UpdateMember")]
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

            result = await _membersRepository.UpdateMember(appUser.Id, appUser).ConfigureAwait(false);
            return ApiResposne<AppUser>.PrepareResponse(result);
        }
        [HttpGet("DeleteMember")]
        public async Task<ApiResposne<AppUser>> DeleteMember([FromQuery] string Id)
        {
            var result = new Result<AppUser, StatusInfo>();

            if (string.IsNullOrEmpty(Id))
            {
                result = CodeLibrary.BadRequest;
                return ApiResposne<AppUser>.PrepareResponse(result);
            }

            result = await _membersRepository.GetMemberById(Id).ConfigureAwait(false);
            return ApiResposne<AppUser>.PrepareResponse(result);
        }
    }
}
