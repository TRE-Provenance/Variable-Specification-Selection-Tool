using DaSH_Researcher_Portal.EntityFramework;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI.WebControls;
using static DaSH_Researcher_Portal.ResForms.Default;

namespace DaSH_Researcher_Portal.DataActions
{
    public class ResearcherDataSetActions
    {

        public static bool datasetVarsSelected(int projectID)
        {
            bool retVal = false;

            using (var ctx = new DareToDaSHEntities2())// DareDashModel())
            {
                var query1 =
                  (from pds in ctx.Project_Datasets
                  // join vars in ctx.DS_Variables on pds.DatasetID equals vars.DatasetID
                   where pds.ProjectId == projectID
                   select pds).ToList();

                foreach(var  pds in query1)
                {
                    var query2 =
                        (from resVars in ctx.Researcher_Variables
                         join dsVars in ctx.DS_Variables on pds.DatasetID equals dsVars.DatasetID
                         where resVars.Project_Dataset_Id == pds.Project_Dataset_Id
                         && dsVars.Can_Release != 0
                         select resVars).ToList();

                    if (query2.Count() > 0 )
                    {
                        retVal = true;
                       // break;
                    }
                    else
                    {
                        retVal = false;
                        break;
                    }

                }
            }
                return retVal;
        }



        public static List<Researcher_Custom_Variables> getResearcherCustom_Variables(int DatasetID, int ProjectID)
        {
            // Researcher_Customer_Variables
            using (var ctx = new DareToDaSHEntities2())// DareDashModel())
            {
                var query1 =
             (from rcv in ctx.Researcher_Custom_Variables
              join pds in ctx.Project_Datasets on rcv.Project_Dataset_Id equals pds.Project_Dataset_Id
              where pds.ProjectId == ProjectID && pds.DatasetID == DatasetID
              select rcv).ToList();

                return query1;
            }
        }

        public static void AddResearcher_CustomVars(Researcher_Custom_Variables vars, string userName)
        {
            using (var ctx = new DareToDaSHEntities2())// DareDashModel())
            {                         
                    Researcher_Custom_Variables rVars = new Researcher_Custom_Variables();
                    rVars.DS_Variable1_VarName = vars.DS_Variable1_VarName;
                    rVars.DS_Variable2_VarName = vars.DS_Variable2_VarName;

                    rVars.DS_VariableID_1 = vars.DS_VariableID_1;
                    rVars.DS_VariableID_2 = vars.DS_VariableID_2;

                    rVars.Output_VarName = vars.Output_VarName;
                    rVars.Researcher_Comments = vars.Researcher_Comments;
                    rVars.Project_Dataset_Id = vars.Project_Dataset_Id;

                    rVars.CreatedBy = userName;
                    rVars.CreatedDate = DateTime.Now;

                ctx.Researcher_Custom_Variables.Add(rVars);

                ctx.SaveChanges();

            }
        }

