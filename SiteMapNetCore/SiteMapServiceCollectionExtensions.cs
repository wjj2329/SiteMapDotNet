using Microsoft.Extensions.DependencyInjection;
using SiteMapNetCore;
using System.IO;
using Microsoft.Extensions.Hosting;

public static class SiteMapServiceCollectionExtensions
{
    public static IServiceCollection AddSiteMap(this IServiceCollection services, string siteMapFileName = "web.sitemap")
    {
        services.AddSingleton<SiteMap>(sp =>
        {
            var env = sp.GetRequiredService<IHostEnvironment>();
            var path = Path.Combine(env.ContentRootPath, siteMapFileName);

            // your hidden static method call
            return SiteMapHelper.ReadWebSiteMap(path);
        });

        return services;
    }
}
