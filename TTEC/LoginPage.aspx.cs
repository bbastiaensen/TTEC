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
                Gebruikersnaam = TxtGebruikersnaam.Text
            };
            if (LoginManager.Login(login))
            {
                int SessieId = SessieManager.GetSessionID(login);
                switch (SessieId)
                {
                    case 0:
                        LblAanmeldMessage.Text = "geen rol";
                        break;

                    case 1:
                        Session["rol"] = "Bezoeker";
                        break;

                    case 2:
                        Session["rol"] = "Personeel";
                        break;

                    case 3:
                        Session["rol"] = "Bevoegd";
                        break;

                    case 4:
                        Session["rol"] = "Beheerder";
                        break;
                }
                Redirect();
            }
            else
            {
                LblAanmeldMessage.Text = "Aanmelden mislukt. Controleer uw gebruikersnaam en wachtwoord.";
            }
        }

        private void Redirect()
        {
            Response.Redirect(Session["rol"] + ".aspx");
        }
    }
}