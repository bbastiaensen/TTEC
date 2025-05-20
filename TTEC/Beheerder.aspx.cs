using System;

namespace TTEC
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["rol"] == null || Session["rol"] != "Beheerder")
            {
                Response.Redirect("LoginPage.aspx");
            }
        }
    }
}