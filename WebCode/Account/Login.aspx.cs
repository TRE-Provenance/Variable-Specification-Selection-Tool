using DaSH_Researcher_Portal.EntityFramework;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DaSH_Researcher_Portal.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //HttpResponse.RemoveOutputCacheItem("/ResForms/defualt.aspx");
            if ((this.Request.Url.Authority).IndexOf("localhost") == -1 && (this.Request.Url.Authority).IndexOf("9090") == -1)
            {
                if (this.Request.Url.Scheme.Equals(Uri.UriSchemeHttp))
                {
                    Response.Redirect(Uri.UriSchemeHttps + Uri.SchemeDelimiter + this.Request.Url.Authority + this.Request.Url.PathAndQuery);
                }
            }
        }

        private HttpRequestBase _request;
        private HttpResponseBase _response;

        protected void btn_addDemoUser_Click(object sender, EventArgs e)
        {
            using (var ctx = new DareToDaSHEntities2())// DareDashModel())
            {
                var checkLogin =
                     (from uus in ctx.Users_Unsecure

                      where uus.UserName == UserName.Text && uus.Password == Password.Text
                      select uus).FirstOrDefault();

                if (checkLogin == null)
                {
                    var nUser = new Users_Unsecure();
                    nUser.Password = Password.Text;
                    nUser.UserName = UserName.Text;
                    ctx.Users_Unsecure.Add(nUser);

                    ctx.SaveChanges();
                }
                    LoginButton_Click(sender, e);
            }
        }


        protected void LoginButton_Click(object sender, EventArgs e)
        {
            using (var ctx = new DareToDaSHEntities2())// DareDashModel())
            {
                var checkLogin =
             (from uus in ctx.Users_Unsecure

              where uus.UserName == UserName.Text && uus.Password == Password.Text  
              select uus).FirstOrDefault();

                if (checkLogin != null)
                {
                    DeleteCookie(HttpContext.Current.Request.Cookies["Dare_UserAuthTicket"]);

                    FormsAuthenticationTicket Dare_UserAuthTicket =
                       new FormsAuthenticationTicket(1, UserName.Text, DateTime.Now,
                       DateTime.MaxValue, true, Password.Text, FormsAuthentication.FormsCookiePath);
                    string encUserAuthTicket = FormsAuthentication.Encrypt(Dare_UserAuthTicket);
                    HttpCookie userAuthCookie = new HttpCookie
                        (FormsAuthentication.FormsCookieName, encUserAuthTicket);
                    //if (Dare_UserAuthTicket.IsPersistent) userAuthCookie.Expires =
                    //        Dare_UserAuthTicket.Expiration;
                    userAuthCookie.Path = FormsAuthentication.FormsCookiePath;
                    userAuthCookie.Value = UserName.Text;
                    userAuthCookie.Name = "Dare_UserAuthTicket";
                    userAuthCookie.Expires = DateTime.Now.AddMinutes(60);
                    Response.Cookies.Add(userAuthCookie);

                    Response.Redirect("~/default.aspx");
                }
            }
        }

        public void DeleteCookie(HttpCookie httpCookie)
        {
            if (httpCookie != null)
            {
                httpCookie.Value = null;
                httpCookie.Expires = DateTime.Now.AddMinutes(-20);
                HttpContext.Current.Request.Cookies.Add(httpCookie);
            }
        }

    }
}