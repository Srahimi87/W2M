using Newtonsoft.Json;

namespace Application.DTOs
{

    public class UserInformation
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("hasBypassPaymentAuthorisation")]
        public bool HasBypassPaymentAuthorisation { get; set; }

        [JsonProperty("isChild")]
        public bool IsChild { get; set; }

        [JsonProperty("passes")]
        public List<string> Passes { get; set; }


    }

}