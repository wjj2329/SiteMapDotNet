using System.Xml.Serialization;

namespace SiteMapNodeNetCore
{
    [XmlRoot("siteMap", Namespace = "http://schemas.microsoft.com/AspNet/SiteMap-File-1.0")]
    public class SiteMap
    {
        [XmlElement("siteMapNode")]
        public List<SiteMapNode> Children { get; set; } = new();


    }
}
