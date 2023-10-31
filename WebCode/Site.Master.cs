using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DaSH_Researcher_Portal
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();



            var cce = HttpContext.Current.Request.Cookies["Dare_UserAuthTicket"];
            if (cce == null)
            {

                Response.Redirect("~/Account/Login.aspx");

            }

        }
    }
}