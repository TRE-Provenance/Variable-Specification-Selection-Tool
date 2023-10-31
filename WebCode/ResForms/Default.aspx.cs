using DaSH_Researcher_Portal.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using DaSH_Researcher_Portal.DataActions;

using CsvHelper;
using System.Globalization;
using static OfficeOpenXml.ExcelErrorValue;
using ICSharpCode.SharpZipLib.Zip;
using System.IO.Compression;
using System.Collections;
using Newtonsoft.Json.Linq;

namespace DaSH_Researcher_Portal.ResForms
{
    public partial class Default : System.Web.UI.Page
    {

        public string ProjectId { get { return ProjectID.ToString(); } }
        private int ProjectID = -1;
        private bool projectId_isNumeric;

        private string projectDatasetID;
        public string ProjectDatasetID { get { return projectDatasetID; } }
        private string Username = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            projectId_isNumeric = int.TryParse(Request["resVar"], out ProjectID);

            /// quick fix to remove membership stuff for demo purposes
            var cce = HttpContext.Current.Request.Cookies["Dare_UserAuthTicket"];
            Username = cce.Value;

            if (!IsPostBack)
            {
                LoadDataSets();
            }
        }




        public void DsVars_To_CSV(object sender, EventArgs e)
        {
            string zipFileName = "Project_" + ProjectID.ToString() + "_" + DateTime.Now.ToString() + ".zip";

            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("content-disposition", "attachment;filename=" + zipFileName);

            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    using (var ctx = new DareToDaSHEntities2())// DareDashModel())
                    {
                        var projectSelectedDatasets =
                            (from pds in ctx.Project_Datasets
                             .Where(s => s.IsSelected == true && s.ProjectId == ProjectID)
                             select new
                             {
                                 pds.DatasetID,
                                 pds.Dataset.DS_Name
                             }).ToList();

                        foreach (var d in projectSelectedDatasets) //loop through selected datasets
                        {
                            var fileToMake = archive.CreateEntry(d.DS_Name + ".csv");
                            var qryCustomVars = ResearcherDataSetActions.getVarsForCSV(ProjectID, d.DatasetID);

                            using (var entryStream = fileToMake.Open())
                            using (TextWriter textWriter = new StreamWriter(entryStream))
                            using (CsvWriter csv = new CsvWriter(textWriter, CultureInfo.InvariantCulture))
                            {
                                   // csv.WriteRecords(query1);
                                csv.WriteRecords(qryCustomVars);
                                textWriter.Flush();
                            }
                        }
                    }
                }
                using (var fileStream = Response.OutputStream)
                {
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    memoryStream.CopyTo(fileStream);
                }
            }
            Response.End();
        }



        protected void DsVars_To_Excel(object sender, EventArgs e)
        {
            using (var ctx = new DareToDaSHEntities2())// DareDashModel())
            {
                ExcelPackage excel = new ExcelPackage();

                //Do the cohort sheet stuff 
                var Proj = (
                              from rProj in ctx.Projects
                              where rProj.ProjectId == ProjectID
                              select new
                              {
                                  rProj
                              }
                                  ).FirstOrDefault();

                if (Proj != null)
                {
                    var workSheetProj = excel.Workbook.Worksheets.Add("Cohort");
                    workSheetProj.TabColor = System.Drawing.Color.Green;
                    workSheetProj.DefaultRowHeight = 12;
                    workSheetProj.Row(1).Height = 20;
                    workSheetProj.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheetProj.Row(1).Style.Font.Bold = true;
                    workSheetProj.Cells[1, 1].Value = "Cohort description";
                    workSheetProj.Cells[1, 2].Value = "Inclusion Criteria";
                    workSheetProj.Cells[1, 3].Value = "Exclusion Criteria";

                    workSheetProj.Cells[2, 1].Value = Proj.rProj.Cohort_Desc;
                    workSheetProj.Cells[2, 2].Value = Proj.rProj.InclusionCriteria;
                    workSheetProj.Cells[2, 3].Value = Proj.rProj.ExclusionCriteria;

                    workSheetProj.Column(1).AutoFit();
                    workSheetProj.Column(2).AutoFit();
                    workSheetProj.Column(3).AutoFit();

                    var hos =
                      (from pds in ctx.Project_Datasets
                       .Where(s => s.IsSelected == true && s.ProjectId == ProjectID)
                       select new
                       {
                           pds.DatasetID,
                           pds.Dataset.DS_Name
                       }).ToList();

                    //int i = 0;
                    foreach (var d in hos)
                    {

                        var query1 = (from rsv in ctx.Researcher_Variables
                                      join pDset in ctx.Project_Datasets on rsv.Project_Dataset_Id equals pDset.Project_Dataset_Id
                                      //join rProj in ctx.Reseacher_Projects on pDset.ProjectId equals rProj.ProjectId
                                      join vars in ctx.DS_Variables on pDset.DatasetID equals vars.DatasetID
                                      where pDset.ProjectId == ProjectID && pDset.DatasetID == d.DatasetID
                                            && vars.DS_VariableID == rsv.DS_VariableID
                                      select new
                                      {
                                          rsv,
                                          pDset,
                                          // rProj,
                                          vars,
                                          VarSelected = ctx.Researcher_Variables
                                                      .Where(s => vars.DS_VariableID == s.DS_VariableID && vars.DatasetID == pDset.DatasetID)
                                                      .Select(s => 1)
                                                      .FirstOrDefault()

                                      }
                                      ).Where(x => x.VarSelected == 1).ToList();



                        //CUSTOM VARIABLES


                        var qryCustomVars = (from rcv in ctx.Researcher_Custom_Variables
                                      join pDset in ctx.Project_Datasets on rcv.Project_Dataset_Id equals pDset.Project_Dataset_Id
                                      where pDset.ProjectId == ProjectID && pDset.DatasetID == d.DatasetID
                                           
                                      select new
                                      {
                                          rcv,
                                          pDset
                                      }
                                  ).ToList();



                        var workSheet = excel.Workbook.Worksheets.Add(d.DS_Name);
                        workSheet.TabColor = System.Drawing.Color.Black;
                        workSheet.DefaultRowHeight = 12;
                        workSheet.Row(1).Height = 20;
                        workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        workSheet.Row(1).Style.Font.Bold = true;
                        workSheet.Cells[1, 1].Value = "Variable Number";
                        workSheet.Cells[1, 2].Value = "VarName";
                        //  workSheet.Cells[1, 3].Value = "DS_VariableID - to remove";
                        workSheet.Cells[1, 3].Value = "Output_VarName";
                        workSheet.Cells[1, 4].Value = "Description";

                        workSheet.Cells[1, 5].Value = "DataType";
                        workSheet.Cells[1, 6].Value = "Researcher Notes";
                        workSheet.Cells[1, 7].Value = "Additional Processing";
                        workSheet.Cells[1, 8].Value = "Additional Processing MIN Value";
                        workSheet.Cells[1, 9].Value = "Additional Processing MAX Value";
                        workSheet.Cells[1, 10].Value = "Is Identifiable (e.g. chi) ";

                        workSheet.Cells[1, 11].Value = "Var Description";

                        int recordIndex = 2;
                        foreach (var DSs in query1)
                        {
                            workSheet.Cells[recordIndex, 1].Value = (recordIndex - 1).ToString();
                            workSheet.Cells[recordIndex, 2].Value = DSs.vars.VarName;
                            workSheet.Cells[recordIndex, 3].Value = DSs.rsv.Output_VarName;
                            workSheet.Cells[recordIndex, 4].Value = DSs.vars.VarDescription;
                            workSheet.Cells[recordIndex, 5].Value = DSs.vars.DataType;
                            workSheet.Cells[recordIndex, 6].Value = DSs.rsv.Researcher_Comments;
                            workSheet.Cells[recordIndex, 7].Value = DSs.rsv.IsRestriction;
                            workSheet.Cells[recordIndex, 8].Value = DSs.rsv.IsRestrictionMinRange;
                            workSheet.Cells[recordIndex, 9].Value = DSs.rsv.IsRestrictionMaxRange;
                            workSheet.Cells[recordIndex, 10].Value = DSs.vars.Is_Identifiable;
                            workSheet.Cells[recordIndex, 11].Value = DSs.vars.VarDescription;
                            recordIndex++;
                        }

                        if (qryCustomVars != null)
                        {
                            foreach (var cVar in qryCustomVars)
                            {
                                workSheet.Cells[recordIndex, 1].Value = (recordIndex - 1).ToString();
                                workSheet.Cells[recordIndex, 2].Value = cVar.rcv.DS_Variable1_VarName + " : " + cVar.rcv.DS_Variable2_VarName;
                                workSheet.Cells[recordIndex, 3].Value = cVar.rcv.Output_VarName;
                                workSheet.Cells[recordIndex, 4].Value = "";
                                workSheet.Cells[recordIndex, 5].Value = "";
                                workSheet.Cells[recordIndex, 6].Value = cVar.rcv.Researcher_Comments;
                                workSheet.Cells[recordIndex, 7].Value = "";
                                workSheet.Cells[recordIndex, 8].Value = "";
                                workSheet.Cells[recordIndex, 9].Value = "";
                                workSheet.Cells[recordIndex, 10].Value = "";
                                workSheet.Cells[recordIndex, 11].Value = "";
                                recordIndex++;
                            }
                        }
                    }
                    string excelName = "DareToDash_Spec_" + DateTime.Now.ToString();
                    using (var memoryStream = new MemoryStream())
                    {
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment; filename=" + excelName + ".xlsx");
                        excel.SaveAs(memoryStream);
                        memoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
        }





        protected void LoadDatasetVars(int DatasetID)
        {
            pnl_varlist.Visible = false;
            rptDataSetDetails.DataSource = null;
            rptDataSetDetails.DataBind();

            var resVars = ResearcherDataSetActions.getResearcherSelectedVars(DatasetID, ProjectID);
            rptDataSetDetails.DataSource = resVars;
            rptDataSetDetails.DataBind();

            if (resVars != null)
            {
                //CUSTOM VARs
                //pop the var dropdowns
                dl_CustField_1.DataSource = resVars;
                dl_CustField_1.DataBind();
                dl_CustField_2.DataSource = resVars;
                dl_CustField_2.DataBind();
                //load already created Vars
                gv_CustomVariable.DataSource = ResearcherDataSetActions.getResearcherCustom_Variables(DatasetID, ProjectID);
                gv_CustomVariable.DataBind();


                foreach (RepeaterItem ri in rptDataSetDetails.Items)
                {
                    try
                    {
                        //really nasty way to get the projectDatasetID -  should be populated higher in tree with retreival procedures refined but time constraints so just grab 1st value from the variable repeater list
                        projectDatasetID = ((HiddenField)ri.FindControl("h_ProjDataSetID")).Value;
                        ((HiddenField)pnl_custVars.FindControl("h_ProjectDatasetID")).Value = projectDatasetID;
                        break;
                    }
                    catch
                    {

                    }
                }

            }

            pnl_varlist.Visible = true;

        }
        protected void btnSaveVariables_Click(object sender, EventArgs e)
        {

            List<Researcher_Variables> resVars = new List<Researcher_Variables>();


            if (projectId_isNumeric)
            {

                foreach (RepeaterItem ri in rptDataSetDetails.Items)
                {
                    var cb = (CheckBox)ri.FindControl("chkSelect");
                    var outVarName = (TextBox)ri.FindControl("output_VarName");
                    var projDataSEtId = (HiddenField)ri.FindControl("h_ProjDataSetID");
                    var ReseacherVariableID = (HiddenField)ri.FindControl("h_ReseacherVariableID");


                    bool isAdded = false;
                    Researcher_Variables rVar = new Researcher_Variables();
                    if (cb.Checked)
                    {

                        var varID = (Literal)ri.FindControl("DS_VariableID");
                        if (outVarName != null)
                        {
                            rVar.Output_VarName = outVarName.Text;
                        }
                        rVar.DS_VariableID = Convert.ToInt16(varID.Text);
                        rVar.Project_Dataset_Id = Convert.ToInt16(projDataSEtId.Value);
                        rVar.Researcher_VariableID = Convert.ToInt16(ReseacherVariableID.Value);



                        //  rVar.Project_Dataset_Id = Convert.ToInt16(Project_DS_Id.Text);
                        // rVar.Researcher_Comments = "TO BE ADDED";
                        rVar.IsRestriction = 0;
                        isAdded = true;
                    }

                    var cbIsresctriction = (CheckBox)ri.FindControl("cbIsresctriction");
                    if (cbIsresctriction.Checked)
                    {
                        rVar.IsRestriction = 1;
                        //##########################
                        var hDataType = (HiddenField)ri.FindControl("h_DataType");
                        rVar.IsRestrictionVariabletype = hDataType.Value;
                        if (hDataType.Value.ToUpper() == "DATE" || hDataType.Value.ToUpper() == "DATETIME2")
                        {
                            TextBox tb = (TextBox)ri.FindControl("tb_resctrictionMinDate");
                            rVar.IsRestrictionMinRange = tb.Text;
                            tb = (TextBox)ri.FindControl("tb_resctrictionMaxDate");
                            rVar.IsRestrictionMaxRange = tb.Text;
                        }

                        if (hDataType.Value.ToUpper() == "INT")
                        {
                            TextBox tb = (TextBox)ri.FindControl("tb_resctrictionMin_INT");
                            rVar.IsRestrictionMinRange = tb.Text;
                            tb = (TextBox)ri.FindControl("tb_resctrictionMax_INT");
                            rVar.IsRestrictionMaxRange = tb.Text;
                        }

                        isAdded = true;
                    }



                    if (isAdded)
                    {
                        resVars.Add(rVar);
                    }
                    else
                    {
                        rVar = null;
                    }

                }

                //  DataActions.ResearcherDataSetActions.UpdateResearcherDataVars(resVars, User.Identity.Name);

                DataActions.ResearcherDataSetActions.UpdateResearcherDataVars(resVars,Username);
                pnl_DownloadSpecBtn.Visible = ResearcherDataSetActions.datasetVarsSelected(ProjectID);
            }
        }





        protected void btn_AddCustVar_Click(object sender, EventArgs e)
        {
        
            // Add custom varaible data to DB


            Researcher_Custom_Variables rcv = new Researcher_Custom_Variables();    

            rcv.DS_Variable1_VarName = dl_CustField_1.SelectedItem.ToString();
            rcv.DS_Variable2_VarName = dl_CustField_2.SelectedItem.ToString()   ;
            rcv.DS_VariableID_1 = Convert.ToInt32(dl_CustField_1.SelectedValue.ToString());
            rcv.DS_VariableID_2 = Convert.ToInt32(dl_CustField_2.SelectedValue.ToString());
            rcv.Researcher_Comments = tb_CustVarDescrition.Text;
            rcv.Output_VarName = tb_CustomVarName.Text;





               var hProjDS_ID = (HiddenField)pnl_custVars.FindControl("h_ProjectDatasetID");
            // rcv.Project_Dataset_Id = Convert.ToInt16(ddl_ResDatasets.SelectedValue);

            rcv.Project_Dataset_Id = Convert.ToInt16(hProjDS_ID.Value);


            //ResearcherDataSetActions.AddResearcher_CustomVars(rcv, User.Identity.Name);
            ResearcherDataSetActions.AddResearcher_CustomVars(rcv, Username);

            tb_CustomVarName.Text = "";
            dl_CustField_1.SelectedValue = "";
            dl_CustField_2.SelectedValue = "";
            tb_CustVarDescrition.Text = "";


            gv_CustomVariable.DataSource = ResearcherDataSetActions.getResearcherCustom_Variables( Convert.ToInt16(ddl_ResDatasets.SelectedValue), ProjectID);
            gv_CustomVariable.DataBind();


        }
        #region DATASETS
        protected void btnSaveDataSets_Click(object sender, EventArgs e)
        {

            if (projectId_isNumeric)
            {
                List<Project_Datasets> projDS = new List<Project_Datasets>();


                foreach (RepeaterItem ri in rptDataSets.Items)
                {
                    var cb = (CheckBox)ri.FindControl("chkDataSet");

                    var DataSetId = (HiddenField)ri.FindControl("h_DataSetID");
                    Project_Datasets pds = new Project_Datasets();
                    if (cb.Checked)
                    {
                        pds.DatasetID = Convert.ToInt16(DataSetId.Value);
                        pds.ProjectId = ProjectID;
                        pds.IsSelected = true;
                        projDS.Add(pds);
                    }
                    else
                    {

                        pds.DatasetID = Convert.ToInt16(DataSetId.Value);
                        pds.ProjectId = ProjectID;
                        pds.IsSelected = false;
                        projDS.Add(pds);
                    }

                }
                DataActions.ProjectActions.UpdateProjectDataSets(projDS);

                loadDataSets_DropDown();

            }
            //   LoadDatasetVars();

        }

        private void loadDataSets_DropDown()
        {



            ddl_ResDatasets.Visible = false;

            //   bool showVarsPanel = false;
            // btn_DownloadSpec.Visible = false;
            ddl_ResDatasets.Items.Clear();
            ListItem ddItem = new ListItem();

            ddItem.Text = "--Please Select Dataset To View/Edit Variable Selection--";
            ddItem.Value = "-1";

            ddl_ResDatasets.Items.Add(ddItem);

            using (var ctx = new DareToDaSHEntities2()) // DareDashModel())
            {

                var hos = ctx.Datasets.Select(
                            x => new
                            {
                                x.DatasetID,
                                x.DS_Name,

                                dsSelected = ctx.Project_Datasets
                                .Where(s => x.DatasetID == s.DatasetID && s.IsSelected == true && s.ProjectId == ProjectID)
                                  .Select(s => true)
                                .FirstOrDefault()
                            }
                            ).ToList();


                foreach (var d in hos)
                {
                    if (d.dsSelected)
                    {
                        ddItem = new ListItem();
                        ddItem.Text = d.DS_Name;
                        ddItem.Value = d.DatasetID.ToString();
                        ddl_ResDatasets.Items.Add(ddItem);
                    }
                }


                if (ddl_ResDatasets.Items.Count > 1)
                {
                    ddl_ResDatasets.Visible = true;
                }


            }

            //show download spec button if at least one var is selected for each selected dataset
            //   btn_DownloadSpec.Visible = ResearcherDataSetActions.datasetVarsSelected(ProjectID);


            //    pnl_dsVars.Visible = showVarsPanel;
            pnl_DownloadSpecBtn.Visible = ResearcherDataSetActions.datasetVarsSelected(ProjectID);

            if (ddl_ResDatasets.SelectedValue == "-1")
            {
                pnl_varlist.Visible = false;
            }
        }



        private void LoadDataSets()
        {

            bool locApp = false;

            using (var ctx = new DareToDaSHEntities2())// DareDashModel())
            {


                var proj = ProjectActions.getProjectByID(ProjectID);
                var hos = ctx.Datasets.Select(
                            x => new
                            {
                                x.DatasetID,
                                x.DS_Name,
                                x.Description,
                                x.webURL,

                                dsSelected = ctx.Project_Datasets
                                .Where(s => x.DatasetID == s.DatasetID && s.IsSelected == true && s.ProjectId == ProjectID)
                                  .Select(s => true)
                                .FirstOrDefault()
                            }
                            ).ToList();

                pnl_Datasets.GroupingText = "Dataset Selection For Project : <b>" + ProjectActions.getProjectByID(ProjectID).ProjectTitle.ToString() + "</b>";


                rptDataSets.DataSource = null;
                rptDataSets.DataBind();

                var allDataSets = hos;

                if (proj.StatusId == 3) //submitted so locked
                {
                    //hid unselected datasets
                    allDataSets = allDataSets.Where(x => x.dsSelected == true).ToList();

                    locApp = true;
                }
                rptDataSets.DataSource = allDataSets;
                rptDataSets.DataBind();



                loadDataSets_DropDown();
            }


            if (locApp) //application locked
            {
                LockApp();
            }
        }
        #endregion



        private void LockApp()

        {

            btnSave_Datasets.Visible = false;
            btnSaveColumn.Visible = false;



            //disable all the checkboxes
            foreach (RepeaterItem ri in rptDataSets.Items)
            {
                foreach (Control c in ri.Controls)
                {
                    if (c.GetType() == typeof(CheckBox))
                    {
                        ((CheckBox)c).Enabled = false;

                    }

                }
            }


            tb_CustomVarName.Enabled = false;
            dl_CustField_1.Enabled = false;
            dl_CustField_2.Enabled = false;
            tb_CustVarDescrition.Enabled = false;




            //disable all the inputs
            //foreach (RepeaterItem ri in rptDataSetDetails.Items)
            //{
            //    foreach (Control c in ri.Controls )    
            //    {
            //        if (c.GetType() == typeof(CheckBox))
            //        {
            //            ((CheckBox)c).Enabled = false;


            //            if (((CheckBox)c).Checked == false)
            //            {
            //                ri.Visible = false;
            //            }

            //        }
            //        if (c.GetType() == typeof(TextBox))
            //        {
            //            ((TextBox)c).Enabled = false;

            //        }
            //    }

            //}
            //    rptDataSetDetails.Items

        }






        protected void ddl_ResDatasets_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            if (ddl != null)
            {
                if (ddl.SelectedValue != "-1")
                {
                    LoadDatasetVars(Convert.ToInt16(ddl.SelectedValue));
                }
            }/**/

        }


        //protected void CheckBox_Load(object sender, EventArgs e)
        //{
        //    //((CheckBox)sender).InputAttributes.Add("onchange", "resctrictionClick(this.id)");
        //    int i = 99;
        //}



        protected void rptDataSetDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            bool incVar = true;
            var proj = ProjectActions.getProjectByID(ProjectID);

            if (proj.StatusId == 3) //sudmitted to DaSH so readonly
            {
                incVar = false;
            }

            RepeaterItem ri = e.Item;
            HiddenField h;
            HiddenField h_VarName;
            h = (HiddenField)ri.FindControl("h_Releaseable");
            h_VarName = (HiddenField)ri.FindControl("h_VarName");

            var h_datatype = (HiddenField)ri.FindControl("h_DataType");

            if (h.Value == "0")
            {
                HtmlTableRow tr = ri.FindControl("tr") as HtmlTableRow;
                tr.Style.Add(HtmlTextWriterStyle.BackgroundColor, "mistyrose");
            }
            string pnl_ClientId = "";

            var cb = (CheckBox)ri.FindControl("cbIsresctriction");

            var pnlInt = (Panel)ri.FindControl("pnl_restrictionInt");  //this is the div around the int pickers
            pnlInt.Style.Add(HtmlTextWriterStyle.Display, "none");
            var pnlDate = (Panel)ri.FindControl("pnl_restictionDate");  //this is the div around the date pickers
            pnlDate.Style.Add(HtmlTextWriterStyle.Display, "none");

            if (h_datatype.Value.ToLower() == "int")
            {

                pnl_ClientId = pnlInt.ClientID;
                if (cb.Checked == true)
                {
                    pnlInt.Style.Add(HtmlTextWriterStyle.Display, "show");
                }
            }

            if (h_datatype.Value.ToLower() == "datetime2" || h_datatype.Value.ToLower() == "datetime" || h_datatype.Value.ToLower() == "date")
            {

                pnl_ClientId = pnlDate.ClientID;

                if (cb.Checked == true)
                {
                    pnlDate.Style.Add(HtmlTextWriterStyle.Display, "show");
                }
            }

            cb.InputAttributes.Add("onchange", "resctrictionClick('" + h_VarName.Value + "','" + cb.ClientID + "','" + pnl_ClientId + "')");

            if (incVar == false)
            {
                var varselected = (CheckBox)ri.FindControl("chkSelect");
                if (varselected.Checked)
                {
                    cb.Enabled = false;
                    ((TextBox)ri.FindControl("tb_resctrictionMinDate")).Enabled = false;
                    ((TextBox)ri.FindControl("tb_resctrictionMaxDate")).Enabled = false;
                    ((TextBox)ri.FindControl("tb_resctrictionMin_INT")).Enabled = false;
                    ((TextBox)ri.FindControl("tb_resctrictionMax_INT")).Enabled = false;
                    ((TextBox)ri.FindControl("output_VarName")).Enabled = false;

                }
                else
                {
                    ri.Visible = false;
                }
            }
        }
    }



}