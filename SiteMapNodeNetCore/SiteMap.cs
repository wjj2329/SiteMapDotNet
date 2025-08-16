using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SiteMapNodeNetCore
{
    [XmlRoot("siteMap", Namespace = "http://schemas.microsoft.com/AspNet/SiteMap-File-1.0")]
    public class SiteMap
    {
        [XmlElement("siteMapNode")]
        public SiteMapNode RootNode { get; set; }
        //HttpContext is no longer a static object we can get whenever in netCore. It must be passed along
        public SiteMapNode CurrentNode(HttpContext httpContext) => _siteMapTable[httpContext.Request.Path];
        public SiteMapNode CurrentNode(string path) => _siteMapTable[path];

        private Dictionary<string, SiteMapNode> _siteMapTable = new Dictionary<string, SiteMapNode>();

        public void OnInitialized()
        {
            Queue<SiteMapNode> myQueue = new Queue<SiteMapNode>();
            myQueue.Enqueue(RootNode);
            while (myQueue.Count > 0)
            {
                SiteMapNode temp = myQueue.Dequeue();
                _siteMapTable.Add(temp.Url, temp);
                foreach (SiteMapNode node in temp.ChildNodes)
                {
                    myQueue.Enqueue(node);
                }
            }
        }

    }
}
