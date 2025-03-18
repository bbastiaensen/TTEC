using System;
using System.Web;

namespace TTEC
{
    public partial class NestedMasterPage3 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["rol"] == null || Session["rol"].ToString() != "Personeel")
            {
                Response.Redirect("loginpage.aspx");
            }
        }
    }
}