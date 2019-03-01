using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMPS.DATA//.Metadata
{
    class AdvertisementMetadata
    {
        public int imageID { get; set; }
        [Display(Name = "Image Name")]
        public string imageName { get; set; }
        public string description { get; set; }
        public string imageSource { get; set; }
         public int typeID { get; set; }
        public string alt { get; set; }

    }
    [MetadataType(typeof(AdvertisementMetadata))]
    public partial class Advertisement{ }
}
