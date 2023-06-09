using Bogus;
using GetFromApiAddDB.Models;

namespace Api.Services
{
    public class CoinService
    {
        private Timer timer;
        private readonly Faker faker;
        public List<CurrencyResponse> coins;

        public CoinService()
        {
            faker = new Faker();

            if (coins == null)
            {
                coins = new List<CurrencyResponse>
                {
                    new CurrencyResponse { Name = "Bitcoin", Price = faker.Finance.Amount(), Symbol = "BTC", UpdateTime = DateTime.Now},
                    new CurrencyResponse { Name = "Ethereum",Price = faker.Finance.Amount(), Symbol = "ETH", UpdateTime = DateTime.Now },
                    new CurrencyResponse { Name = "Litecoin", Price = faker.Finance.Amount(), Symbol = "LTC", UpdateTime = DateTime.Now }
                };
            }


            // Timer'ı başlat
            timer = new Timer(UpdateCoins, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        }

        private void UpdateCoins(object state)
        {
            foreach (var item in coins)
            {
                item.Price = faker.Finance.Amount();
               item.UpdateTime = DateTime.Now;
            }
        }
    }
}
