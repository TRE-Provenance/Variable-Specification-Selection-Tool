

using DaSH_Researcher_Portal.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace DaSH_Researcher_Portal.DataActions
{
    public class ResearcherActions
    {

        public static void AddNewResearcherToProject(int projectId, string ResearcherName, string UserName, string Email)
        {

            bool AddResToProj = false;
            using (var ctx = new DareToDaSHEntities2()) // DareDashModel())
            {
                Researcher res = GetResearcherByUserNameAndProjId(UserName,projectId);
                if (res is null)
                {
                    AddResToProj = true;    
                    res=GetResearcherByUserName(UserName);
                    if (res is null)
                    {
                        res = new Researcher();
                        res.Email = Email;
                        res.UserName = UserName;
                        res.ResearcherName = ResearcherName;

                        ctx.Researchers.Add(res);

                    }

                    if (AddResToProj)
                    {
                        Reseacher_Projects resP = new Reseacher_Projects();   

                        resP.ProjectId = projectId;
                        resP.ResearcherID = res.ResearcherID;
                        ctx.Reseacher_Projects.Add(resP);   
                    }                  
                }
            }
        }


        private void AddResearcherToProject(int projID, int researcherID)
        {
            using (var ctx = new DareToDaSHEntities2()) // DareDashModel())
            {
                Reseacher_Projects rp = GetResearcher_ProjectsByResearcherId(researcherID);
                if (rp is null)
                {
                    rp = new Reseacher_Projects();
                    rp.ProjectId = projID;
                    rp.ResearcherID = researcherID;

                    ctx.Reseacher_Projects.Add(rp);
                }
            }

        }

        public static Reseacher_Projects GetResearcher_ProjectsByResearcherId(int researcherID)
        {
            using (var ctx = new DareToDaSHEntities2()) // DareDashModel())
            {
                var query1 =
              (from pds in ctx.Reseacher_Projects
               join res in ctx.Researchers on pds.ResearcherID equals res.ResearcherID
               where pds.ResearcherID == researcherID
               select pds).FirstOrDefault();

                return query1;

            }
        }


        public static List<Researcher> GetResearchersByProjectID(int projectId)
        {
            using (var ctx = new DareToDaSHEntities2()) // DareDashModel())
            {
                var query1 =
                 (from pds in ctx.Reseacher_Projects
                  join res in ctx.Researchers on pds.ResearcherID equals res.ResearcherID
                  where pds.ProjectId == projectId
                  select res).ToList();

                return query1;
            }
        }


        public static List<Researcher> GetResearchersByResearcherID(int researcherId)
        {
            using (var ctx = new DareToDaSHEntities2()) // DareDashModel())
            {
                var query1 =
                 (
                  from res in ctx.Researchers
                  where res.ResearcherID == researcherId
                  select res).ToList();

                return query1;
            }
        }



        public static Researcher GetResearcherByUserNameAndProjId(string userName,int projectId)
        {
            using (var ctx = new DareToDaSHEntities2()) // DareDashModel())
            {

                var query1 =
                 (from pds in ctx.Reseacher_Projects
                  join res in ctx.Researchers on pds.ResearcherID equals res.ResearcherID
                  where pds.ProjectId == projectId && res.UserName == userName
                  select res).FirstOrDefault();

                return query1;
            }
        }


        public static Researcher GetResearcherByUserName(string userName)
        {
            using (var ctx = new DareToDaSHEntities2()) // DareDashModel())
            {
                var query1 =
                 (
                  from res in ctx.Researchers
                  where res.UserName == userName
                  select res).FirstOrDefault();

                return query1;
            }
        }



        public static int GetReseracherIdByUserName(string UserName)
        {
            int ret = -1;
            if (!string.IsNullOrEmpty(UserName))
            {
                using (var ctx = new DareToDaSHEntities2())// DareDashModel())
                {
                    var res = (from r in ctx.Researchers where r.UserName.ToUpper() == UserName.ToUpper() select r).FirstOrDefault();

                    ret = res.ResearcherID;
                }
            }
            return ret;
        }
    }

}



