using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TTECLogic.Object;
using TTECLogic.Manager;
using System.Configuration;
using System.Security.Policy;
using System.Threading;

namespace TTEC
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            if (Session["rol"] != null)
            {
                Response.Redirect(Session["rol"].ToString() + ".aspx");
            }
        }

        protected void SubmitLogin_Click(object sender, EventArgs e)
        {
            LoginManager.ConnectionString = ConfigurationManager.ConnectionStrings["TTEC"].ConnectionString;
            login login = new login
            {
                Wachtwoord = TxtPassword.Text,
                Gebruikersnaam = TxtUsername.Text
            };
            if (LoginManager.Login(login))
            {
                int SessieId = SessieManager.GetSessionID(login);
                switch (SessieId) {
                    case 0:
                        LblMessage.Text = "geen rol";
                        break;
                    case 1:
                        Session["rol"] = "Personeel";
                        break;
                    case 2:
                        Session["rol"] = "Bevoegd";
                        break;

                }
                Redirect();
            }

        }

        private void Redirect()
        {
            HttpCookie loginCookie = new HttpCookie("LoginCookie");
            loginCookie.Values["username"] = TxtUsername.Text;
            loginCookie.Expires = DateTime.Now.AddDays(30); // Cookie vervalt na 30 dagen
            Response.Cookies.Add(loginCookie);
            Response.Redirect(Session["rol"] + ".aspx", true);
        }
    }
}