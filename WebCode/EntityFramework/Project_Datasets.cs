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
    
    public partial class Project_Datasets
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Project_Datasets()
        {
            this.Researcher_Variables = new HashSet<Researcher_Variables>();
            this.Researcher_Custom_Variables = new HashSet<Researcher_Custom_Variables>();
        }
    
        public int Project_Dataset_Id { get; set; }
        public Nullable<int> ProjectId { get; set; }
        public Nullable<int> DatasetID { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string LastUpdateBy { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public Nullable<bool> IsSelected { get; set; }
        public string DS_Name { get; set; }
    
        public virtual Dataset Dataset { get; set; }
        public virtual Project Project { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Researcher_Variables> Researcher_Variables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Researcher_Custom_Variables> Researcher_Custom_Variables { get; set; }
    }
}