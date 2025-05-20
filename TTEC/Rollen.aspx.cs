using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TTECLogic.Manager;

namespace TTEC
{
    public partial class Rollen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["rol"] == null || Session["rol"].ToString() != "Beheerder")
            {
                Response.Redirect("LoginPage.aspx");
            }

            if (!IsPostBack)
            {
                RollenManager.ConnectionString = ConfigurationManager.ConnectionStrings["TTEC"].ConnectionString;
                ViewState["Status"] = "nieuw";
                LaadRollen();
                btnVerwijderen.Enabled = false;
            }
        }

        private void LaadRollen()
        {
            var rollen = RollenManager.GetRollen();
            ddlRollen.DataSource = rollen;
            ddlRollen.DataTextField = "Naam";
            ddlRollen.DataValueField = "Id";
            ddlRollen.DataBind();
        }

        protected void ddlRollen_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rolId = int.Parse(ddlRollen.SelectedValue);
            if (rolId > 0)
            {
                var rol = RollenManager.GetRol(rolId);
                txtRolNaam.Text = rol.Naam;
                ViewState["Status"] = "bestaand";
                ViewState["RolId"] = rolId;
                btnVerwijderen.Enabled = true;
                lblMessage.Text = "";
            }
            else
            {
                btnVerwijderen.Enabled = false;
                txtRolNaam.Text = "";
                ViewState["Status"] = "nieuw";
            }
        }

        protected void btnBewaren_Click(object sender, EventArgs e)
        {
            string naam = txtRolNaam.Text.Trim();

            if (string.IsNullOrWhiteSpace(naam))
            {
                lblMessage.Text = "De naam van de rol mag niet leeg zijn.";
                return;
            }

            try
            {
                if (ViewState["Status"].ToString() == "nieuw")
                {
                    RollenManager.InsertRol(naam);
                    lblMessage.Text = "Rol succesvol aangemaakt.";
                }
                else
                {
                    int rolId = (int)ViewState["RolId"];
                    RollenManager.UpdateRol(rolId, naam);
                    lblMessage.Text = "Rol succesvol aangepast.";
                }

                LaadRollen();

                // Selecteer de nieuw toegevoegde of aangepaste rol opnieuw
                var rollen = RollenManager.GetRollen();
                var rol = rollen.FirstOrDefault(r => r.Naam == naam);
                if (rol != null)
                {
                    ddlRollen.SelectedValue = rol.Id.ToString();
                    ViewState["RolId"] = rol.Id;
                    ViewState["Status"] = "bestaand";
                    btnVerwijderen.Enabled = true;
                }
                else
                {
                    ViewState["Status"] = "nieuw";
                    btnVerwijderen.Enabled = false;
                }

                txtRolNaam.Text = naam;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Fout bij opslaan: " + ex.Message;
            }
        }


        protected void btnVerwijderen_Click(object sender, EventArgs e)
        {
            if (ViewState["Status"].ToString() == "nieuw")
                return;

            try
            {
                int rolId = (int)ViewState["RolId"];
                RollenManager.DeleteRol(rolId);
                lblMessage.Text = "Rol succesvol verwijderd.";

                LaadRollen();
                txtRolNaam.Text = "";
                ViewState["Status"] = "nieuw";
                btnVerwijderen.Enabled = false;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Fout bij verwijderen: " + ex.Message;
            }
        }

        protected void btnNieuw_Click(object sender, EventArgs e)
        {
            txtRolNaam.Text = "";
            ddlRollen.ClearSelection();
            ViewState["Status"] = "nieuw";
            btnVerwijderen.Enabled = false;
            lblMessage.Text = "";
        }
    }
}