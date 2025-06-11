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
    public partial class Organisaties : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["rol"] == null || Session["rol"].ToString() != "Beheerder")
            {
                Response.Redirect("LoginPage.aspx");
            }
            if (!IsPostBack)
            {
                OrganisatieManager.ConnectionString = ConfigurationManager.ConnectionStrings["TTEC"].ConnectionString;
                ViewState["Status"] = "nieuw";
                LaadOrganisaties();
                btnVerwijderen.Enabled = false;
            }
        }

        private void LaadOrganisaties()
        {
            var Organisaties = OrganisatieManager.GetOrganisaties();
            ddlOrganisaties.DataSource = Organisaties;
            ddlOrganisaties.DataTextField = "Naam";
            ddlOrganisaties.DataValueField = "Id";
            ddlOrganisaties.DataBind();
        }

        protected void ddlOrganisaties_SelectedIndexChanged(object sender, EventArgs e)
        {
            int organisatieId = int.Parse(ddlOrganisaties.SelectedValue);
            if (organisatieId > 0)
            {
                var organisatie = OrganisatieManager.GetOrganisatie(organisatieId);
                txtOrganisatieNaam.Text = organisatie.Naam;
                ViewState["Status"] = "bestaand";
                ViewState["OrganisatieId"] = organisatieId;
                btnVerwijderen.Enabled = true;
                lblMessage.Text = "";
            }
            else
            {
                btnVerwijderen.Enabled = false;
                txtOrganisatieNaam.Text = "";
                ViewState["Status"] = "nieuw";
            }
        }

        protected void btnBewaren_Click(object sender, EventArgs e)
        {
            string naam = txtOrganisatieNaam.Text.Trim();

            if (string.IsNullOrWhiteSpace(naam))
            {
                lblMessage.Text = "De naam van de Organisatie mag niet leeg zijn.";
                return;
            }

            try
            {
                if (ViewState["Status"].ToString() == "nieuw")
                {
                    OrganisatieManager.InsertOrganisatie(naam);
                    lblMessage.Text = "Organisatie succesvol aangemaakt.";
                }
                else
                {
                    int OrganisatieId = (int)ViewState["OrganisatieId"];
                    OrganisatieManager.UpdateOrganisatie(OrganisatieId, naam);
                    lblMessage.Text = "Organisatie succesvol aangepast.";
                }

                LaadOrganisaties();

                // Selecteer de nieuw toegevoegde of aangepaste organisatie opnieuw
                var organisaties = OrganisatieManager.GetOrganisaties();
                var organisatie = organisaties.FirstOrDefault(o => o.Naam == naam);
                if (organisatie != null)
                {
                    ddlOrganisaties.SelectedValue = organisatie.Id.ToString();
                    ViewState["OrganisatieId"] = organisatie.Id;
                    ViewState["Status"] = "bestaand";
                    btnVerwijderen.Enabled = true;
                }
                else
                {
                    ViewState["Status"] = "nieuw";
                    btnVerwijderen.Enabled = false;
                }

                txtOrganisatieNaam.Text = naam;
            }
            catch
            {
                lblMessage.Text = "Fout bij het opslaan van de organisatie.";
            }
        }

        protected void btnVerwijderen_Click(object sender, EventArgs e)
        {
            if (ViewState["Status"].ToString() == "nieuw")
                return;

            try
            {
                int OrganisatieId = (int)ViewState["OrganisatieId"];
                OrganisatieManager.DeleteOrganisatie(OrganisatieId);
                lblMessage.Text = "Organisatie succesvol verwijderd.";

                LaadOrganisaties();
                txtOrganisatieNaam.Text = "";
                ViewState["Status"] = "nieuw";
                btnVerwijderen.Enabled = false;
            }
            catch
            {
                lblMessage.Text = "Fout bij het verwijderen van de organisatie. Er is mogelijk een afhankelijkheid met gebruikers.";
            }
        }

        protected void btnNieuw_Click(object sender, EventArgs e)
        {
            txtOrganisatieNaam.Text = "";
            ddlOrganisaties.ClearSelection();
            ViewState["Status"] = "nieuw";
            btnVerwijderen.Enabled = false;
            lblMessage.Text = "";
        }
    }
}