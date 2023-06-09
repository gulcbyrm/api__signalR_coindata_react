
using Bogus;
using GetFromApiAddDB.Currency.Clients;
using GetFromApiAddDB.Models;
using GetFromApiAddDB.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;
 
class Program
{
    private static CancellationTokenSource _cancellationTokenSource;

    private readonly CurrencyClient _client;
    public Program(CurrencyClient client)
    {
        _client = client;
    }




    public static async Task Main(string[] args)
    {
        string connectionString = "Server=DESKTOP-C13QDLJ\\MSSQLSERVER01;Database=CryptoGulcin;Trusted_Connection=True;encrypt=false;";

        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "ConnectionStrings:DefaultConnection", connectionString }
            })
            .Build();



        var serviceProvider = new ServiceCollection()
            .AddDbContext<CryptoGulcinContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")))
            .AddTransient<CurrencyClient>()
             .AddTransient<CurrencyService>()
                  .BuildServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            _cancellationTokenSource = new CancellationTokenSource();

            // Verilerin sürekli olarak alınması ve kaydedilmesi için bir döngü oluşturulur
            var task = Task.Run(async () =>
            {
                int i = 0;
                while (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    i++;
                    var services = scope.ServiceProvider;
                    var dbContext = services.GetRequiredService<CryptoGulcinContext>();
                    var apiService = services.GetRequiredService<CurrencyClient>();

                    var currencyService = scope.ServiceProvider.GetRequiredService<CurrencyService>();
                    // API'den veri al
                    var addedList = new List<Currency>();
                    var data = apiService.GetCurrencies().Result;
                    var currencies = currencyService.GetAll().Result;


                    /*FirstOrDefault: Koleksiyondaki belirli bir özelliğe göre ilk öğeyi döndürme: Eğer koleksiyon
                     * içerisinde koşulu sağlayan bir öğe varsa, bu öğeyi döndürür.
                     * Eğer hiçbir öğe koşulu sağlamazsa, varsayılan değeri döndürür.*/
                    foreach (var item in data)
                    {
                        var currency = currencies.FirstOrDefault(c => c.Name == item.Name);
                        if (currency == null)
                        {
                             

                        currency = new Currency
                            {
                                Name = item.Name,
                                Symbol = item.Symbol,
                                CurrencyActions = new List<CurrencyAction>
                        {
                            new CurrencyAction
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
                            currency.CurrencyActions.Add(new CurrencyAction
                            {
                                Date = item.UpdateTime,
                                Price = item.Price
                            });
                        }
                    }
                    //liste halinde ekleme
                    currencyService.BulkCreate(addedList).Wait();

                    Console.WriteLine(i + "." + "istekte bulunup db ye kaydediyor");

                    // Belirli bir süre beklemek için Task.Delay kullanılır
                    await Task.Delay(TimeSpan.FromSeconds(5));
                    Console.WriteLine("bekleniyor");
                }
            });
            // Kullanıcı tarafından bir tuşa basılmasını bekler
            Console.ReadKey();

            // Program sonlandırıldığında döngüyü durdurur
            _cancellationTokenSource.Cancel();

            // Döngünün tamamlanmasını bekler
            await task;



        }
    }




}





