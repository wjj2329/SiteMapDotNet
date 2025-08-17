using System;
using System.Collections.Generic;
using System.Text;

namespace SiteMapNetCore
{
    internal interface ISiteMapHelper
    {
        public SiteMap ReadWebSiteMap(string siteMapFileName);
    }
}
