using ImgbbApi.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ImgbbApi.Registration
{
    public static class ImgbbClientRegistration
    {
        public static IServiceCollection AddImgbbApiClient(this IServiceCollection services, string url, string secret)
        {
            services.Configure<ImgbbApiClientConfiguration>(config =>
            {
                config.ImgbbApiEndPoint = url;
                config.ImgbbApiSecret = secret;
            });

            services.AddSingleton<IImgbbApiClient, ImgbbApiClient>();

            return services;
        }
    }
}
