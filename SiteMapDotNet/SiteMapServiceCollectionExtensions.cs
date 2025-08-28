using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace SiteMapDotNet
{
    public static class SiteMapServiceCollectionExtensions
    {
        public static IServiceCollection AddSiteMap(this IServiceCollection services, string siteMapFileName = "Web.sitemap")
        {
            services.AddSingleton(sp =>
            {
                var env = sp.GetRequiredService<IHostEnvironment>();
                var path = Path.Combine(env.ContentRootPath, siteMapFileName);

                // your hidden static method call
                return SiteMapHelper.ReadWebSiteMap(path);
            });

            return services;
        }
        public static IServiceCollection AddSiteMapFromPath(this IServiceCollection services, string siteMapFilePath)
        {
            services.AddSingleton(sp =>
            {
                // your hidden static method call
                return SiteMapHelper.ReadWebSiteMap(siteMapFilePath);
            });
            return services;
        }
    }
}