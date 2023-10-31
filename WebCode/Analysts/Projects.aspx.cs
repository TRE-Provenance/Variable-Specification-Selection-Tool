using CsvHelper;
using DaSH_Researcher_Portal.DataActions;
using DaSH_Researcher_Portal.EntityFramework;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DaSH_Researcher_Portal.Analysts
{
    public partial class Projects : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProjects();
            }
        }



        private void LoadProjects()
        {
            using (var ctx = new DareToDaSHEntities2())// DareDashModel())
            {
                var query1 =
                  (from pds in ctx.Projects
                   where pds.StatusId == 3  //3 is submitted but really we want authorised or proceeding or similar - this functionality is out of scope for proof of concept
                   select pds).ToArray();

                if (query1 != null)
                {
                    gv_Projects.DataSource = query1;
                    gv_Projects.DataBind();
                }
            }
        }



        public void lnkBtn_JSon_Click(object sender, EventArgs e)
        {
                        LinkButton lb = (LinkButton)sender;
            int ProjectID = Convert.ToInt32(lb.CommandArgument.ToString());
            string output = "";
            using (var ctx = new DareToDaSHEntities2())// DareDashModel())
            {
                var datasets = (from pDset in ctx.Project_Datasets
                                where pDset.ProjectId == ProjectID
                                select pDset).ToArray();
                              
                foreach (var ds in datasets)
                {
                    var qq = ctx.OutputSourceDatasets_toJson_byProjectId_DatasetID(ProjectID, ds.DatasetID);
                    output += qq;
                }
            }

            string zipFileName = "Project_" + ProjectID.ToString() + "_JSON_ouput"+"_" + DateTime.Now.ToString() +".zip";

            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("content-disposition", "attachment;filename=" + zipFileName);

            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {

                    var fileToMake = archive.CreateEntry("project_JSON_" + ProjectID.ToString() +"_" + DateTime.Now.ToString() + ".csv");

                    using (var entryStream = fileToMake.Open())
                    using (TextWriter textWriter = new StreamWriter(entryStream))
                    using (CsvWriter csv = new CsvWriter(textWriter, CultureInfo.InvariantCulture))
                    {
                        // csv.WriteRecords(query1);
                        csv.WriteRecords(output);
                        textWriter.Flush();
                    }

                    using (var fileStream = Response.OutputStream)
                    {
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        memoryStream.CopyTo(fileStream);
                    }
                }
            }
            Response.End();
        }
    



        public void dsVars_To_Single_CSV(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            int ProjectID = Convert.ToInt32(lb.CommandArgument.ToString());
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


                        var fileToMake = archive.CreateEntry("Project" + ProjectID.ToString() + "_AllDatasets.csv");
                        using (var entryStream = fileToMake.Open())
                        using (TextWriter textWriter = new StreamWriter(entryStream))
                        {
                            using (CsvWriter csv = new CsvWriter(textWriter, CultureInfo.InvariantCulture))
                            {
                                var csvList = new List<csvVars_HELPER>();
                                foreach (var d in projectSelectedDatasets) //loop through selected datasets
                                {

                                    var qryCustomVars = ResearcherDataSetActions.getVarsForCSV(ProjectID, d.DatasetID);
                                    // csv.WriteRecords(query1);

                                    csvList.AddRange(qryCustomVars);
                                }
                                csv.WriteRecords(csvList);
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

        public void DsVars_To_CSV(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            int ProjectID = Convert.ToInt32(lb.CommandArgument.ToString());

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

            LinkButton lb = (LinkButton)sender;
            int ProjectID = Convert.ToInt32(lb.CommandArgument.ToString());

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
    }
}