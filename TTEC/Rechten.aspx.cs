using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace TTEC
{
    public partial class Rechten : System.Web.UI.Page
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["TTEC"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is a "Beheerder"
            if (Session["rol"] == null || Session["rol"] != "Beheerder")
            {
                Response.Redirect("LoginPage.aspx");
            }

            if (!IsPostBack)
            {
                LoadUsers();
                LoadRoles();
            }
        }

        private void LoadUsers()
        {
            string query = "SELECT Gebruikersnaam, (Voornaam + ' ' + Achternaam) AS FullName FROM Gebruikers";
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    ddlUsers.DataSource = reader;
                    ddlUsers.DataTextField = "FullName";
                    ddlUsers.DataValueField = "Gebruikersnaam";
                    ddlUsers.DataBind();
                }
            }
        }

        private void LoadRoles()
        {
            ddlRoles.Items.Add(new ListItem("Bezoeker", "1"));
            ddlRoles.Items.Add(new ListItem("Personeel", "2"));
            ddlRoles.Items.Add(new ListItem("Bevoegd", "3"));
            ddlRoles.Items.Add(new ListItem("Beheerder", "4"));
        }

        protected void ddlUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            string gebruikersnaam = ddlUsers.SelectedValue;
            string query = "SELECT Gebruikersnaam, Voornaam, Achternaam, RolId FROM Gebruikers WHERE Gebruikersnaam = @Gebruikersnaam";
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Gebruikersnaam", gebruikersnaam);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        lblUsernameValue.Text = reader["Gebruikersnaam"].ToString();
                        lblFirstNameValue.Text = reader["Voornaam"].ToString();
                        lblLastNameValue.Text = reader["Achternaam"].ToString();
                        ddlRoles.SelectedValue = reader["RolId"].ToString();
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string gebruikersnaam = ddlUsers.SelectedValue;
                int roleId = int.Parse(ddlRoles.SelectedValue);
                string query = "UPDATE Gebruikers SET RolId = @RolId WHERE Gebruikersnaam = @Gebruikersnaam";
                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Gebruikersnaam", gebruikersnaam);
                    cmd.Parameters.AddWithValue("@RolId", roleId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
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