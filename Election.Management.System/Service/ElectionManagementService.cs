

using Election.Management.Repository;
using Election.Management.System.Interface;
using Election.Management.System.Models;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Election.Management.System.Service
{
    public class ElectionManagementSystemService: IElectionManagementSystemService
    {
        private readonly IElectionManagementSystemRepository _electionManagementRepository;
        private readonly ILogger _logger;
        public ElectionManagementSystemService(ILoggerFactory loggerFactory, IElectionManagementSystemRepository electionManagementRepository) 
        {
            _electionManagementRepository = electionManagementRepository;
            _logger = loggerFactory.CreateLogger<ElectionManagementSystemService>();
        }

        public async Task<CommonResponse> ApproveVoter(int id)
        {
            try
            {
               return await _electionManagementRepository.ApproveVoter(id);
            }
            catch
            {
                throw;
            }
        }

        public async Task<CommonResponse> GetMPSeats(int stateId)
        {
            CommonResponse response = new CommonResponse();
            try
            {   
                response = await _electionManagementRepository.GetMPSeats(stateId);
            }
            catch
            {
                throw;
            }
            return response;

        }

        public async Task<CommonResponse> GetParty(int id)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                response = await _electionManagementRepository.GetParty(id);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public async Task<CommonResponse> GetVoter(int id)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                response = await _electionManagementRepository.GetVoter(id);
            }
            catch
            {
                throw;
            }
            return response;
        }

        public async Task<CommonResponse> RegisterParty(Party party)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = await _electionManagementRepository.RegisterParty(party);
                return commonResponse;
            }
            catch
            {
                throw;
            }

        }

        public async Task<CommonResponse> RegisterVoter(Voter voter)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse =  await _electionManagementRepository.RegisterVoter(voter);
            }
            catch
            {
                throw;
            }
            return commonResponse;
        }

        public  async Task<CommonResponse> UpdateMPSeats(int stateId, int newSeatCount)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                 response = await _electionManagementRepository.UpdateMPSeats(stateId, newSeatCount);
                if (response.StatusCode == (int)HttpStatusCode.NoContent)
                {
                    _logger.LogError("Record Updated Successfully");
                }
                else
                {
                    _logger.LogError("Error While Updating the MP seats");
                }
               return response;
            }
            catch
            {
                throw;
            }
        }
    }
}
