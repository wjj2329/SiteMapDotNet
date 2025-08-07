using SiteMapNodeNetCore;

namespace SiteMapNetCoreTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            SiteMap t = new SiteMap();
            t.ReadWebSiteMap("Web.sitemap");

        }
    }
}