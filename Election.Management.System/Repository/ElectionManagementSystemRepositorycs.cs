
using Election.Management.System.Interface;
using Election.Management.System.Models;
using Microsoft.Extensions.Logging;
using System.Net;


namespace Election.Management.Repository
{
    public class ElectionManagementSystemRepository : IElectionManagementSystemRepository
    {
        #region Initialization

        //private readonly ElectionDbContext _context;
        private readonly ILogger _logger;

        public ElectionManagementSystemRepository(ILoggerFactory loggerFactory
            //,ElectionDbContext context
            )
        {
            //_context = context;
            _logger = loggerFactory.CreateLogger<ElectionManagementSystemRepository>();
        }
        #endregion

        #region ApproveVoter
        /// <summary>
        /// Approve the Voter Details Based on Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CommonResponse> ApproveVoter(int id)
        {
            try
            {
                List<Voter> getData = await GetVotersData();
                Voter? voter = getData.Where(x => x.Id == id).FirstOrDefault();
                if (voter != null)
                {
                    voter.IsApproved= true;
                    getData.Add(voter);
                }
                return new CommonResponse { StatusCode = (int)HttpStatusCode.OK, Data = voter };
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error while Approving the Voter details");
                return new CommonResponse { StatusCode = (int)HttpStatusCode.InternalServerError, Data = null, Message = "Error while Approving the Voter Details" };
            }

        }
        #endregion

