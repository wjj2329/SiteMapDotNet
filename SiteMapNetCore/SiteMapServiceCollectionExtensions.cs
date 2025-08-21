using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SiteMapNetCore;
using System.IO;

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
