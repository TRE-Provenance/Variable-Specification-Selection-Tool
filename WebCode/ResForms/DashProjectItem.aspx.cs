using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using DaSH_Researcher_Portal.DataActions;
using DaSH_Researcher_Portal.EntityFramework;

namespace DaSH_Researcher_Portal.ResForms
{
    public partial class DashProjectItem : System.Web.UI.Page
    {



        private string projectDescription;
        private string dashIdent;
        private string shortTitle;
        private string projectID;
        public string ProjectDescription { get { return projectDescription; } }
        public string DashIdent { get { return dashIdent; } }
        public string ShortTitle { get { return shortTitle; } }
        public string ProjectId { get { return projectID; } }



        private int projID;




        private string Username = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            bool isNumeric = int.TryParse(Request["resVar"], out projID);
            var cce = HttpContext.Current.Request.Cookies["Dare_UserAuthTicket"];
            Username = cce.Value;
            if (!IsPostBack)
            {
              //  int projID;//= Request["resVar"];
               
                if (isNumeric)
                {

                    loadData();

                }
                else
                {
                    //New project
                    dashIdent = "TBC";
                    shortTitle = "TBC";

                //    tb_projTitleTxt.Attributes["placeholder"] = "Proposed Title Of Project";
                    tb_proj_Desc.Attributes["placeholder"] = "Description Of Project";
                    tb_Cohort_Desc.Attributes["placeholder"] = "Description Of The Cohorot (Please keep concise and brief (e.g. Men over the age of 50 with disease (as defined by BNF codes))";
                    tb_projStartDate.Attributes["placeholder"] = "Proposed Start Date";
                    tb_projEndDate.Attributes["placeholder"] = "Proposed End Date";
                }
            }
        }


        private void loadData ()
        {
            var proj = ProjectActions.getProjectByID_UserName(Convert.ToInt32(projID), Username);

            if (proj != null)
            {
                tb_projStartDate.Text = ((DateTime)proj.ProjectStartDate).ToShortDateString();
                tb_projEndDate.Text = ((DateTime)proj.ProjectEndDate).ToShortDateString();
                tb_projTitle.Text = proj.ProjectTitle.ToString();
                tb_projTitle.Attributes["Title"] = proj.ProjectDescription.ToString();

                dashIdent = "TBC";
                shortTitle = "TBC";
                projectID = proj.ProjectId.ToString();
                if (proj.DashIdent != null)
                {
                    dashIdent = proj.DashIdent;
                    shortTitle = proj.ShortTitle;

                }
                projectDescription = proj.ProjectDescription.ToString();
                tb_proj_Desc.Text = proj.ProjectDescription;
                tb_Cohort_Desc.Text = proj.Cohort_Desc;

                loadDocuments();
                loadResearchers();

                if (proj.StatusId == 3) //submitted to DaSH so lock application
                {
                    lbl_Status.Text = "Project Locked (submitted to TRE for approval)";
                    locApp();
                }
            }
        }


        private void locApp()
        {
            btnSaveColumn.Visible = false;
            btnSubmit.Visible = false;
            btnAddResearcher.Visible = false;
           
            FileUpload1.Visible = false;


            tb_projTitle.ReadOnly = true;
            tb_proj_Desc.ReadOnly = true;
            tb_Cohort_Desc.ReadOnly = true;
            tb_InclusionCriteria.ReadOnly = true;
            tb_ExclusionCriteria.ReadOnly = true;
            tb_projStartDate.ReadOnly = true;
            tb_projEndDate.ReadOnly = true;



            btnUpload.Visible = false;
            tb_fileDescription.ReadOnly = true;
        
                tb_ResearcherFName.ReadOnly = true;
            tb_ResEmail.ReadOnly = true;
            tb_ResUserName.ReadOnly = true;

        }


        private void loadResearchers()
        {
            var res = DataActions.ResearcherActions.GetResearchersByProjectID(projID);
            if (res != null)
            {
                gv_AdditionalRes.DataSource = res;
                gv_AdditionalRes.DataBind();    
            }
        }

