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
        // -------------------------
        // Private fields
        // -------------------------
        private string _url = string.Empty;

        // -------------------------
        // Public properties
        // -------------------------

        // Child nodes
        [XmlElement("siteMapNode")]
        public List<SiteMapNode> ChildNodes { get; init; } = [];

        public bool HasChildNodes => ChildNodes.Count > 0;

        // Standard attributes
        [XmlAttribute("title")]
        public string? Title { get; init; }

        [XmlAttribute("description")]
        public string? Description { get; init; }

        [XmlAttribute("url")]
        public required string Url
        {
            get => _url;
            init => _url = SiteMapHelper.FormatUrl(value);
        }

        [XmlAttribute("roles")]
        public string? RolesString { get; init; }

        [XmlAttribute("resourceKey")]
        public string? ResourceKey { get; init; }

        // Additional attributes
        [XmlAnyAttribute]
        public XmlAttribute[]? AdditionalAttributes { get; init; }

        public string? this[string attributeName]
        {
            get
            {
                if (AdditionalAttributes == null)
                    return null;

                var attr = AdditionalAttributes.FirstOrDefault(a => a.Name == attributeName);
                return attr?.Value;
            }
        }

        // Computed properties
        public List<string> Roles => string.IsNullOrWhiteSpace(RolesString)
            ? []
            : [.. RolesString.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(r => r.Trim())];

        public string Key => string.IsNullOrEmpty(Url) ? "" : Url.TrimStart('/');

        // Tree navigation
        [XmlIgnore]
        public SiteMapNode? ParentNode { get; private set; }
        [XmlIgnore]
        public SiteMapNode? PreviousSibling { get; private set; }
        [XmlIgnore]
        public SiteMapNode? NextSibling { get; private set; }
        [XmlIgnore]
        public SiteMapNode? RootNode { get; private set; }

        // -------------------------
        // Public methods
        // -------------------------
        public void OnInitialized(SiteMapNode root)
        {
            RootNode = root;

            for (int i = 0; i < ChildNodes.Count; i++)
            {
                var child = ChildNodes[i];
                child.ParentNode = this;

                if (i > 0) child.PreviousSibling = ChildNodes[i - 1];
                if (i < ChildNodes.Count - 1) child.NextSibling = ChildNodes[i + 1];

                // Recursively initialize children
                child.OnInitialized(root);
            }
        }

        // -------------------------
        // Overrides
        // -------------------------
        public override bool Equals(object? obj)
        {
            return obj is SiteMapNode other &&
                   string.Equals(Key, other.Key, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(Url, other.Url, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return Key?.ToLowerInvariant().GetHashCode() ?? 0;
        }

        public override string ToString()
        {
            return $"SiteMapNode: {Title ?? "(no title)"} ({Url ?? "(no url)"})";
        }
    }
}
