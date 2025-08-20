using Microsoft.AspNetCore.Http;
using SiteMapNetCore;

namespace SiteMapNetCoreTest
{
    public class SiteMapTests
    {
        private readonly SiteMap _siteMap;

        public SiteMapTests()
        {
            _siteMap = SiteMapHelper.ReadWebSiteMap("Web.sitemap");
        }
        [Fact]
        public void CurrentNode_ReturnsContactNode()
        {
            var httpContextHomeFullPath = new DefaultHttpContext();
            httpContextHomeFullPath.Request.Path = "/Home/Contact";
          
            var contactUsNode = _siteMap.CurrentNode(httpContextHomeFullPath);
         
            // Assert
            Assert.NotNull(contactUsNode);
            Assert.Equal("Contact Us", contactUsNode.Title);
            Assert.Equal("How to reach us", contactUsNode.Description);
            Assert.Equal(new[] { "Guest", "User" }, contactUsNode.Roles);
            Assert.Equal("/Home/Contact", contactUsNode.Url);
            Assert.Equal("Contact_Res", contactUsNode.ResourceKey);
        }

        [Fact]
        public void CurrentNode_ReturnsFindNode() //Test other two types
        {
            var httpContextHomeFullPath = new DefaultHttpContext();
            httpContextHomeFullPath.Request.Path = "/Home/Find";

            var findUsNode = _siteMap.CurrentNode(httpContextHomeFullPath);

            Assert.Equal("Find Us", findUsNode.Title);
            Assert.Equal("How to find us", findUsNode.Description);
            Assert.Equal(new[] { "Guest", "User" }, findUsNode.Roles);
            Assert.Equal("/Home/Find", findUsNode.Url);
            Assert.Equal("Find_Res", findUsNode.ResourceKey);
        }

        [Fact]

        public void CurrentNode_ReturnsLikeNode() //Test other two types
        {
            var httpContextHomeFullPath = new DefaultHttpContext();
            httpContextHomeFullPath.Request.Path = "/Home/Like";

            var likeUsNode = _siteMap.CurrentNode(httpContextHomeFullPath);

            Assert.Equal("Like Us", likeUsNode.Title);
            Assert.Equal("How to like us", likeUsNode.Description);
            Assert.Equal(new[] { "Guest", "User" }, likeUsNode.Roles);
            Assert.Equal("/Home/Like", likeUsNode.Url);
            Assert.Equal("Like_Res", likeUsNode.ResourceKey);
        }

        [Fact]
        public void CurrentNode_Throws_WhenHttpContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _siteMap.CurrentNode(null));
            var fakePathContext = new DefaultHttpContext();
            fakePathContext.Request.Path = "/A/Random/NonPath/FakeNode";
            Assert.Null(_siteMap.CurrentNode(fakePathContext));
        }

    }
}
