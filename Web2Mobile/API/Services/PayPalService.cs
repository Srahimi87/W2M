/*using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace API.Services
{
    public class PayPalService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PayPalService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private async Task<string> GetAccessTokenAsync()
        {
            var client = _httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.sandbox.paypal.com/v1/oauth2/token");
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes("YourClientId:YourClientSecret")));
            request.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);

            return jsonResponse.access_token;
        }

        public async Task<string> CreatePaymentAsync(decimal amount)
        {
            var accessToken = await GetAccessTokenAsync();
            var client = _httpClientFactory.CreateClient();

            var paymentRequest = new
            {
                intent = "sale",
                payer = new { payment_method = "paypal" },
                transactions = new[]
                {
                new {
                    amount = new { total = amount.ToString("F2"), currency = "USD" },
                    description = "Payment description"
                }
            },
                redirect_urls = new
                {
                    return_url = "https://your-site.com/success",
                    cancel_url = "https://your-site.com/cancel"
                }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.sandbox.paypal.com/v1/payments/payment");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            request.Content = new StringContent(JsonConvert.SerializeObject(paymentRequest), Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            return responseData;  // Returns payment details including approval URL
        }

        public async Task<string> CapturePaymentAsync(string paymentId, string payerId)
        {
            var accessToken = await GetAccessTokenAsync();
            var client = _httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Post, $"https://api.sandbox.paypal.com/v1/payments/payment/{paymentId}/execute");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            request.Content = new StringContent(JsonConvert.SerializeObject(new { payer_id = payerId }), Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            return responseData;  // Returns the final payment details
        }
    }
}
 */