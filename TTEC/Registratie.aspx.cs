using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TTECLogic;
using TTECLogic.Manager;

namespace TTEC
{
    public partial class Registratie : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegistratieManager.ConnectionString = ConfigurationManager.ConnectionStrings["TTCn"].ConnectionString;

            if (!IsPostBack)
            {
                // Reset velden bij het laden van de pagina
                ResetVelden();
                LblRegistratieMessage.Visible = false;
            }
        }


        protected void BtnRegistreer_Click(object sender, EventArgs e)
        {
            // Controleer of minstens één checkbox is aangevinkt
            bool CampusZenit = CheckZenit.Checked;
            bool CampusBoomgaard = CheckBoomgaard.Checked;

            if (!CampusZenit && !CampusBoomgaard)
            {
                LblRegistratieMessage.Text = "Selecteer minstens een campus.";
                LblRegistratieMessage.CssClass = "text-danger";
                LblRegistratieMessage.Visible = true;
                return;
            }

            //Vul een registratie object met data
            Registratie registratie = new Registratie();
            registratie.Voornaam = TxtVoornaam.Text;
            registratie.Achternaam = TxtAchternaam.Text;
            registratie.Gebruikersnaam = TxtEmail.Text;
            registratie.CampusBoomgaard = CheckBoomgaard.Checked;
            registratie.CampusZenit = CheckZenit.Checked;

            //Registratie gaan bewaren.
            try
            {
                RegistratieManager.SaveRegistratie(registratie);
            }
            catch (Exception)
            {


            }



            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    try
            //    {
            //        connection.Open();

            //        // Controleer of het e-mailadres al bestaat
            //        string checkQuery = "SELECT COUNT(*) FROM Registraties WHERE Gebruikersnaam = @Gebruikersnaam";
            //        SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
            //        checkCommand.Parameters.AddWithValue("@Gebruikersnaam", gebruikersnaam);

            //        int emailExists = (int)checkCommand.ExecuteScalar();
            //        if (emailExists > 0)
            //        {
            //            LblRegistratieMessage.Text = "Het e-mailadres is al geregistreerd.";
            //            LblRegistratieMessage.CssClass = "text-danger";
            //            LblRegistratieMessage.Visible = true;
            //            return;
            //        }

            //        string query = @"
            //    INSERT INTO Registraties (Voornaam, Achternaam, Gebruikersnaam, CampusZenit, CampusBoomgaard)
            //    VALUES (@Voornaam, @Achternaam, @Gebruikersnaam, @CampusZenit, @CampusBoomgaard)";
            //        SqlCommand command = new SqlCommand(query, connection);

            //        command.Parameters.AddWithValue("@Voornaam", voornaam);
            //        command.Parameters.AddWithValue("@Achternaam", achternaam);
            //        command.Parameters.AddWithValue("@Gebruikersnaam", gebruikersnaam);
            //        command.Parameters.AddWithValue("@CampusZenit", CampusZenit);
            //        command.Parameters.AddWithValue("@CampusBoomgaard", CampusBoomgaard);

            //        command.ExecuteNonQuery();

            //        LblRegistratieMessage.Text = "Registratie succesvol!";
            //        LblRegistratieMessage.CssClass = "text-success";
            //        LblRegistratieMessage.Visible = true;

            //        ResetVelden();
            //    }
            //    catch (Exception ex)
            //    {
            //        LblRegistratieMessage.Text = "Er is een fout opgetreden: " + ex.Message;
            //        LblRegistratieMessage.CssClass = "text-danger";
            //        LblRegistratieMessage.Visible = true;
            //    }
            //}
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
