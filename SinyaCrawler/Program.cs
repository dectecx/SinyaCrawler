using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SinyaCrawler.Service;
using SinyaCrawler.Service.Interface;
using SinyaCrawler.Service.Model.LineNotify;

namespace Program
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsetting.json");
            IConfiguration configuration = builder.Build();
            // 1. 建立依賴注入的容器
            var serviceCollection = new ServiceCollection();
            // 2. 註冊服務
            ConfigureServices(serviceCollection, configuration);
            // 3. 建立依賴服務提供者
            var serviceProvider = serviceCollection.BuildServiceProvider();
            // 4. 執行主服務
            var crawler = serviceProvider.GetRequiredService<CrawlerService>();
            Task task = Task.Factory.StartNew(async () => await crawler.RunAsync());
            task.Wait();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            // DI Config
            services.Configure<LineNotifyOption>(configuration.GetSection(nameof(LineNotifyOption)));
            // DI Common
            services.AddTransient<HttpClient>();
            // DI Service
            services.AddTransient<CrawlerService>();
            services.AddTransient<SinyaService>();
            services.AddTransient<LineNotifyService>();
            if (configuration.GetSection("HttpOption").GetValue<string>("Mode") == "Debug")
            {
                //services.AddTransient<IHttpService, MockHttpService>();
            }
            else
            {
                services.AddTransient<IHttpService, HttpService>();
            }
            // DI Process
            // DI Dao
            return;
        }
    }
}