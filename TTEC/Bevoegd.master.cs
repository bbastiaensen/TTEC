using System;

namespace TTEC
{
    public partial class NestedMasterPage2 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["rol"] != "Bevoegd")
            {
                // Gebruiker heeft geen geldige sessie, doorsturen naar loginpagina
                Response.Redirect("LoginPage.aspx");
            }
        }
    }
}