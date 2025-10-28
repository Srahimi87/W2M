using Application.APIClients;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        public string authToken { get; private set; }

        private readonly WalletAPIClient _walletAPIClient;
		public PaymentsController(WalletAPIClient walletClient)
        {
            _walletAPIClient = walletClient;
        }


        [HttpPost("validate-payment")]
		public async Task<IActionResult> GetValidate([FromHeader(Name = "c3-device-id")] string deviceID, 
													 [FromBody]List<BasketItem> items)
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


            var valididateItem = await _walletAPIClient.PostValidateBasket(items, authToken, deviceID);
			return Ok(valididateItem);

		}

        [HttpPost("Complete-payment")]
        public async Task<IActionResult> GetPaymentComplete([FromHeader(Name = "c3-device-id")] string deviceID,
                                                     [FromBody] PaymentId ids)
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
            var completepayment = await _walletAPIClient.PostPaymentComplete(ids, authToken, deviceID);
            return Ok(completepayment);
        }

    }


}