        private void loadDocuments()
        {
            var dpcs = DataActions.ProjectActions.getProjectDocuments(projID);

            if (dpcs != null)
            {
                gv_files.DataSource = dpcs;
                gv_files.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //MembershipUser mu = Membership.GetUser(Page.User.Identity.Name);


            if (Request["resVar"] == "AddNew")
            {

                Project newProj = new Project();

        //        newProj.ProjectTitle = tb_projTitleTxt.Text;
                newProj.ProjectDescription = tb_proj_Desc.Text;
                newProj.Cohort_Desc = tb_Cohort_Desc.Text;

                newProj.ExclusionCriteria = tb_ExclusionCriteria.Text;
                newProj.InclusionCriteria = tb_ExclusionCriteria.Text;

                newProj.ProjectStartDate = Convert.ToDateTime(tb_projStartDate.Text);
                newProj.ProjectEndDate = Convert.ToDateTime(tb_projEndDate.Text);

                DataActions.ProjectActions.AddNewProject(newProj, Username);// User.Identity.Name);

                // DataActions.ProjectActions.AddNewProject(newProj, mu.UserName);// User.Identity.Name);
            }
            else
            {

                //update Existing project
                Project updateProj = new Project();

                updateProj.ProjectId = projID;
                updateProj.ProjectTitle = tb_projTitle.Text;
                updateProj.ProjectDescription = tb_proj_Desc.Text;
                updateProj.Cohort_Desc = tb_Cohort_Desc.Text;


                updateProj.ExclusionCriteria = tb_ExclusionCriteria.Text;
                updateProj.InclusionCriteria = tb_ExclusionCriteria.Text;
                updateProj.ProjectStartDate = Convert.ToDateTime(tb_projStartDate.Text);
                updateProj.ProjectEndDate = Convert.ToDateTime(tb_projEndDate.Text);


                //DataActions.ProjectActions.UpdateProject(updateProj, mu.UserName);
                DataActions.ProjectActions.UpdateProject(updateProj, Username);

            }

            loadData();

        }


        protected void btnSubmist_Click(object sender, EventArgs e)
        {

            //submit project to Dash RCs for approval
            //add checks for supporting documents
            //email Dash that new project proposal submitted
            var proj = ProjectActions.getProjectByID(projID);


            proj.StatusId = 3;  //submitted for approval
            //MembershipUser mu = Membership.GetUser(Page.User.Identity.Name);

            //DataActions.ProjectActions.UpdateProject(proj, mu.UserName);
            DataActions.ProjectActions.UpdateProject(proj,Username);

            loadData();
        }



        protected void Upload(object sender, EventArgs e)
        {
            string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string contentType = FileUpload1.PostedFile.ContentType;
         //   MembershipUser mu = Membership.GetUser(Page.User.Identity.Name);
            using (Stream fs = FileUpload1.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    Researcher_Documents newupload = new Researcher_Documents();
                    newupload.ProjectId = projID;
                    newupload.DocumentDescription = tb_fileDescription.Text;
                    newupload.DocumentName = filename;
                    newupload.DocData = bytes;
                    newupload.ContentType = contentType;

                    newupload.UploadedBy = Username;
                    //newupload.UploadedBy = mu.UserName;

                    newupload.UploadDate = DateTime.Now;
                 
                  Utilities.UploadDocument(newupload);    

                }
            }
            Response.Redirect(Request.Url.AbsoluteUri);
        }


        protected void DownloadFile(object sender, EventArgs e)
        {
            int id = int.Parse((sender as LinkButton).CommandArgument);
            byte[] bytes;
            string fileName, contentType;


            Researcher_Documents newDownload = new Researcher_Documents();



            newDownload = Utilities.DownloadDocument(projID, id);


            bytes = (byte[])newDownload.DocData;
            contentType = newDownload.ContentType;
            fileName = newDownload.DocumentName;


            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }




        protected void AddResearcher(object sender, EventArgs e)
        {
            DataActions.ResearcherActions.AddNewResearcherToProject(projID, tb_ResearcherFName.Text, tb_ResUserName.Text, tb_ResEmail.Text);
        }

    }
}