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

        }

        protected void BtnRegistreer_Click(object sender, EventArgs e)
        {
            string voornaam = TxtVoornaam.Text;
            string achternaam = TxtAchternaam.Text; 
            string gebruikersnaam = TxtEmail.Text;
            bool CampusZenit = CheckZenit.Checked;
            bool CampusBoomgaard = CheckBoomgaard.Checked;

            string connectionString = "Data Source=localhost;Initial Catalog=TTEC;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Query om gegevens in te voegen
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
                    hifRegistratie.Value = "Registratie succesvol!";
                }
                catch (Exception ex)
                {
                    hifRegistratie.Value = "Er is een fout opgetreden: " + ex.Message;
                }

            }
        }
    }
}