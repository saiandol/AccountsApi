using System;using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using AccountsApi.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AccountsApi.Services
{
    public class AccountAddressService : IAccountAddressService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountAddressService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException();
        }

        public async Task<string> GetAddress()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://randomuser.me/api/?nat=gb");

            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode) throw new Exception("Error while getting the account address");

            var readAsStringAsync = await response.Content.ReadAsStringAsync();

            // var responseContent = ((System.Net.Http.ObjectContent) response.Content).Value.ToString();

            var fullAddressResponse = JObject.Parse(readAsStringAsync);

            var postcode = fullAddressResponse.SelectToken("$.results[0].location.postcode").ToString();
            var city = fullAddressResponse.SelectToken("$.results[0].location.city").ToString();
            return await Task.FromResult($"Postcode: {postcode}, City: {city}");

        }
    }
}
