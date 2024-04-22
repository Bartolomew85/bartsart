using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WikiArtParser.Core.Interfaces;

namespace WikiArtParser.Core.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWikiArtParserCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();

            services.AddTransient<IWikiArtParserMessageHandler, WikiArtParserMessageHandler>();
            services.AddTransient<IWikiArtParser, WikiArtParser>();
            services.AddTransient<IArtRepository, ArtRepository>();

            return services;
        }
    }
}
