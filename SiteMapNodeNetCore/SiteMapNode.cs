using System.Xml;
using System.Xml.Serialization;

namespace SiteMapNodeNetCore
{
    [XmlRoot("siteMapNode")]
    public class SiteMapNode
    {
        [XmlElement("siteMapNode")]
        public List<SiteMapNode> ChildNodes { get; set; } = new();
        [XmlAttribute("title")]
        public string Title;
        [XmlAttribute("description")]
        public string Description;
        [XmlAttribute("url")]
        public string Url;
        [XmlAttribute("roles")]
        public string RolesString;
        [XmlAttribute("resourceKey")]
        public string ResourceKey;
        // Capture any additional attributes
        [XmlAnyAttribute]
        public XmlAttribute[] AdditionalAttributes { get; set; }
        public string this[string attributeName]
        {
            get
            {
                if (AdditionalAttributes == null)
                    return null;
                var attr = AdditionalAttributes.FirstOrDefault(a => a.Name == attributeName);
                return attr?.Value;
            }
        }
        public bool HasChildNodes => ChildNodes.Count > 0;
        public List<string> Roles
        {
            get
            {
                if (string.IsNullOrWhiteSpace(RolesString))
                {
                    return new List<string>();
                }
                return RolesString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                  .Select(r => r.Trim())
                                  .ToList();
            }
        }

        private SiteMapNode parent_node = null;
        private void SetParentNode(SiteMapNode node)
        {
            parent_node = node;
        }

        public SiteMapNode ParentNode => parent_node;

        private SiteMapNode previous_sibling = null;
        private void SetPreviousSibling(SiteMapNode node)
        {
            previous_sibling = node;
        }
        public SiteMapNode PreviousSibling => previous_sibling;

        private SiteMapNode next_sibling = null;
        private void SetNextSibling(SiteMapNode node)
        {
            next_sibling = node;
        }
        public SiteMapNode NextSibling => next_sibling;

        private SiteMapNode root_node = null;
        public SiteMapNode RootNode => root_node;
        private void SeRootNode(SiteMapNode node)
        {
            root_node = node;
        }
        public string Key => Url == null || Url.Length == 0 ? "" : Url[1..];
        public void OnInitialized(SiteMapNode root)
        {
            SeRootNode(root);
            for (int i = 0; i < ChildNodes.Count; i++)
            {
                ChildNodes[i].SetParentNode(this);
                if (i != 0)
                {
                    ChildNodes[i].SetPreviousSibling(ChildNodes[i - 1]);
                }
                if (i != ChildNodes.Count - 1)
                {
                    ChildNodes[i].SetNextSibling(ChildNodes[i + 1]);
                }
            }
        }
        //copied from original netfw class
        public override bool Equals(object obj)
        {
            if (obj is SiteMapNode siteMapNode && Key == siteMapNode.Key)
            {
                return string.Equals(Url, siteMapNode.Url, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }
        /*public virtual SiteMapNode Clone()
        {
            ArrayList roles = null;
            NameValueCollection attributes = null;
            NameValueCollection explicitResourceKeys = null;
            if (Roles != null)
            {
                roles = new ArrayList(Roles);
            }

            if (_attributes != null)
            {
                attributes = new NameValueCollection(_attributes);
            }

            if (_resourceKeys != null)
            {
                explicitResourceKeys = new NameValueCollection(_resourceKeys);
            }

            return new SiteMapNode(_provider, Key, Url, Title, Description, roles, attributes, explicitResourceKeys, _resourceKey);
        }
        public virtual SiteMapNode Clone(bool cloneParentNodes)
        {
            SiteMapNode siteMapNode = Clone();
            if (cloneParentNodes)
            {
                SiteMapNode siteMapNode2 = siteMapNode;
                SiteMapNode parentNode = ParentNode;
                while (parentNode != null)
                {
                    SiteMapNode siteMapNode4 = (siteMapNode2.ParentNode = parentNode.Clone());
                    siteMapNode4.ChildNodes = new SiteMapNodeCollection(siteMapNode2);
                    parentNode = parentNode.ParentNode;
                    siteMapNode2 = siteMapNode4;
                }
            }

            return siteMapNode;
        }*/
    }
}
