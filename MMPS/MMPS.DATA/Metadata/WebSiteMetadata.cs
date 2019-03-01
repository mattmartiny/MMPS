using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMPS.DATA//.Metadata
{
    class WebSiteMetadata
    {

        public int imageID { get; set; }
        [Display(Name = "Site Name")]
        public string SiteName { get; set; }
        public string description { get; set; }
        public string imageSource { get; set; }
        public string siteUrl { get; set; }
        public int typeID { get; set; }
        public string alt { get; set; }
        public int SiteOrder { get; set; }
    }
    [MetadataType(typeof(WebSiteMetadata))]
    public partial class WebSite { }
}
