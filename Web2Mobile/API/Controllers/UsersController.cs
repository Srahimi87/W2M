using Domain;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Application.APIClients;

namespace API.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserAPIClient _userAPIClient;

        public string authToken { get; private set; }

        public UsersController(UserAPIClient userAPIClient)
        {
            _userAPIClient = userAPIClient;
        }
        [HttpGet("User-Information")]
        public async Task<IActionResult> GetUserInfor([FromHeader(Name = "c3-device-id")] string deviceID)
        {
            string userIdKey = "userId";

            IHeaderDictionary headers = Request.Headers;
            string authToken = string.Empty;

            if (headers.ContainsKey(userIdKey))
            {
                string userId = headers[userIdKey];
                //RW: can clean this up later but just getting it functional for now for the POC
                authToken = $"test.{Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new CustomJwt { PreferredUsername = userId })))}.test";
            }
            else
                return Forbid();

            var userDetails = await _userAPIClient.GetUserInfo(authToken, deviceID);
            return Ok(userDetails);
        }


    }

    public class CustomJwt
    {
        [JsonProperty("preferred_username")]
        public string PreferredUsername { get; set; }
    }



