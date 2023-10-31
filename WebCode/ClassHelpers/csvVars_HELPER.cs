public class csvVars_HELPER
{
    public int ID { get; set; }
    public string VarName { get; set; }
    public string DataType { get; set; }
    public string Output_VarName { get; set; }
    public int? IsRestriction { get; set; }
    public string IsRestrictionMinRange { get; set; }
    public string IsRestrictionMaxRange { get; set; }
    public int? Is_Identifiable { get; set; }
    public int? Can_Release { get; set; }

    public string VarDescription { get; set; }
    public int VarSelected { get; set; }

    public string Researcher_Comments { get; set; }

    public string DatsetSource { get; set; }

}
