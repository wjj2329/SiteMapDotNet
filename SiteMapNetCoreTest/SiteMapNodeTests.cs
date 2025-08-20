using SiteMapNetCore;

namespace SiteMapNetCoreTest
{
    public class SiteMapNodeTests
    {
        private readonly SiteMap _siteMap;
        private readonly SiteMapNode _homeNode;
        private readonly SiteMapNode _aboutUsNode;
        private readonly SiteMapNode _ourHistoryNode;
        private readonly SiteMapNode _teamNode;
        private readonly SiteMapNode _productsNode;
        private readonly SiteMapNode _productANode;
        private readonly SiteMapNode _productBNode;
        private readonly SiteMapNode _contactUsNode;
        private readonly SiteMapNode _findUsNode;
        private readonly SiteMapNode _likeUsNode;

        public SiteMapNodeTests()
        {
            _siteMap = SiteMapHelper.ReadWebSiteMap("Web.sitemap");
            _homeNode = _siteMap.RootNode;
            _aboutUsNode = _homeNode.ChildNodes[0];
            _ourHistoryNode = _aboutUsNode.ChildNodes[0];
            _teamNode = _aboutUsNode.ChildNodes[1];
            _productsNode = _homeNode.ChildNodes[1];
            _productANode = _productsNode.ChildNodes[0];
            _productBNode = _productsNode.ChildNodes[1];
            _contactUsNode = _homeNode.ChildNodes[2];
            _findUsNode = _homeNode.ChildNodes[3];
            _likeUsNode = _homeNode.ChildNodes[4];
        }

        [Fact]
        public void TestSiteMapLoaded()
        {
            Assert.NotNull(_siteMap);
            Assert.NotNull(_siteMap.RootNode);
        }

        [Fact]
        public void HomePageNodeTestCase()
        {
            Assert.Equal("Home Page", _homeNode.Title);
            Assert.Equal("The main landing page of the site", _homeNode.Description);
            Assert.Equal(new[] { "Admin", "User", "Guest" }, _homeNode.Roles);
            Assert.Equal("/Home/Index", _homeNode.Url);
            Assert.Equal("Home_Res", _homeNode.ResourceKey);
            Assert.Equal("foo", _homeNode["customAttr"]);
            Assert.Equal("bar", _homeNode["anotherAttr"]);
        }

        [Fact]
        public void AboutUsNodeTest()
        {
            Assert.Equal("About Us", _aboutUsNode.Title);
            Assert.Equal("Learn about our company", _aboutUsNode.Description);
            Assert.Equal(new[] { "Admin", "User" }, _aboutUsNode.Roles);
            Assert.Equal("/Home/About", _aboutUsNode.Url);
            Assert.Equal("About_Res", _aboutUsNode.ResourceKey);
        }

        [Fact]
        public void OurHistoryNodeTest()
        {
            Assert.Equal("Our History", _ourHistoryNode.Title);
            Assert.Equal("Company history page", _ourHistoryNode.Description);
            Assert.Equal(new[] { "User" }, _ourHistoryNode.Roles);
            Assert.Equal("/Home/History", _ourHistoryNode.Url);
            Assert.Equal("History_Res", _ourHistoryNode.ResourceKey);
        }

        [Fact]
        public void TeamNodeTest()
        {
            Assert.Equal("Team & Staff", _teamNode.Title);
            Assert.Equal("Meet our team", _teamNode.Description);
            Assert.Equal(new[] { "Admin", "User" }, _teamNode.Roles);
            Assert.Equal("/Home/Team", _teamNode.Url);
            Assert.Equal("Team_Res", _teamNode.ResourceKey);
        }

        [Fact]
        public void ProductsNodeTest()
        {
            Assert.Equal("Products", _productsNode.Title);
            Assert.Equal("What we offer", _productsNode.Description);
            Assert.Equal(new[] { "Admin", "User" }, _productsNode.Roles);
            Assert.Equal("/Products/Index", _productsNode.Url);
            Assert.Equal("Products_Res", _productsNode.ResourceKey);
        }

        [Fact]
        public void ProductANodeTest()
        {
            Assert.Equal("Product A", _productANode.Title);
            Assert.Equal("Details about Product A", _productANode.Description);
            Assert.Equal(new[] { "User" }, _productANode.Roles);
            Assert.Equal("/Products/A", _productANode.Url);
            Assert.Equal("ProdA_Res", _productANode.ResourceKey);
        }

        [Fact]
        public void ProductBNodeTest()
        {
            Assert.Equal("Product B", _productBNode.Title);
            Assert.Equal("Details about Product B", _productBNode.Description);
            Assert.Equal(new[] { "Admin" }, _productBNode.Roles);
            Assert.Equal("/Products/B", _productBNode.Url);
            Assert.Equal("ProdB_Res", _productBNode.ResourceKey);
        }

        [Fact]
        public void ContactUsNodeTest()
        {
            Assert.Equal("Contact Us", _contactUsNode.Title);
            Assert.Equal("How to reach us", _contactUsNode.Description);
            Assert.Equal(new[] { "Guest", "User" }, _contactUsNode.Roles);
            Assert.Equal("/Home/Contact", _contactUsNode.Url);
            Assert.Equal("Contact_Res", _contactUsNode.ResourceKey);
        }

        [Fact]
        public void FindUsNodeTest() //Test Url with just slash
        {
            Assert.Equal("Find Us", _findUsNode.Title);
            Assert.Equal("How to find us", _findUsNode.Description);
            Assert.Equal(new[] { "Guest", "User" }, _findUsNode.Roles);
            Assert.Equal("/Home/Find", _findUsNode.Url);
            Assert.Equal("Find_Res", _findUsNode.ResourceKey);
        }

        [Fact]
        public void LikeUsNodeTest() //Test Url without leading slash
        {
            Assert.Equal("Like Us", _likeUsNode.Title);
            Assert.Equal("How to like us", _likeUsNode.Description);
            Assert.Equal(new[] { "Guest", "User" }, _likeUsNode.Roles);
            Assert.Equal("/Home/Like", _likeUsNode.Url);
            Assert.Equal("Like_Res", _likeUsNode.ResourceKey);
        }
    }
}