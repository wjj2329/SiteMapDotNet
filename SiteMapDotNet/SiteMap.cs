using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SiteMapDotNet
{
    [XmlRoot("siteMap", Namespace = "http://schemas.microsoft.com/AspNet/SiteMap-File-1.0")]
    public class SiteMap
    {
        // -------------------------
        // Public properties
        // -------------------------
        [XmlElement("siteMapNode")]
        public required SiteMapNode RootNode { get; init; }

        // -------------------------
        // Private fields
        // -------------------------
        private readonly Dictionary<string, SiteMapNode> _siteMapTable = [];

        // -------------------------
        // Public methods
        // -------------------------
        public SiteMapNode? CurrentNode(HttpContext httpContext)
        {
            if (httpContext?.Request?.Path == null)
                return null;

            return GetNodeByPath(httpContext.Request.Path.ToString());
        }

        public void OnInitialized()
        {
            if (RootNode == null) throw new InvalidOperationException("RootNode is null.");

            var nodesQueue = new Queue<SiteMapNode>();
            nodesQueue.Enqueue(RootNode);

            while (nodesQueue.Count > 0)
            {
                var node = nodesQueue.Dequeue();

                if (_siteMapTable.ContainsKey(node.Url))
                {
                    throw new InvalidOperationException(
                        $"Multiple nodes with the same URL '{node.Url}' were found. Sitemap nodes must have unique URLs.");
                }

                _siteMapTable.Add(node.Url, node);

                foreach (var child in node.ChildNodes)
                {
                    nodesQueue.Enqueue(child);
                }
            }
        }

        // -------------------------
        // Private helpers
        // -------------------------
        private SiteMapNode? GetNodeByPath(string path)
        {
            return _siteMapTable.TryGetValue(path, out var node) ? node : null;
        }
    }
}
