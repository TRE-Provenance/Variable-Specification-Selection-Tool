using DaSH_Researcher_Portal.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DaSH_Researcher_Portal.DataActions;
using System.Web.Security;
using System.Xml.Linq;

namespace DaSH_Researcher_Portal.ResForms
{
    public partial class ResProjects : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            using (var ctx = new DareToDaSHEntities2())// DareDashModel())
            {

                var cce = HttpContext.Current.Request.Cookies["Dare_UserAuthTicket"];


                var proJs = ProjectActions.getProjectByUserName(cce.Value);


                //MembershipUser mu = Membership.GetUser(Page.User.Identity.Name);
                //var proJs = ProjectActions.getProjectByUserName(mu.UserName);



                gvProjects.DataSource = proJs.ToList();
                gvProjects.DataBind();



            }

        }
    }
}