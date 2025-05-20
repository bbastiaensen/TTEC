using System;

namespace TTEC
{
    public partial class Bevoegd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["rol"] == null || Session["rol"] != "Bevoegd")
            {
                Response.Redirect("LoginPage.aspx");
            }
        }
    }
}