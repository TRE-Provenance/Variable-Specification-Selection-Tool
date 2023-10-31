using DaSH_Researcher_Portal.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;

namespace DaSH_Researcher_Portal.DataActions
{
    public class Utilities
    {




        //public  void UpdateProject(Project proj, string userName)
        //{

        //    int rrr = -1;
        //}



        public static void UploadDocument(Researcher_Documents uploadDoc)
        {
            if (uploadDoc.ProjectId == null)
            {
                return;
            }
            else
            {
                using (var ctx = new DareToDaSHEntities2()) // DareDashModel())
                {
                 ctx.Researcher_Documents.Add(uploadDoc);
                    ctx.SaveChanges();
                   
                }
            }
        }



        public static Researcher_Documents DownloadDocument(int ProjectID, int DocId)
        {

            if (ProjectID <1)
            {
                return null;
            }
            else
            {

                using (var ctx = new DareToDaSHEntities2()) // DareDashModel())
                {

                   // Researcher_Documents updateProj = getProjectByID_UserName(proj.ProjectId, userName);

                        var query1 =
                          (from rDoc in ctx.Researcher_Documents
                               // join vars in ctx.DS_Variables on pds.DatasetID equals vars.DatasetID
                           where rDoc.ProjectId == ProjectID && rDoc.DocId == DocId
                           select rDoc).FirstOrDefault();

                        // Project proj = query1.FirstOrDefault(); 
                        return query1;
                    }

                }
        }


        public static int CheckResearcherLogin(string username)
        {
            int RetVal = -1;

            return RetVal;

        }

    }
}