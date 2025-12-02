using API.Entities;
using API.Responses;

namespace API.DbServices.Members
{
    public class MembersService
    {
        private readonly MembersRepository _membersRepository;

        public MembersService(MembersRepository membersRepository)
        {
            _membersRepository = membersRepository;
        }
        public async Task<Result<List<AppUser>, StatusInfo>> GetMembers()
        {

            try
            {
                var result = await _membersRepository.GetMembers().ConfigureAwait(false);

                if (result.IsSuccess)
                {
                    result.Status = StatusHelper.PrepareStatusInfo(CodeLibrary.Success.Code, "Members fetched successfully.");
                    return result;
                }

                return CodeLibrary.NotFound;
            }
            catch (Exception ex)
            {
                return StatusHelper.PrepareStatusInfo(CodeLibrary.InternalServerError.Code, ex.Message);
            }
        }
    }
}
