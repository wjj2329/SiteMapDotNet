using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SiteMapNodeNetCore
{
    public class SiteMap
    {
        private SiteMapNode _rootNode;
        public SiteMapNode RootNode { get { return _rootNode; } }

        public void ReadWebSiteMap(string siteMapFileName) {
            XmlDocument doc = new XmlDocument();
            doc.Load(siteMapFileName);
            XmlNode root = doc.DocumentElement;
            _rootNode = new SiteMapNode();
        }

    }
}
