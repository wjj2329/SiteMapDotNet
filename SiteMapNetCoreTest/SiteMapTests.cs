using Microsoft.AspNetCore.Http;
using SiteMapDotNet;

namespace SiteMapDotNetTest
{
    public class SiteMapTests
    {
        private readonly SiteMap _siteMap;
        private readonly string[] _guestUserArr;

        public SiteMapTests()
        {
            _guestUserArr = ["Guest", "User"];
            _siteMap = SiteMapHelper.ReadWebSiteMap("Web.sitemap");
        }

        private static DefaultHttpContext CreateHttpContext(string path)
        {
            var context = new DefaultHttpContext();
            context.Request.Path = path;
            return context;
        }

        [Fact]
        public void CurrentNode_ReturnsContactNode()
        {
            var context = CreateHttpContext("/Home/Contact");
            var node = _siteMap.CurrentNode(context);

            Assert.NotNull(node);
            Assert.Equal("Contact Us", node!.Title);
            Assert.Equal("How to reach us", node.Description);
            Assert.Equal(_guestUserArr, node.Roles);
            Assert.Equal("/Home/Contact", node.Url);
            Assert.Equal("Contact_Res", node.ResourceKey);
        }

        [Fact]
        public void CurrentNode_ReturnsFindNode()
        {
            var context = CreateHttpContext("/Home/Find");
            var node = _siteMap.CurrentNode(context);

            Assert.NotNull(node);
            Assert.Equal("Find Us", node!.Title);
            Assert.Equal("How to find us", node.Description);
            Assert.Equal(_guestUserArr, node.Roles);
            Assert.Equal("/Home/Find", node.Url);
            Assert.Equal("Find_Res", node.ResourceKey);
        }

        [Fact]
        public void CurrentNode_ReturnsLikeNode()
        {
            var context = CreateHttpContext("/Home/Like");
            var node = _siteMap.CurrentNode(context);

            Assert.NotNull(node);
            Assert.Equal("Like Us", node!.Title);
            Assert.Equal("How to like us", node.Description);
            Assert.Equal(_guestUserArr, node.Roles);
            Assert.Equal("/Home/Like", node.Url);
            Assert.Equal("Like_Res", node.ResourceKey);
        }

        [Fact]
        public void CurrentNode_Throws_WhenHttpContextIsNull()
        {
            Assert.Null(_siteMap.CurrentNode(null));
        }

        [Fact]
        public void CurrentNode_ReturnsNull_WhenPathDoesNotExist()
        {
            var context = CreateHttpContext("/A/Random/NonPath/FakeNode");
            var node = _siteMap.CurrentNode(context);
            Assert.Null(node);
        }
    }
}
