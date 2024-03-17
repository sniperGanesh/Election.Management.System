using System.Net;
using System.Text;
using Election.Management.System.Interface;
using Election.Management.System.Model;
using Election.Management.System.Model.Constant;
using Election.Management.System.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Election.Management.System.Function
{
    public class ElectionManagement
    {
        private readonly ILogger _logger;
        private readonly IElectionManagementSystemService _systemService;

        public ElectionManagement(ILoggerFactory loggerFactory, IElectionManagementSystemService systemService)
        {
            _logger = loggerFactory.CreateLogger<ElectionManagement>();
            _systemService = systemService;
        }

        #region UpdateMPSeats
        /// <summary>
        ///  Update MP seat Based on StatId
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [Function("UpdateMPSeats")]
        public async Task<HttpResponseData> UpdateMPSeats([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "get/election/UpdateMPSeats")] HttpRequestData req)
        {
            try
            {
                HttpResponseData responseData = req.CreateResponse();
                //log the incoming request
                _logger.LogInformation(StringConstant.IncomingRequest);

                string requestBody;
                using (StreamReader reader = new StreamReader(req.Body, Encoding.UTF8))
                {
                    requestBody = await reader.ReadToEndAsync();
                }
                UpdateStateSeats? deatils = JsonConvert.DeserializeObject<UpdateStateSeats>(requestBody);
                // update the MP seats
                CommonResponse result = await _systemService.UpdateMPSeats(deatils.StateId, deatils.NoOfSeats);
                responseData.Headers.Add("content-Type", "text/json; charset=utf-8");
                JsonSerializerSettings serializerSettings = new()
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    NullValueHandling = NullValueHandling.Ignore,
                };
                await responseData.WriteStringAsync(JsonConvert.SerializeObject(result, serializerSettings));
                //responseData.StatusCode= (HttpStatusCode)result.StatusCode;
                return responseData;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error While Updating MPSeats");
                throw;
            }
        }
        #endregion

        #region GetMPSeats
        /// <summary>
        /// GetMPSeats
        /// </summary>
        /// <param name="req"></param>
        /// <param name="stateId"></param>
        /// <returns></returns>
        [Function("GetMPSeats")]
        public async Task<HttpResponseData> GetMPSeats([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "get/election/{stateId}")] HttpRequestData req, string stateId)
        {
            try
            {
                HttpResponseData responseData = req.CreateResponse();
                if (!int.TryParse(stateId, out int state))
                {
                    _logger.LogError("Invalid state Id Formate");
                    throw new Exception();
                }
                _logger.LogInformation("C# HTTP trigger function processed a request.");
                var result= await _systemService.GetMPSeats(state);
                responseData.Headers.Add("content-Type", "text/json; charset=utf-8");
                JsonSerializerSettings serializerSettings = new()
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    NullValueHandling = NullValueHandling.Ignore,
                };
                await responseData.WriteStringAsync(JsonConvert.SerializeObject(result,serializerSettings));
                //responseData.StatusCode= (HttpStatusCode)result.StatusCode;
                return responseData;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error While Getting MPSeats from collection");
                throw;

            }
        }
        #endregion

        #region RegisterParty
        /// <summary>
        /// RegisterParty
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [Function("RegisterParty")]
        public async Task<HttpResponseData> RegisterParty([HttpTrigger(AuthorizationLevel.Function, "post", Route = "election/RegisterParty")] HttpRequestData req)
        {
            try
            {
                HttpResponseData responseData = req.CreateResponse();
                _logger.LogInformation("C# HTTP trigger function processed a request.");
                string requestBody;
                using (StreamReader reader = new StreamReader(req.Body, Encoding.UTF8))
                {
                    requestBody = reader.ReadToEnd();
                }
                Party? newParty = JsonConvert.DeserializeObject<Party>(requestBody);
                var result= await _systemService.RegisterParty(newParty);
                responseData.Headers.Add("content-Type", "text/json; charset=utf-8");
                JsonSerializerSettings serializerSettings = new()
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    NullValueHandling = NullValueHandling.Ignore,
                };
                await responseData.WriteStringAsync(JsonConvert.SerializeObject(result, serializerSettings));
                //responseData.StatusCode= (HttpStatusCode)result.StatusCode;
                return responseData;
            }
            catch
            {
                _logger.LogError("Error While Updating MPSeats");
                throw;
            }
        }
        #endregion

        #region GetParty
        /// <summary>
        /// GetParty
        /// </summary>
        /// <param name="req"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Function("GetParty")]
        public async Task<HttpResponseData> GetParty([HttpTrigger(AuthorizationLevel.Function, "get", Route = "getParty/election/{id}")] HttpRequestData req, string id)
        {
            HttpResponseData responseData = req.CreateResponse();
            try
            {
                if (!int.TryParse(id, out int PartyId))
                {
                    _logger.LogError("Party id is invalid");
                }
                _logger.LogInformation("C# HTTP trigger function processed a request.");
                var result= await _systemService.GetParty(PartyId);
                responseData.Headers.Add("content-Type", "text/json; charset=utf-8");
                JsonSerializerSettings serializerSettings = new()
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    NullValueHandling = NullValueHandling.Ignore,
                };
                await responseData.WriteStringAsync(JsonConvert.SerializeObject(result, serializerSettings));
                //responseData.StatusCode= (HttpStatusCode)result.StatusCode;
                return responseData;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error While Updating MPSeats");
                throw;
            }
        }

        #endregion

        #region RegisterVoter
        /// <summary>
        /// RegisterVoter
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [Function("RegisterVoter")]
        public async Task<HttpResponseData> RegisterVoter([HttpTrigger(AuthorizationLevel.Function,  "post",Route ="election/RegisterVoters")] HttpRequestData req)
        {
            HttpResponseData responseData = req.CreateResponse();
            try
            {
                _logger.LogInformation("C# HTTP trigger function processed a request.");
                string requestBody;
                using (StreamReader reader = new StreamReader(req.Body, Encoding.UTF8))
                {
                    requestBody = reader.ReadToEnd();
                }
                Voter? newVoters = JsonConvert.DeserializeObject<Voter>(requestBody);
              var result = await _systemService.RegisterVoter(newVoters);
                responseData.Headers.Add("content-Type", "text/json; charset=utf-8");
                JsonSerializerSettings serializerSettings = new()
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    NullValueHandling = NullValueHandling.Ignore,
                };
                await responseData.WriteStringAsync(JsonConvert.SerializeObject(result, serializerSettings));
                //responseData.StatusCode= (HttpStatusCode)result.StatusCode;
                return responseData;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error While Updating MPSeats");
                throw;
            }
        }
        #endregion

        #region GetVoter
        /// <summary>
        /// GetVoter
        /// </summary>
        /// <param name="req"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Function("GetVoter")]
        public async Task<HttpResponseData> GetVoter([HttpTrigger(AuthorizationLevel.Function, "get", Route ="election/{id}/getvoterdetails")] HttpRequestData req, string id)
        {
            HttpResponseData responseData = req.CreateResponse();
            try
            {
                if(!int.TryParse(id, out int ids))
                {
                    _logger.LogError("invalid id formate");
                }
                _logger.LogInformation("C# HTTP trigger function processed a request.");
                var result = await _systemService.GetVoter(ids);
                responseData.Headers.Add("content-Type", "text/json; charset=utf-8");
                JsonSerializerSettings serializerSettings = new()
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    NullValueHandling = NullValueHandling.Ignore,
                };
                await responseData.WriteStringAsync(JsonConvert.SerializeObject(result, serializerSettings));
                //responseData.StatusCode= (HttpStatusCode)result.StatusCode;
                return responseData;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error While Updating MPSeats");
                throw;
            }
        }
        #endregion

        #region ApproveVoter
        /// <summary>
        /// ApproveVoter
        /// </summary>
        /// <param name="req"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Function("ApproveVoter")]
        public async Task<HttpResponseData> ApproveVoter([HttpTrigger(AuthorizationLevel.Function, "get", Route ="updateVoter/election/{id}")] HttpRequestData req, string id)
        {
            HttpResponseData responseData = req.CreateResponse();
            try
            {
                if (!int.TryParse(id, out int Id))
                {
                    _logger.LogError("invalid id formate");
                }
                _logger.LogInformation("C# HTTP trigger function processed a request.");
                var result = await _systemService.ApproveVoter(Id);
                responseData.Headers.Add("content-Type", "text/json; charset=utf-8");
                JsonSerializerSettings serializerSettings = new()
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    NullValueHandling = NullValueHandling.Ignore,
                };
                await responseData.WriteStringAsync(JsonConvert.SerializeObject(result, serializerSettings));
                //responseData.StatusCode= (HttpStatusCode)result.StatusCode;
                return responseData;
            }
            catch
            {
                _logger.LogError("error while Approving the Voter");
                throw;
            }
        }

        #endregion

    }
}
