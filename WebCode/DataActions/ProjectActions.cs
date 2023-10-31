using DaSH_Researcher_Portal.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace DaSH_Researcher_Portal.DataActions
{
    public class ProjectActions
    {





    public static void AddNewProject(Project proj, string userName)
        {

            if (proj == null)
            {
                return;
            }
            else
            {
                using (var ctx = new DareToDaSHEntities2()) // DareDashModel())
                {
                    Project newProj = new Project();
                    newProj = proj;
                    newProj.CreatedBy = userName;   
                    newProj.CreatedDate = DateTime.Now;                   
                    ctx.Projects.Add(newProj);  
                    ctx.SaveChanges();


                    Reseacher_Projects newResProjLink = new Reseacher_Projects();

                    newResProjLink.ProjectId = newProj.ProjectId;
                    newResProjLink.ResearcherID = ResearcherActions.GetReseracherIdByUserName(userName);

                    ctx.Reseacher_Projects.Add(newResProjLink);

                    ctx.SaveChanges();

                }

            }
        }


        public static void UpdateProject(Project proj, string userName)
        {

            if (proj == null)
            {
                return;
            }
            else
            {
                using (var ctx = new DareToDaSHEntities2()) // DareDashModel())
                {

                    Project updateProj = getProjectByID_UserName(proj.ProjectId, userName);
                    updateProj = proj;

                    updateProj.LastUpdatedBy = userName;
                    updateProj.LastUpdatedDate = DateTime.Now;
  
                    ctx.Projects.AddOrUpdate(updateProj);   
                    ctx.SaveChanges();
                
                }

            }
        }




        public static Project getProjectByID(int projectID)
        {

            using (var ctx = new DareToDaSHEntities2())// DareDashModel())
            {
                var query1 =
                  (from pds in ctx.Projects
                       // join vars in ctx.DS_Variables on pds.DatasetID equals vars.DatasetID
                   where pds.ProjectId == projectID
                   select pds).FirstOrDefault();

                return query1;
            }

        }


        public static Project getProjectByID_UserName(int projectID,string UserName)
        {
            using (var ctx = new DareToDaSHEntities2())// DareDashModel())
            {
                var query1 =
                  (from pds in ctx.Projects
                   join res in ctx.Reseacher_Projects on pds.ProjectId equals res.ProjectId
                   where res.Researcher.UserName == UserName   && res.ProjectId == projectID
                   select pds).FirstOrDefault();

                return query1;
            }
        }


        public static List<Project> getProjectByUserName(String userName)
        { 
            using (var ctx = new DareToDaSHEntities2())// DareDashModel())
            {
                var query1 =
                  (from pds in ctx.Projects
                   join res in ctx.Reseacher_Projects on pds.ProjectId equals res.ProjectId
                   where res.Researcher.UserName == userName
                   select pds).ToList();

                return query1;
            }
        }


        public static List<Researcher_Documents> getProjectDocuments(int projectID)
        {
                using (var ctx = new DareToDaSHEntities2()) // DareDashModel())
                {

                    var projDocs = (from docs in ctx.Researcher_Documents
                                    where docs.ProjectId == projectID
                                    select docs).ToList();
                    return projDocs;
                }

        }
        public static void UpdateProjectDataSets(IEnumerable<Project_Datasets> details)
        {

            using (var ctx = new DareToDaSHEntities2()) // DareDashModel())
            {
                foreach (var det in details)
            {
                
                    var projectDetails = (from a in ctx.Project_Datasets
                                          where (a.ProjectId == det.ProjectId && a.DatasetID == det.DatasetID)
                                          select a).FirstOrDefault();

                    if (det.IsSelected == false)
                    {
                        if (projectDetails != null)
                        {
                            //remove the reseracher vars and the project datasets
                            var resVars = ctx.Researcher_Variables.Where(x => x.Project_Dataset_Id == projectDetails.Project_Dataset_Id);
                            ctx.Researcher_Variables.RemoveRange(resVars);

                            ctx.Project_Datasets.Remove(projectDetails);
                            ctx.SaveChanges();
                       
                        }
                    }
                    else
                    {
                        if (projectDetails == null)
                        {
                            Project_Datasets pds = new Project_Datasets();
                            pds.ProjectId = det.ProjectId;
                            pds.DatasetID = det.DatasetID;
                            pds.IsSelected = true;
                            pds.CreatedDate = DateTime.Now;
                            ctx.Project_Datasets.Add(pds);
                            ctx.SaveChanges();
                        }
                    }
                      
                }
                ctx.Dispose();
            }
    }
    }
}