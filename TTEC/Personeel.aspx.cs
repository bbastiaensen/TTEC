using System;

namespace TTEC
{
    public partial class Personeel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["rol"] == null || Session["rol"] != "Personeel")
            {
                Response.Redirect("LoginPage.aspx");
            }
        }
    }
}