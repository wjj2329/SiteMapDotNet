using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SiteMapNetCore
{
    [XmlRoot("siteMap", Namespace = "http://schemas.microsoft.com/AspNet/SiteMap-File-1.0")]
    public class SiteMap
    {
        [XmlElement("siteMapNode")]
        public SiteMapNode RootNode { get; set; }
        //HttpContext is no longer a static object we can get whenever in netCore. It must be passed along
        public SiteMapNode CurrentNode(HttpContext httpContext)
        {
            if (httpContext == null || httpContext.Request == null || httpContext.Request.Path == null)
            {
                throw new ArgumentNullException(nameof(httpContext), "HttpContext or Request Path cannot be null.");
            }
            return GetNodeByPath(httpContext.Request.Path.ToString());
        }
        private SiteMapNode GetNodeByPath(string path)
        {
            if (!_siteMapTable.ContainsKey(path))
            {
                return null;
            }
            return _siteMapTable[path];
        }

        private Dictionary<string, SiteMapNode> _siteMapTable = new Dictionary<string, SiteMapNode>();
        private string FormatUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return string.Empty;
            }
            if (url.StartsWith("~/")) // ASP.NET style URL
                return url.Substring(1);

            if (url.StartsWith("/")) // Starting with a slash
                return url;
            StringBuilder sb = new StringBuilder(url);
            sb.Insert(0, '/'); // Prepend a slash if it doesn't start with one
            return sb.ToString();
        }
        public void OnInitialized()
        {
            Queue<SiteMapNode> myQueue = new Queue<SiteMapNode>();
            myQueue.Enqueue(RootNode);
            while (myQueue.Count > 0)
            {
                SiteMapNode temp = myQueue.Dequeue();
                if (_siteMapTable.ContainsKey(temp.Url))
                {
                    throw new Exception($"Multiple nodes with the same URL {temp.Url} were found. XmlSiteMapProvider requires that sitemap nodes have unique URLs.");
                }
                temp.Url = FormatUrl(temp.Url);
                _siteMapTable.Add(temp.Url, temp);
                foreach (SiteMapNode node in temp.ChildNodes)
                {
                    myQueue.Enqueue(node);
                }
            }
        }

    }
}
