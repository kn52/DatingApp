using API.DbRepository.Members;
using API.Entities;
using API.Responses;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly MembersRepository  _membersRepository;

        public MembersController(
             MembersRepository membersRepository)
        {
            _membersRepository = membersRepository;
        }


        [HttpGet("GetMembers")]
        public async Task<ApiResposne<List<AppUser>>> GetMembers()
        {
            var result = await _membersRepository.GetMembers().ConfigureAwait(false);
            return result;
        }
        [HttpGet("GetMemberById")]
        public async Task<ApiResposne<AppUser>> GetMemberById([FromQuery] string Id)
        {
            var result = new ApiResposne<AppUser>();

            if (string.IsNullOrEmpty(Id))
            {
                result.StatusCode = CodeLibrary.BadRequest.Code;
                result.Message = CodeLibrary.BadRequest.Message;
                return result;
            }

            result = await _membersRepository.GetMemberById(Id).ConfigureAwait(false);
            return result;
        }
        [HttpPost("AddMember")]
        public async Task<ApiResposne<AppUser>> AddMember([FromBody] AppUser appUser)
        {
            var result = new ApiResposne<AppUser>();

            if (appUser == null)
            {
                result.StatusCode = CodeLibrary.BadRequest.Code;
                result.Message = CodeLibrary.BadRequest.Message;
                return result;
            }

            appUser.Id = Guid.NewGuid().ToString();
            result = await _membersRepository.InsertMember(appUser).ConfigureAwait(false);
            return result;
        }
        [HttpPut("UpdateMember")]
        public async Task<ApiResposne<string>> UpdateMember([FromBody] AppUser appUser)
        {
            var result = new ApiResposne<string>();

            if (appUser == null)
            {
                result.StatusCode = CodeLibrary.BadRequest.Code;
                result.Message = CodeLibrary.BadRequest.Message;
                return result;
            }
            if (string.IsNullOrEmpty(appUser.Id))
            {
                result.StatusCode = CodeLibrary.BadRequest.Code;
                result.Message = "Invalid Id.";
                return result;
            }

            result = await _membersRepository.UpdateMember(appUser.Id, appUser).ConfigureAwait(false);
            return result;
        }
        [HttpDelete("DeleteMember")]
        public async Task<ApiResposne<string>> DeleteMember([FromQuery] string Id)
        {
            var result = new ApiResposne<string>();

            if (string.IsNullOrEmpty(Id))
            {
                result.StatusCode = CodeLibrary.BadRequest.Code;
                result.Message = CodeLibrary.BadRequest.Message;
                return result;
            }

            result = await _membersRepository.DeleteMember(Id).ConfigureAwait(false);
            return result;
        }
    }
}
