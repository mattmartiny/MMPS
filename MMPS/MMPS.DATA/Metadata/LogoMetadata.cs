using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMPS.DATA//.Metadata
{
    class LogoMetadata
    {
        public int imageID { get; set; }
        [Display(Name = "Image Name")]
        public string imageName { get; set; }
        public string description { get; set; }
        public string imageSource { get; set; }
        public int typeID { get; set; }
        public string alt { get; set; }
        public int LogoOrder { get; set; }
    }
    [MetadataType(typeof(LogoMetadata))]
    public partial class Logo { }
}
