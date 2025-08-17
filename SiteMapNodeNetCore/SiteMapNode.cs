
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace SiteMapNetCore
{
    [XmlRoot("siteMapNode")]
    public class SiteMapNode
    {
        [XmlElement("siteMapNode")]
        public List<SiteMapNode> ChildNodes { get; set; }
        [XmlAttribute("title")]
        public string? Title;
        [XmlAttribute("description")]
        public string? Description;
        [XmlAttribute("url")]
        public string Url;
        [XmlAttribute("roles")]
        public string? RolesString;
        [XmlAttribute("resourceKey")]
        public string? ResourceKey;
        // Capture any additional attributes
        [XmlAnyAttribute]
        public XmlAttribute[]? AdditionalAttributes { get; set; }
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

        private SiteMapNode? parent_node;
        private void SetParentNode(SiteMapNode node)
        {
            parent_node = node;
        }

        public SiteMapNode ParentNode => parent_node;

        private SiteMapNode? previous_sibling;
        private void SetPreviousSibling(SiteMapNode node)
        {
            previous_sibling = node;
        }
        public SiteMapNode PreviousSibling => previous_sibling;

        private SiteMapNode? next_sibling;
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
        public string Key => Url == null || Url.Length == 0 ? "" : Url.Substring(1);
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

    }
}
