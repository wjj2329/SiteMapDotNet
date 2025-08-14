using System.Xml.Serialization;

namespace SiteMapNodeNetCore
{
    public class SiteMapHelper
    {
        public SiteMap ReadWebSiteMap(string siteMapFileName)
        {
            var serializer = new XmlSerializer(typeof(SiteMap));
            using (var reader = new StreamReader(siteMapFileName))
            {
                var c = serializer.Deserialize(reader);
                if (c is SiteMap siteMap)
                {
                    InitializeSiteMapNodes(siteMap);
                    return siteMap;
                }
                throw new InvalidOperationException("Deserialization returned null or incorrect type.");
            }
        }
        private void InitializeSiteMapNodes(SiteMap c)
        {
            Queue<SiteMapNode> nodesQueue = new Queue<SiteMapNode>(c.Children);
            SiteMapNode parent = c.Children[0];
            while (nodesQueue.Count > 0)
            {
                var currentNode = nodesQueue.Dequeue();
                currentNode.OnInitialized(parent);
                foreach (var child in currentNode.ChildNodes)
                {
                    nodesQueue.Enqueue(child);
                }
            }
        }
    }
}
