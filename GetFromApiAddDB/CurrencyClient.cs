using Bogus;
using GetFromApiAddDB.Models;
using Newtonsoft.Json;
using System.Drawing;
using System.Net.Http.Headers;

namespace GetFromApiAddDB.Currency.Clients
{
    public class CurrencyClient
    {    // private const string GetCurrenciesUrl = "/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=100&page=1&sparkline=false";

         
        private const string GetCurrenciesUrl = "/api/coin";

        public async Task<List<CurrencyResponse>> GetCurrencies()
        {
           
                List<CurrencyResponse> result = null;
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://localhost:5000");
                var serializerOptions = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Other");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.Timeout = TimeSpan.FromSeconds(25);
                try
                {
                    var response = await httpClient.GetAsync(GetCurrenciesUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<List<CurrencyResponse>>(content, serializerOptions);


                    }
                }
                catch (Exception ex)
                {
                }
               
                return result;

                  
                
            }
        }
    }
}