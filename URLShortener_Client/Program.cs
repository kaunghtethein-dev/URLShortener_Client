using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using URLShortener_Client.Configuration;
using URLShortener_Client.Extensions;
using URLShortener_Client.Services;

namespace URLShortener_Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            var baseAddress = builder.HostEnvironment.BaseAddress;

            string configFile = builder.HostEnvironment.IsDevelopment() ? "appsettings.Development.json" : "appsettings.json";
            var response = await ConfigLoader.LoadConfigFileAsync($"{baseAddress}{configFile}");
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
         
            var config = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build();

            // Bind to AppSettings class
            var appSettings = config.Get<AppSettings>()?? new AppSettings();
            builder.Services.AddSingleton(appSettings);

            // Configure HttpClient with API base URL from settings
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(appSettings.API_BaseURL)
            });
            builder.Services.AddSingleton<NotifierService>();
            builder.Services.AddScoped<ApiClient>();
            builder.Services.AddScoped<LocalStorageService>();
            builder.Services.AddScoped<VariablesService>();
            builder.Services.AddScoped<CommonFunctionsService>();
            var host = builder.Build();

            // Load AuthInfo from LocalStorage at startup 
            var comFn = host.Services.GetRequiredService<CommonFunctionsService>();
            await comFn.LoadAuthInfoAsync();

            await host.RunAsync();
        }
    }
}
