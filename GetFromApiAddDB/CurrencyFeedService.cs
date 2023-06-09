


using GetFromApiAddDB.Currency.Clients;
using GetFromApiAddDB.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GetFromApiAddDB.Currency.Services
{
    public class CurrencyFeedService 
    {
        private readonly CurrencyClient _client;
         private readonly IServiceProvider _services;
 
        public CurrencyFeedService(CurrencyClient client, IServiceProvider services)
        {
            _client = client;
            _services = services;
        }

         

       
        private void DoWork(object? state)
        {
            using (var scope = _services.CreateScope())
            {
                var currencyService = scope.ServiceProvider.GetRequiredService<CurrencyService>();

                var addedList = new List<GetFromApiAddDB.Models.Currency>();
                var data = _client.GetCurrencies().Result;
                var currencies = currencyService.GetAll().Result;


                /*FirstOrDefault: Koleksiyondaki belirli bir özelliğe göre ilk öğeyi döndürme: Eğer koleksiyon
                 * içerisinde koşulu sağlayan bir öğe varsa, bu öğeyi döndürür.
                 * Eğer hiçbir öğe koşulu sağlamazsa, varsayılan değeri döndürür.*/
                foreach (var item in data)
                {
                    var currency = currencies.FirstOrDefault(c => c.Name == item.Name);
                    if (currency == null)
                    {
                        currency = new GetFromApiAddDB.Models.Currency
                        {
                            Name = item.Name,
                            Symbol = item.Symbol,
                            CurrencyActions = new List<GetFromApiAddDB.Models.CurrencyAction>
                        {
                            new GetFromApiAddDB.Models.CurrencyAction
                            {
                                Date = item.UpdateTime,
                                Price = item.Price
                            }
                        }
                        };
                        addedList.Add(currency);
                    }
                    else
                    {
                        currency.CurrencyActions.Add(new GetFromApiAddDB.Models.CurrencyAction
                        {
                            Date = item.UpdateTime,
                            Price = item.Price
                        });
                    }
                }
                //liste halinde ekleme
                currencyService.BulkCreate(addedList).Wait();
            }
        }

        
    }
}
