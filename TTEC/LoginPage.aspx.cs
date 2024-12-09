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
        }

        protected void SubmitLogin_Click(object sender, EventArgs e)
        {
            LoginManager.ConnectionString = ConfigurationManager.ConnectionStrings["TTEC"].ConnectionString;
            login login = new login
            {
                Wachtwoord = TxtPassword.Text,
                Gebruikersnaam = TxtUsername.Text
            };
            if (LoginManager.IsLoginValid(login))
            {
                LblMessage.Text = login.FoutBoodschap;
                //Redirect/start sessie
            }
            else
            {
                LblMessage.Text = login.FoutBoodschap;
            }


        }
    }
}