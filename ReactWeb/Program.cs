using GetFromApiAddDB.Currency.Clients;
 
using ReactCoin.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR(); // SignalR'ý servislere ekleyin
builder.Services.AddSingleton<CurrencyClient>();//return api data
builder.Services.AddControllers().AddControllersAsServices();

//cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder
             .WithOrigins("https://localhost:7004/", "http://localhost:5177", "http://localhost:44476")
            .AllowAnyMethod()
            .AllowAnyHeader() 
            .SetIsOriginAllowed((host) => true) // Ýzin verilen tüm kökleri kabul etmek için
            .AllowCredentials();
    });
});
  
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("CorsPolicy");

app.UseAuthorization();
 

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");
app.MapHub<CoinHub>("/CoinHub");
//app.MapFallbackToFile("index.html");

app.Run();

 
