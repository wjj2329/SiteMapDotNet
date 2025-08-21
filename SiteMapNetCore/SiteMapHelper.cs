using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace SiteMapNetCore
{
    public static class SiteMapHelper
    {
        // -------------------------
        // Load / Deserialize
        // -------------------------
        public static SiteMap ReadWebSiteMap(string siteMapFileName)
        {
            var serializer = new XmlSerializer(typeof(SiteMap));
            using var reader = new StreamReader(siteMapFileName);

            var deserialized = serializer.Deserialize(reader);
            if (deserialized is SiteMap siteMap)
            {
                InitializeSiteMapNodes(siteMap);
                return siteMap;
            }

            throw new InvalidOperationException("Deserialization returned null or incorrect type.");
        }

        // -------------------------
        // Initialize site map nodes
        // -------------------------
        private static void InitializeSiteMapNodes(SiteMap siteMap)
        {
            // Initialize the root node first
            siteMap.OnInitialized();

            // BFS to initialize all nodes with root reference
            var nodesQueue = new Queue<SiteMapNode>();
            var rootNode = siteMap.RootNode;

            nodesQueue.Enqueue(rootNode);

            while (nodesQueue.Count > 0)
            {
                var currentNode = nodesQueue.Dequeue();
                currentNode.OnInitialized(rootNode);

                foreach (var child in currentNode.ChildNodes)
                {
                    nodesQueue.Enqueue(child);
                }
            }
        }

        // -------------------------
        // Utility methods
        // -------------------------
        public static string FormatUrl(string url)
        {
            if (string.IsNullOrEmpty(url)) return string.Empty;

            if (url.StartsWith("~/")) return url.Substring(1); // ASP.NET style
            if (url.StartsWith('/')) return url;               // Already correct

            // Prepend slash if missing
            return '/' + url;
        }
    }
}
