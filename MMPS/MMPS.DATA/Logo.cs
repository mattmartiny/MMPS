//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MMPS.DATA
{
    using System;
    using System.Collections.Generic;
    
    public partial class Logo
    {
        public int imageID { get; set; }
        public string imageName { get; set; }
        public string description { get; set; }
        public string imageSource { get; set; }
        public int typeID { get; set; }
        public string alt { get; set; }
        public int LogoOrder { get; set; }
    
        public virtual Type Type { get; set; }
    }
}
