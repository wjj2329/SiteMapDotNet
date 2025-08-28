
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using SiteMapDotNet;

public class FakeHostEnvironment : IHostEnvironment
{
    public string EnvironmentName { get; set; } = "Development";
    public string ApplicationName { get; set; } = "TestApp";
    public string ContentRootPath { get; set; } = Directory.GetCurrentDirectory();
    public IFileProvider ContentRootFileProvider { get; set; } = new NullFileProvider();
}
public class SiteMapServiceCollectionExtensionsTests
{
    [Fact]
    public void AddSiteMapDefaultFile()
    {
        // Arrange
        var services = new ServiceCollection();

        services.AddSingleton<IHostEnvironment>(new FakeHostEnvironment());

        // Act
        services.AddSiteMap(); // registers Web.sitemap by default
        var provider = services.BuildServiceProvider();

        // Assert
        var siteMap = provider.GetRequiredService<SiteMap>();
        Assert.NotNull(siteMap);
        Assert.Equal("/Home/Index", siteMap.RootNode.Url); 
    }

    [Fact]
    public void AddSiteMapFromCustomPath()
    {
        var tempFile = Path.GetTempFileName();
        File.WriteAllText(tempFile, @"<?xml version=""1.0"" encoding=""utf-8"" ?>
            <siteMap xmlns=""http://schemas.microsoft.com/AspNet/SiteMap-File-1.0"">
              <!-- Root node -->
              <siteMapNode
                  title=""Home Page""
                  description=""The main landing page of the site""
                  url=""~/Home/Index""
                  roles=""Admin,User,Guest""
                  resourceKey=""Home_Res""
                  customAttr=""foo""
                  anotherAttr=""bar"">
                </siteMapNode>
                </siteMap>");

        var services = new ServiceCollection();

        // Act
        services.AddSiteMapFromPath(tempFile);
        var provider = services.BuildServiceProvider();

        // Assert
        var siteMap = provider.GetRequiredService<SiteMap>();
        Assert.NotNull(siteMap);
        Assert.Equal("/Home/Index", siteMap.RootNode.Url);

        // Cleanup
        File.Delete(tempFile);
    }
}