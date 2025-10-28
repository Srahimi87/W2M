using Application.DTOs;
using Microsoft.Extensions.Options;

namespace Application.APIClients;

public class WalletAPIClient : CoreThreeAPIClientBase
{
    private readonly APIConfig _config;

    private readonly CustomJwt _jwt;



    public WalletAPIClient(HttpClient client, IOptions<APIConfig> config, CustomJwt jwt) : base(client)
    {
        this._config = config.Value;
        this._jwt = jwt;
        _client.DefaultRequestHeaders.Add("x-api-key", _config.APIKey);
        Authentication = new System.Net.Http.Headers.AuthenticationHeaderValue("x-api-key", _config.APIKey);
        _client.DefaultRequestHeaders.Add("c3-auth-token", _jwt.PreferredUsername);
        Authentication = new System.Net.Http.Headers.AuthenticationHeaderValue("c3-auth-token", _jwt.PreferredUsername);

    }

    public async Task<BasketValidationResultDto> PostValidateBasket(List<BasketItem> itemsToValidate,string authToken, string deviceID) =>
	 await Post<List<BasketItem>, BasketValidationResultDto>($"{_config.BaseURL}/wallet/api/vendor/toxZgyNaGeW/Checkout/validate", itemsToValidate , authToken, deviceID);

	public async Task<BasketPaymentResultDto> PostPaymentComplete(PaymentId itemsToPayment, string authToken, string deviceID) =>
	 await Post<PaymentId, BasketPaymentResultDto>($"{_config.BaseURL}/wallet/api/vendor/toxZgyNaGeW/Checkout/completewithbypass", itemsToPayment, authToken, deviceID);

}
