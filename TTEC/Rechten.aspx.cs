using System;
using System.Configuration;
using System.Web.UI.WebControls;
using TTECLogic.Manager;

namespace TTEC
{
    public partial class Rechten : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["rol"] == null || Session["rol"] != "Beheerder")
            {
                Response.Redirect("LoginPage.aspx");
            }

            if (!IsPostBack)
            {
                RechtenManager.ConnectionString = ConfigurationManager.ConnectionStrings["TTEC"].ConnectionString;
                RollenManager.ConnectionString = ConfigurationManager.ConnectionStrings["TTEC"].ConnectionString;

                LoadUsers();
                LoadRoles();
            }
        }

        private void LoadUsers()
        {
            RechtenManager.LoadUsers(ddlUsers);
        }

        private void LoadRoles()
        {
            var rollen = RollenManager.GetRollen();
            ddlRollen.DataSource = rollen;
            ddlRollen.DataTextField = "Naam";
            ddlRollen.DataValueField = "Id";
            ddlRollen.DataBind();
        }

        protected void ddlUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            string gebruikersnaam = ddlUsers.SelectedValue;
            RechtenManager.LoadUserDetails(gebruikersnaam, out string voornaam, out string achternaam, out int rolId);

            lblGebruikersnaamValue.Text = gebruikersnaam;
            lblVoornaamValue.Text = voornaam;
            lblAchternaamValue.Text = achternaam;
            ddlRollen.SelectedValue = rolId.ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string gebruikersnaam = ddlUsers.SelectedValue;
                int rolId = int.Parse(ddlRollen.SelectedValue);
                RechtenManager.UpdateUserRole(gebruikersnaam, rolId);

                lblMessage.Text = "Rol succesvol bijgewerkt.";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Er is een fout opgetreden: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}