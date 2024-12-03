using System;
using System.Configuration;
using TTECLogic.Object;
using System.Net.Mail;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace TTEC
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_load(object sender, EventArgs e)
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
            bool campusZenit = CheckZenit.Checked;
            bool campusBoomgaard = CheckBoomgaard.Checked;            
            string gebruikersnaam = TxtEmail.Text;

            if (!campusZenit && !campusBoomgaard)
            {
                LblRegistratieMessage.Text = "Selecteer minstens een campus.";
                LblRegistratieMessage.Visible = true;
                return;
            }

            if (RegistratieManager.IsEmailInUse(gebruikersnaam))
            {
                LblRegistratieMessage.Text = "Het e-mailadres is al geregistreerd. Gebruik een ander e-mailadres.";
                LblRegistratieMessage.Visible = true;
                return;
            }


            Registratie registratie = new Registratie
            {
                Voornaam = TxtVoornaam.Text,
                Achternaam = TxtAchternaam.Text,
                Gebruikersnaam = gebruikersnaam,
                CampusZenit = campusZenit,
                CampusBoomgaard = campusBoomgaard
            };

            try
            {
                RegistratieManager.SaveRegistratie(registratie);
                SendEmails(gebruikersnaam);
                LblRegistratieMessage.Text = "Registratie succesvol! Controleer uw e-mail.";
                LblRegistratieMessage.Visible = true;
            }
            catch (SqlException sqlEx)
            {
                LblRegistratieMessage.Text = "Databasefout: " + sqlEx.Message;
                LblRegistratieMessage.Visible = true;
            }
            catch (Exception ex)
            {
                LblRegistratieMessage.Text = "Er is een onverwachte fout opgetreden: " + ex.Message;
                LblRegistratieMessage.Visible = true;
            }

        }

        private void SendEmails(string userEmail)
        {
            string registratieLink = "http://localhost/Registraties.aspx";
            MailMessage beheerder = new MailMessage();
            beheerder.From = new MailAddress(ConfigurationManager.AppSettings["fromAdress"].ToString());
            beheerder.To.Add(TxtEmail.Text);
            beheerder.Subject = "Nieuwe TTEC registratie.";
            beheerder.Body = $"Er is een nieuwe registratie. Klik <a href='{registratieLink}'>hier</a> om deze te bekijken.";

            MailMessage gebruiker = new MailMessage();
            gebruiker.From = new MailAddress(ConfigurationManager.AppSettings[TxtEmail.Text].ToString());
            gebruiker.To.Add(TxtEmail.Text);
            gebruiker.Subject = "TTEC registratie behandeling";
            gebruiker.Body = "Uw registratie wordt in behandeling genomen door de beheerder.";

            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["smptServer"].ToString();

            try
            {
                smtp.Send(gebruiker);
                smtp.Send(beheerder);
            }
            catch (Exception ex) 
            {
                LblRegistratieMessage.Text = "Er is iets misgelopen tijdens het versturen van de mail" + ex.Message;
                return;
            }
            LblRegistratieMessage.Text = "Uw mail is succesvol doorgestuurd.";

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
