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
        public async Task<Result<AppUser, StatusInfo>> GetMemberById(string Id)
        {

            try
            {
                var result = await _membersRepository.GetMemberById(Id).ConfigureAwait(false);

                if (result.IsSuccess)
                {
                    result.Status = StatusHelper.PrepareStatusInfo(CodeLibrary.Success.Code, "Member fetched successfully.");
                    return result;
                }

                return CodeLibrary.NotFound;
            }
            catch (Exception ex)
            {
                return StatusHelper.PrepareStatusInfo(CodeLibrary.InternalServerError.Code, ex.Message);
            }
        }
        public async Task<Result<AppUser, StatusInfo>> InsertMember(AppUser appUser)
        {

            try
            {
                var result = new Result<AppUser, StatusInfo>();
                var isExist = await _membersRepository.IsMemberExists(appUser).ConfigureAwait(false);
               

                if (isExist)
                {
                    result = CodeLibrary.AlreadyExists;
                    return result;
                }

                result = await _membersRepository.InsertMember(appUser).ConfigureAwait(false);

                if (result.IsSuccess)
                {
                    result.Status = StatusHelper.PrepareStatusInfo(CodeLibrary.Success.Code, "Member inserted successfully.");
                    return result;
                }

                return CodeLibrary.FailedToInsert;
            }
            catch (Exception ex)
            {
                return StatusHelper.PrepareStatusInfo(CodeLibrary.InternalServerError.Code, ex.Message);
            }
        }
        public async Task<Result<AppUser, StatusInfo>> UpdateMember(string Id, AppUser appUser)
        {

            try
            {
                var result = await _membersRepository.UpdateMember(Id, appUser).ConfigureAwait(false);

                if (result.IsSuccess)
                {
                    result.Status = StatusHelper.PrepareStatusInfo(CodeLibrary.Success.Code, "Member updated successfully.");
                    return result;
                }

                return CodeLibrary.FailedToUpdate;
            }
            catch (Exception ex)
            {
                return StatusHelper.PrepareStatusInfo(CodeLibrary.InternalServerError.Code, ex.Message);
            }
        }
        public async Task<Result<AppUser, StatusInfo>> DeleteMember(string Id)
        {

            try
            {
                var result = await _membersRepository.DeleteMember(Id).ConfigureAwait(false);

                if (result.IsSuccess)
                {
                    result.Status = StatusHelper.PrepareStatusInfo(CodeLibrary.Success.Code, "Members deleted successfully.");
                    return result;
                }

                return CodeLibrary.FailedToDelete;
            }
            catch (Exception ex)
            {
                return StatusHelper.PrepareStatusInfo(CodeLibrary.InternalServerError.Code, ex.Message);
            }
        }
    }
}
