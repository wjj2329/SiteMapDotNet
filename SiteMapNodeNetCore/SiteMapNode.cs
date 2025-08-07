namespace SiteMapNodeNetCore
{
    public class SiteMapNode
    {
        public SiteMapNode RootNode; //pointer to parent rootnode
        public string Title;
        public List<SiteMapNode> Children;
        public List<SiteMapNode> Parent;
        public string Description;
        public bool HasDescription;
        public string Key;
        public SiteMapNode NextNode;

        public bool Equals(SiteMapNode other)
        {
            return this == other;
        }

    }
}
