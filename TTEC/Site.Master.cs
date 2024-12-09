using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using TTECLogic.Manager;

namespace TTEC
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["UserID"] = "test";
            if (!SessieManager.CanVisitPage(Session["UserID"].ToString()))
            {
                Response.Redirect("test.aspx");
            }
        }
    }
}