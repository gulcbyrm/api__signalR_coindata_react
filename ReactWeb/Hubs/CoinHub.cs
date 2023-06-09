using GetFromApiAddDB.Currency.Clients;
using GetFromApiAddDB.Models;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ReactCoin.Hubs
{
    [HubName("CoinHub")]
    public class CoinHub : Hub
    {
      
        private readonly CurrencyClient _currencyClient;

        public CoinHub(CurrencyClient currencyClient)
        {
            _currencyClient = currencyClient;
        }
       private List<CurrencyResponse> _lastCoinData;

        public async Task SendCoinData()
        {

            int i = 0;
            while (true)
            {
                i++;
 
                var coinData = await _currencyClient.GetCurrencies();
                _lastCoinData = coinData;
                Console.WriteLine(i+" Coin data received:"); // Consola yazdırma
                if (_lastCoinData != null)
                {
                    foreach (var coin in _lastCoinData)
                    {
                        Console.WriteLine($"{coin.Name}: {coin.Price}"); // Consola yazdırma
                    }
                    //guncel veriyi gonder
                    await Clients.All.SendAsync("ReceiveCoinData", _lastCoinData);
                }
                else
                {
                    // İkinci istek null döndüğünde yapılacak işlemleri burada ele alabilirsiniz.
                    Console.WriteLine("Coin data is null."); // Consola yazdırma veya gerekli hata işleme mekanizmasını uygulayabilirsiniz.
                }
                // Belirli bir süre beklemek için Task.Delay kullanın
                await Task.Delay(TimeSpan.FromSeconds(5)); //   saniye bekle
            }
        }
    }
}

         