        #region GetMPSeats
        /// <summary>
        /// GetMP Seta Based On stateId 
        /// </summary>
        /// <param name="stateId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<CommonResponse> GetMPSeats(int stateId)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                List<State> getData = await ElectionManagementSystemRepository.GetStateDataWithMpSeats();
                int getNumberOfMpSeats = getData.Where(x => x.Id == stateId).Select(x => x.NumberOfMPSeats).FirstOrDefault();
                if (getNumberOfMpSeats > 0)
                {
                    response.Data = getNumberOfMpSeats;
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Record Fetched Successfully ";
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Record Not Found for StateId:{stateId}";
                    _logger.LogError("Record Not Found for StateId:" + stateId);
                }
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Could not update the record {ex.Message}");
                throw new Exception();
            }

        }
        #endregion

        #region GetParty
        /// <summary>
        /// Get Party
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CommonResponse> GetParty(int id)
        {
            try
            {
                List<Party> party1 = await GetPartyData();
                Party? result = party1.Where(x => x.Id == id).FirstOrDefault();
                CommonResponse response = new CommonResponse { StatusCode = (int)HttpStatusCode.OK, Data = result };
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while Getting Party details");
                return new CommonResponse { StatusCode = (int)HttpStatusCode.InternalServerError, Data = null, Message = "Error while Getting party details" };
            }
        }
        #endregion

        #region GetVoter
        /// <summary>
        /// GetVoter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CommonResponse> GetVoter(int id)
        {
            try
            {
                List<Voter> voter = await GetVotersData();
                Voter? result = voter.Where(x => x.Id == id).FirstOrDefault();
                CommonResponse response = new CommonResponse { StatusCode = (int)HttpStatusCode.OK, Data = result };
             return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error While Getting Data from collection");
                return new CommonResponse { StatusCode = (int)HttpStatusCode.InternalServerError, Data = null, Message = "Error While Getting Data by Id" };
            }

        }
        #endregion

        #region RegisterParty
        /// <summary>
        /// RegisterParty
        /// </summary>
        /// <param name="party"></param>
        /// <returns></returns>
        public async Task<CommonResponse> RegisterParty(Party party)
        {
            try
            {
                List<Party> party1 = await GetPartyData();
                party1.Add(party);
                return new CommonResponse { StatusCode = (int)HttpStatusCode.OK, Data=party1, Message = "Data Inserted" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error While adding new Party details");
                return new CommonResponse { StatusCode = (int)HttpStatusCode.InternalServerError, Message = "Data Insertion failed" };

            }
        }
        #endregion

        #region RegisterVoter

        /// <summary>
        /// RegisterVoter
        /// </summary>
        /// <param name="voter"></param>
        /// <returns></returns>
        public async Task<CommonResponse> RegisterVoter(Voter voter)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                List<Voter> votes = await GetVotersData();
                votes.Add(voter);
                return new CommonResponse { StatusCode = (int)HttpStatusCode.OK, Data= votes, Message = "Voter Detail Registered" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while Registering voter Details");
                return new CommonResponse { StatusCode = (int)HttpStatusCode.InternalServerError, Message = "Voter Detail Registered failed" };

            }
        }

        #endregion

        #region UpdateMPSeats
        /// <summary>
        /// UpdateMPSeats
        /// </summary>
        /// <param name="stateId"></param>
        /// <param name="newSeatCount"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<CommonResponse> UpdateMPSeats(int stateId, int newSeatCount)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                List<State> getData = await ElectionManagementSystemRepository.GetStateDataWithMpSeats();
                State? getBasedOnStateId = getData.FirstOrDefault(x => x.Id == stateId);
                if (getBasedOnStateId != null)
                {

                    getBasedOnStateId.NumberOfMPSeats = newSeatCount;
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Record Updated Successfully";
                    response.Data = getBasedOnStateId;
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Record Not Found for StateId:{stateId}";
                    _logger.LogError("error while Insert new MpSeat in collection");
                }
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Could not update the record {ex.Message}");
                throw new Exception();
            }
        }
        #endregion

        #region Candidatedata
        /// <summary>
        /// Candidatedata
        /// </summary>
        /// <returns></returns>
        public static List<Candidate> Candidatedata()
        {
            var candidates = new List<Candidate>
{
    new Candidate { Id = 1, Name = "John Doe", PartyId = 1, StateId = 1 },
    new Candidate { Id = 2, Name = "Jane Smith", PartyId = 2, StateId = 1 },
    new Candidate { Id = 3, Name = "Michael Johnson", PartyId = 1, StateId = 2 },
    new Candidate { Id = 4, Name = "Emily Brown", PartyId = 3, StateId = 2 },
    new Candidate { Id = 5, Name = "Robert Wilson", PartyId = 2, StateId = 3 },
    new Candidate { Id = 6, Name = "Emma Taylor", PartyId = 1, StateId = 3 },
    new Candidate { Id = 7, Name = "David Martinez", PartyId = 3, StateId = 4 },
    new Candidate { Id = 8, Name = "Olivia Anderson", PartyId = 2, StateId = 4 },
    new Candidate { Id = 9, Name = "Daniel Hernandez", PartyId = 1, StateId = 5 },
    new Candidate { Id = 10, Name = "Sophia Lee", PartyId = 3, StateId = 5 },
    new Candidate { Id = 11, Name = "Matthew Gonzalez", PartyId = 2, StateId = 6 },
    new Candidate { Id = 12, Name = "Isabella Perez", PartyId = 1, StateId = 6 },
    new Candidate { Id = 13, Name = "James Wilson", PartyId = 3, StateId = 7 },
    new Candidate { Id = 14, Name = "Charlotte Moore", PartyId = 2, StateId = 7 },
    new Candidate { Id = 15, Name = "Benjamin Taylor", PartyId = 1, StateId = 8 },
    new Candidate { Id = 16, Name = "Amelia Brown", PartyId = 3, StateId = 8 },
    new Candidate { Id = 17, Name = "Alexander Martinez", PartyId = 2, StateId = 9 },
    new Candidate { Id = 18, Name = "Evelyn Garcia", PartyId = 1, StateId = 9 },
    new Candidate { Id = 19, Name = "William Rivera", PartyId = 3, StateId = 10 },
    new Candidate { Id = 20, Name = "Mia Hernandez", PartyId = 2, StateId = 10 }
};
            return candidates;

        }
        #endregion

        #region GetPartyData
        /// <summary>
        /// GetPartyData
        /// </summary>
        /// <returns></returns>
        public async static Task<List<Party>> GetPartyData()
        {
            var parties = new List<Party>
{
    new Party { Id = 1, Name = "Democratic Party", Symbol = "" },
    new Party { Id = 2, Name = "Republican Party", Symbol = "" },
    new Party { Id = 3, Name = "Green Party", Symbol = "" },
    new Party { Id = 4, Name = "Libertarian Party", Symbol = "" },
    new Party { Id = 5, Name = "Constitution Party", Symbol = "" },
    new Party { Id = 6, Name = "Socialist Party", Symbol = "" },
    new Party { Id = 7, Name = "Independent Party", Symbol = "" },
    new Party { Id = 8, Name = "Progressive Party", Symbol = "" },
    new Party { Id = 9, Name = "Conservative Party", Symbol = "" },
    new Party { Id = 10, Name = "Labor Party", Symbol = "" },
    new Party { Id = 11, Name = "Nationalist Party", Symbol = "" },
    new Party { Id = 12, Name = "Social Democratic Party", Symbol = "" },
    new Party { Id = 13, Name = "Pirate Party", Symbol = "" },
    new Party { Id = 14, Name = "Anarchist Party", Symbol = "" },
    new Party { Id = 15, Name = "Feminist Party", Symbol = "" },
    new Party { Id = 16, Name = "Animal Rights Party", Symbol = "" },
    new Party { Id = 17, Name = "Environmental Party", Symbol = "" },
    new Party { Id = 18, Name = "Christian Democratic Party", Symbol = "" },
    new Party { Id = 19, Name = "Islamic Party", Symbol = "" },
    new Party { Id = 20, Name = "Hindu Nationalist Party", Symbol = "" }
};
            return parties;
        }
        #endregion

        #region GetStateDataWithMpSeats
        /// <summary>
        /// GetStateDataWithMpSeats
        /// </summary>
        /// <returns></returns>
        public static async Task<List<State>> GetStateDataWithMpSeats()
        {
            var states = new List<State>
{
    new State { Id = 1, Name = "Alabama", NumberOfMPSeats = 7 },
    new State { Id = 2, Name = "Alaska", NumberOfMPSeats = 1 },
    new State { Id = 3, Name = "Arizona", NumberOfMPSeats = 9 },
    new State { Id = 4, Name = "Arkansas", NumberOfMPSeats = 4 },
    new State { Id = 5, Name = "California", NumberOfMPSeats = 53 },
    new State { Id = 6, Name = "Colorado", NumberOfMPSeats = 7 },
    new State { Id = 7, Name = "Connecticut", NumberOfMPSeats = 5 },
    new State { Id = 8, Name = "Delaware", NumberOfMPSeats = 1 },
    new State { Id = 9, Name = "Florida", NumberOfMPSeats = 27 },
    new State { Id = 10, Name = "Georgia", NumberOfMPSeats = 14 },
    new State { Id = 11, Name = "Hawaii", NumberOfMPSeats = 2 },
    new State { Id = 12, Name = "Idaho", NumberOfMPSeats = 2 },
    new State { Id = 13, Name = "Illinois", NumberOfMPSeats = 18 },
    new State { Id = 14, Name = "Indiana", NumberOfMPSeats = 9 },
    new State { Id = 15, Name = "Iowa", NumberOfMPSeats = 4 },
    new State { Id = 16, Name = "Kansas", NumberOfMPSeats = 4 },
    new State { Id = 17, Name = "Kentucky", NumberOfMPSeats = 6 },
    new State { Id = 18, Name = "Louisiana", NumberOfMPSeats = 6 },
    new State { Id = 19, Name = "Maine", NumberOfMPSeats = 2 },
    new State { Id = 20, Name = "Maryland", NumberOfMPSeats = 8 }
};
            return states;
        }
        #endregion

        #region GetVotersData
        /// <summary>
        /// GetVotersData
        /// </summary>
        /// <returns></returns>
        public async static Task<List<Voter>> GetVotersData()
        {
            var voters = new List<Voter>
{
    new Voter { Id = 1, VoterId = "VOT001", Name = "John Doe", Address = "123 Main St", Photo = null, IsApproved = true },
    new Voter { Id = 2, VoterId = "VOT002", Name = "Jane Smith", Address = "456 Elm St", Photo = null, IsApproved = true },
    new Voter { Id = 3, VoterId = "VOT003", Name = "Michael Johnson", Address = "789 Oak St", Photo = null, IsApproved = false },
    new Voter { Id = 4, VoterId = "VOT004", Name = "Emily Brown", Address = "101 Pine St", Photo = null, IsApproved = true },
    new Voter { Id = 5, VoterId = "VOT005", Name = "Robert Wilson", Address = "222 Maple St", Photo = null, IsApproved = false },
    new Voter { Id = 6, VoterId = "VOT006", Name = "Emma Taylor", Address = "333 Cedar St", Photo = null, IsApproved = true },
    new Voter { Id = 7, VoterId = "VOT007", Name = "David Martinez", Address = "444 Birch St", Photo = null, IsApproved = true },
    new Voter { Id = 8, VoterId = "VOT008", Name = "Olivia Anderson", Address = "555 Walnut St", Photo = null, IsApproved = false },
    new Voter { Id = 9, VoterId = "VOT009", Name = "Daniel Hernandez", Address = "666 Pineapple St", Photo = null, IsApproved = true },
    new Voter { Id = 10, VoterId = "VOT010", Name = "Sophia Lee", Address = "777 Lemon St", Photo = null, IsApproved = true },
    new Voter { Id = 11, VoterId = "VOT011", Name = "Matthew Gonzalez", Address = "888 Banana St", Photo = null, IsApproved = false },
    new Voter { Id = 12, VoterId = "VOT012", Name = "Isabella Perez", Address = "999 Orange St", Photo = null, IsApproved = true },
    new Voter { Id = 13, VoterId = "VOT013", Name = "James Wilson", Address = "123 Grape St", Photo = null, IsApproved = true },
    new Voter { Id = 14, VoterId = "VOT014", Name = "Charlotte Moore", Address = "456 Cherry St", Photo = null, IsApproved = false },
    new Voter { Id = 15, VoterId = "VOT015", Name = "Benjamin Taylor", Address = "789 Kiwi St", Photo = null, IsApproved = true },
    new Voter { Id = 16, VoterId = "VOT016", Name = "Amelia Brown", Address = "101 Peach St", Photo = null, IsApproved = true },
    new Voter { Id = 17, VoterId = "VOT017", Name = "Alexander Martinez", Address = "222 Plum St", Photo = null, IsApproved = false },
    new Voter { Id = 18, VoterId = "VOT018", Name = "Evelyn Garcia", Address = "333 Apricot St", Photo = null, IsApproved = true },
    new Voter { Id = 19, VoterId = "VOT019", Name = "William Rivera", Address = "444 Mango St", Photo = null, IsApproved = true },
    new Voter { Id = 20, VoterId = "VOT020", Name = "Mia Hernandez", Address = "555 Papaya St", Photo = null, IsApproved = false }
};
            return voters;
        }
        #endregion
    }


}





