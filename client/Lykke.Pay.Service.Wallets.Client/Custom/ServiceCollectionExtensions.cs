using Lykke.Pay.Service.Wallets.Client.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Lykke.Pay.Service.Wallets.Client.Custom
{
    public static class ServiceCollectionExtensions
    {
        public static void UseAssetsClient(this IServiceCollection services, AssetServiceSettings settings)
        {
            services.AddSingleton<IAssetsservice>(x => new Assetsservice(settings.BaseUri, settings.Handlers));
            services.AddTransient<ICachedAssetsService, CachedAssetsService>();
            services.AddSingleton<IDictionaryCache<AssetResponseModel>>(x => new DictionaryCache<AssetResponseModel>(new DateTimeProvider(), settings.AssetsCacheExpirationPeriod));
            services.AddSingleton<IDictionaryCache<AssetPairResponseModel>>(x => new DictionaryCache<AssetPairResponseModel>(new DateTimeProvider(), settings.AssetPairsCacheExpirationPeriod));
        }
    }
}