using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace TTEC
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Reset velden bij het laden van de pagina
                ResetVelden();
                LblRegistratieMessage.Visible = false;
            }
        }


        protected void BtnRegistreer_Click(object sender, EventArgs e)
        {
            string voornaam = TxtVoornaam.Text;
            string achternaam = TxtAchternaam.Text;
            string gebruikersnaam = TxtEmail.Text;
            bool CampusZenit = CheckZenit.Checked;
            bool CampusBoomgaard = CheckBoomgaard.Checked;

            // Controleer of minstens één checkbox is aangevinkt
            if (!CampusZenit && !CampusBoomgaard)
            {
                LblRegistratieMessage.Text = "Selecteer minstens een campus.";
                LblRegistratieMessage.CssClass = "text-danger";
                LblRegistratieMessage.Visible = true;
                return;
            }

            string connectionString = "Data Source=localhost;Initial Catalog=TTEC;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
            INSERT INTO Registraties (Voornaam, Achternaam, Gebruikersnaam, CampusZenit, CampusBoomgaard)
            VALUES (@Voornaam, @Achternaam, @Gebruikersnaam, @CampusZenit, @CampusBoomgaard)";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Voornaam", voornaam);
                command.Parameters.AddWithValue("@Achternaam", achternaam);
                command.Parameters.AddWithValue("@Gebruikersnaam", gebruikersnaam);
                command.Parameters.AddWithValue("@CampusZenit", CampusZenit);
                command.Parameters.AddWithValue("@CampusBoomgaard", CampusBoomgaard);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();

                    LblRegistratieMessage.Text = "Registratie succesvol!";
                    LblRegistratieMessage.CssClass = "text-success";
                    LblRegistratieMessage.Visible = true;

                    ResetVelden();
                }
                catch (Exception ex)
                {
                    LblRegistratieMessage.Text = "Er is een fout opgetreden: " + ex.Message;
                    LblRegistratieMessage.CssClass = "text-danger";
                    LblRegistratieMessage.Visible = true;
                }
            }
        }

        // Methode om alle velden te resetten
        private void ResetVelden()
        {
            TxtVoornaam.Text = string.Empty;
            TxtAchternaam.Text = string.Empty;
            TxtEmail.Text = string.Empty;
            CheckZenit.Checked = false;
            CheckBoomgaard.Checked = false;
        }
    }
}
