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
    
    public partial class Researcher_Custom_Variables
    {
        public int Researcher_Custom_VariableID { get; set; }
        public Nullable<int> Project_Dataset_Id { get; set; }
        public string Output_VarName { get; set; }
        public string Researcher_Comments { get; set; }
        public Nullable<int> DS_VariableID_1 { get; set; }
        public string DS_Variable1_VarName { get; set; }
        public Nullable<int> DS_VariableID_2 { get; set; }
        public string DS_Variable2_VarName { get; set; }
        public string LastUpdateBy { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    
        public virtual Project_Datasets Project_Datasets { get; set; }
    }
}