        public static Array getResearcherSelectedVars(int DatasetID, int ProjectID)
        {
                   Array ret_List =null;
            using (var ctx = new DareToDaSHEntities2())// DareDashModel())
            {

                var query2 = (from vars in ctx.DS_Variables

                              join pds in ctx.Project_Datasets on vars.DatasetID equals pds.DatasetID


                              from rsv in ctx.Researcher_Variables
                              .Where(s => vars.DS_VariableID == s.DS_VariableID).DefaultIfEmpty()
                              where vars.DatasetID == DatasetID && pds.ProjectId == ProjectID
                              select new
                              {
                                  Can_Release = (vars.Is_Identifiable == 1) ? 0 :
                                        (vars.Is_Identifiable == 1 && vars.Can_Release == 1) ? 0 :
                                        vars.Can_Release ?? 1,
                                  pds.ProjectId,
                                  pds.DatasetID,
                                  pds.Project_Dataset_Id,
                                  vars.VarName,
                                  vars.DataType,
                                  varsDS = vars.DatasetID,
                                  vars.DS_VariableID,
                                  vars.VarDescription,
                                  vars.RequiredForDisplay,
                                  vars.Is_Identifiable,
                                  rsv.Output_VarName ,
                                  SelectedVar = (rsv.DS_VariableID != null) ? 1 : 0,
                                  VarSelected = ctx.Researcher_Variables
                            .Where(s => vars.DS_VariableID == s.DS_VariableID && pds.Project_Dataset_Id == s.Project_Dataset_Id)
                            .Select(s => 1)
                            .FirstOrDefault()
                                 ,IsRestriction = (rsv.IsRestriction != null) ? rsv.IsRestriction :0
                                  , rsv.IsRestrictionMinRange
                                  , rsv.IsRestrictionMaxRange
                                  ,
                                  Researcher_VariableID = ctx.Researcher_Variables
                            .Where(s => vars.DS_VariableID == s.DS_VariableID && pds.Project_Dataset_Id == s.Project_Dataset_Id)
                            .Select(s => s.Researcher_VariableID)
                            .FirstOrDefault()
                            ,
                                  xxxx = ctx.Researcher_Variables
                            .Where(s => vars.DS_VariableID == s.DS_VariableID && pds.Project_Dataset_Id == s.Project_Dataset_Id)
                            .Select(s => s.IsRestrictionMinRange)
                            .FirstOrDefault()
                              }).ToList();

                ret_List = query2.OrderByDescending(x => x.VarSelected).ToArray();



                var query1 =
                    (from pds in ctx.Project_Datasets
                     join vars in ctx.DS_Variables on pds.DatasetID equals vars.DatasetID 
                     //from rs in ctx.Researcher_Variables.Where
                     //               (s => vars.DS_VariableID == s.DS_VariableID 
                     //                   && vars.DatasetID == pds.DatasetID
                     //                   && pds.ProjectId == ProjectID

                     //               ).DefaultIfEmpty()
                     where vars.DatasetID == DatasetID && pds.ProjectId == ProjectID
                     select new
                     {
                         pds.ProjectId,
                         pds.DatasetID,
                         pds.Project_Dataset_Id,
                         vars.VarName,
                         vars.DataType,
                         varsDS = vars.DatasetID,
                         vars.DS_VariableID,
                        vars.VarDescription,
                        vars.RequiredForDisplay,
                        vars.Is_Identifiable,
                       

                         Can_Release =  (vars.Is_Identifiable == 1) ? 0 :
                                        (vars.Is_Identifiable == 1 && vars.Can_Release == 1) ? 0 :
                                        vars.Can_Release ?? 1,
                         Output_VarName = ctx.Researcher_Variables
                            .Where(s => vars.DS_VariableID == s.DS_VariableID &&  pds.Project_Dataset_Id == s.Project_Dataset_Id)
                            .Select(s=>s.Output_VarName)
                            .FirstOrDefault(),
                         VarSelected = ctx.Researcher_Variables
                            .Where(s => vars.DS_VariableID == s.DS_VariableID &&  pds.Project_Dataset_Id == s.Project_Dataset_Id)
                            .Select(s => 1)
                            .FirstOrDefault(),
                      Researcher_VariableID = ctx.Researcher_Variables
                            .Where(s => vars.DS_VariableID == s.DS_VariableID && pds.Project_Dataset_Id == s.Project_Dataset_Id)
                            .Select(s => s.Researcher_VariableID)
                            .FirstOrDefault(),
                         IsRestriction = ctx.Researcher_Variables
                            .Where(s => vars.DS_VariableID == s.DS_VariableID && pds.Project_Dataset_Id == s.Project_Dataset_Id)
                            .Select(s => s.IsRestriction)
                            .FirstOrDefault() ??0,
                         IsRestrictionMinRange = ctx.Researcher_Variables
                            .Where(s => vars.DS_VariableID == s.DS_VariableID && pds.Project_Dataset_Id == s.Project_Dataset_Id)
                            .Select(s => s.IsRestrictionMinRange)
                            .FirstOrDefault() ,
                         IsRestrictionMaxRange = ctx.Researcher_Variables
                            .Where(s => vars.DS_VariableID == s.DS_VariableID && pds.Project_Dataset_Id == s.Project_Dataset_Id)
                            .Select(s => s.IsRestrictionMaxRange)
                            .FirstOrDefault(),
                     }).ToList();


                if (query1 != null)
                {
                    ret_List = query1.OrderByDescending(x => x.VarSelected).ToArray();
                }
            }

            return ret_List;
        }

       
        public static List<csvVars_HELPER> getVarsForCSV(int ProjectID, int? DatasetID)
        {
            using (var ctx = new DareToDaSHEntities2())// DareDashModel())
            {
                var qryCustomVars = ((from rcv in ctx.Researcher_Custom_Variables
                                      join pDset in ctx.Project_Datasets on rcv.Project_Dataset_Id equals pDset.Project_Dataset_Id
                                      join ds in ctx.Datasets on pDset.DatasetID equals ds.DatasetID
                                      where pDset.ProjectId == ProjectID && pDset.DatasetID == DatasetID

                                      select new csvVars_HELPER
                                      {
                                          //rcv,
                                          //pDset
                                          VarName = rcv.DS_Variable1_VarName + " : " + rcv.DS_Variable2_VarName,
                                          DataType = ""
                                          ,Output_VarName = rcv.Output_VarName
                                          ,IsRestriction = -1
                                          ,IsRestrictionMinRange = ""
                                          ,IsRestrictionMaxRange = ""
                                          ,Is_Identifiable = -1
                                          ,Can_Release = 1
                                          ,VarDescription = "Custom Variable"
                                          ,VarSelected = 1
                                         ,Researcher_Comments = rcv.Researcher_Comments
                                         ,ID = rcv.Researcher_Custom_VariableID
                                         ,
                                          DatsetSource = ds.DS_Name

                                      }
                                    ).Union
                              (
                                        (from rsv in ctx.Researcher_Variables
                                         join pDset in ctx.Project_Datasets on rsv.Project_Dataset_Id equals pDset.Project_Dataset_Id
                                         //join rProj in ctx.Reseacher_Projects on pDset.ProjectId equals rProj.ProjectId
                                         join vars in ctx.DS_Variables on pDset.DatasetID equals vars.DatasetID
                                         join ds in ctx.Datasets on pDset.DatasetID equals ds.DatasetID
                                         where pDset.ProjectId == ProjectID && pDset.DatasetID == DatasetID
                                               && vars.DS_VariableID == rsv.DS_VariableID
                                         select new csvVars_HELPER
                                         {

                                             //pDset,
                                             // rProj,
                                             VarName = vars.VarName,
                                             DataType = vars.DataType,
                                             Output_VarName = rsv.Output_VarName,
                                             IsRestriction = rsv.IsRestriction,
                                             IsRestrictionMinRange = rsv.IsRestrictionMinRange,
                                             IsRestrictionMaxRange = rsv.IsRestrictionMaxRange,
                                             Is_Identifiable = vars.Is_Identifiable,
                                             Can_Release = vars.Can_Release,
                                             VarDescription = vars.VarDescription,
                                             VarSelected = ctx.Researcher_Variables
                                                         .Where(s => vars.DS_VariableID == s.DS_VariableID && vars.DatasetID == pDset.DatasetID)
                                                         .Select(s => 1)
                                                         .FirstOrDefault(),
                                             Researcher_Comments = rsv.Researcher_Comments
                                             ,ID= rsv.Researcher_VariableID
                                             ,DatsetSource = ds.DS_Name

                                         }
                               ).Where(x => x.VarSelected == 1)
                                      )
                                    ).ToList();


                return qryCustomVars;
            }
        }



