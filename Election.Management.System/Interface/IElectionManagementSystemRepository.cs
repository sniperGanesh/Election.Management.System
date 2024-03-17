


using Election.Management.System.Models;

namespace Election.Management.System.Interface
{
    public interface IElectionManagementSystemRepository
    {

        Task<CommonResponse> UpdateMPSeats(int stateId, int newSeatCount);
        Task<CommonResponse> GetMPSeats(int stateId);
        Task<CommonResponse> RegisterParty(Party party);
        Task<CommonResponse> GetParty(int id);

        Task<CommonResponse> RegisterVoter(Voter voter);
        Task<CommonResponse> GetVoter(int id);

        Task<CommonResponse> ApproveVoter(int id);
    }
}
