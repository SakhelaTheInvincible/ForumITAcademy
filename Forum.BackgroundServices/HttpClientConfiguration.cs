using Microsoft.Extensions.DependencyInjection;

namespace BackGroundServices
{
    public class HttpClientConfiguration
    {
        public static void ConfigureHttpClient(IServiceCollection services)
        {
            services.AddSingleton<HttpClient>(provider =>
            {
                var httpClient = new HttpClient();
                return httpClient;
            });
        }
    }
}