        public static void UpdateResearcherDataVars(IEnumerable<Researcher_Variables> vars,string userName)
        {

            using (var ctx = new DareToDaSHEntities2())// DareDashModel())
            {
                ////this bit to be done betterly please

                var SS = vars.FirstOrDefault();

                if (SS != null)
                {
                    if (SS.DS_VariableID == null) // no variables selected so delete the selection
                    {
                        var oldVars = ctx.Researcher_Variables.Where(c => c.Project_Dataset_Id == SS.Project_Dataset_Id);
                        ctx.Researcher_Variables.RemoveRange(oldVars);
                        ctx.SaveChanges();
                    }
                    else
                    {
                        //delete any that have been unselected
                        var CurrentList = vars.Select(x => x.DS_VariableID).ToArray();


                        var CurrentListII = vars.Select(x => x.Output_VarName).ToArray();



                        var oldVars = from a in ctx.Researcher_Variables
                                      where a.Project_Dataset_Id == SS.Project_Dataset_Id
                                      && (
                                      !CurrentList.Contains(a.Researcher_VariableID)
                                      )
                                      select a;

                        var result = ctx.Researcher_Variables.Where(p => vars.All(p2 => p2.Researcher_VariableID != p.Researcher_VariableID));


                        ctx.Researcher_Variables.RemoveRange(oldVars);

                        ctx.SaveChanges();

                        //New set of dataset variables selected
                        foreach (var det in vars)
                        {
                            var variableDetails = (from a in ctx.Researcher_Variables
                                                   where (a.Project_Dataset_Id == det.Project_Dataset_Id && a.DS_VariableID == det.DS_VariableID)
                                                   select a).FirstOrDefault();
                            if (variableDetails == null)
                            {
                                //insert new section1 data
                                Researcher_Variables rVars = new Researcher_Variables();
                                rVars.DS_VariableID = det.DS_VariableID;
                                rVars.Output_VarName = det.Output_VarName;
                                rVars.Researcher_Comments = det.Researcher_Comments;
                                rVars.Project_Dataset_Id = det.Project_Dataset_Id;

                                rVars.IsRestriction = det.IsRestriction;
                                rVars.IsRestrictionMinRange = det.IsRestrictionMinRange;
                                rVars.IsRestrictionMaxRange = det.IsRestrictionMaxRange;

                                rVars.CreatedBy = userName;
                                rVars.CreatedDate = DateTime.Now;
                                ctx.Researcher_Variables.Add(rVars);

                                ctx.SaveChanges();

                            }
                            else
                            {
                                bool isUpate = false;

                                if (det.Output_VarName != variableDetails.Output_VarName)
                                {
                                    variableDetails.Output_VarName = det.Output_VarName;
                                    isUpate = true;
                                }

                                if (det.IsRestriction != variableDetails.IsRestriction)
                                {
                                    variableDetails.IsRestriction = det.IsRestriction;
                                    variableDetails.IsRestrictionMinRange = det.IsRestrictionMinRange;
                                    variableDetails.IsRestrictionMaxRange = det.IsRestrictionMaxRange;
                                }

                                if (det.Researcher_Comments != variableDetails.Researcher_Comments)
                                {
                                    variableDetails.Researcher_Comments = det.Researcher_Comments;
                                    isUpate = true;
                                }

                                if (isUpate)
                                {
                                    variableDetails.LastUpdateBy = userName;
                                    variableDetails.LastUpdateDate = DateTime.Now;
                                    ctx.SaveChanges();
                                }

                            }


                        }

                    }
                }
             
                ctx.Dispose();
            }


        }
    }
}