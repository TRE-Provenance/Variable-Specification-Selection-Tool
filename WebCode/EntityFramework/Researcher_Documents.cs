//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DaSH_Researcher_Portal.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class Researcher_Documents
    {
        public int DocId { get; set; }
        public Nullable<int> ProjectId { get; set; }
        public string DocumentDescription { get; set; }
        public string DocumentName { get; set; }
        public byte[] DocData { get; set; }
        public string ContentType { get; set; }
        public string UploadedBy { get; set; }
        public Nullable<System.DateTime> UploadDate { get; set; }
    
        public virtual Project Project { get; set; }
    }
}
