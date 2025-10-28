using Application.DTOs;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.APIClients
{
    public class UserAPIClient: CoreThreeAPIClientBase
    {
        private readonly APIConfig _config;

        public UserAPIClient(HttpClient client, IOptions<APIConfig> config) : base(client)
        {
            this._config = config.Value;
            _client.DefaultRequestHeaders.Add("x-api-key", _config.APIKey);
            Authentication = new System.Net.Http.Headers.AuthenticationHeaderValue("x-api-key", _config.APIKey);
        }

        public async Task<UserInformation> GetUserInfo(string authToken, string deviceID)
        {
            var results = await Get<UserInformation>($"{_config.BaseURL}/user/api/vendor/toxZgyNaGeW/userdetails", authToken, deviceID);
            return results;
        }

    }
}
