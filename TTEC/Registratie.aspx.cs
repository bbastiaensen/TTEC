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
            beheerder.From = new MailAddress(ConfigurationManager.AppSettings["fromAddress"]);
            beheerder.To.Add(ConfigurationManager.AppSettings["adminEmail"]);
            beheerder.Subject = "Nieuwe TTEC registratie.";
            beheerder.Body = $"Er is een nieuwe registratie. Klik <a href='{registratieLink}'>hier</a> om deze te bekijken.";

            MailMessage gebruiker = new MailMessage();
            gebruiker.From = new MailAddress(ConfigurationManager.AppSettings["fromAddress"]);
            gebruiker.To.Add(userEmail);
            gebruiker.Subject = "TTEC registratie behandeling";
            gebruiker.Body = "Uw registratie wordt in behandeling genomen door de beheerder.";

            SmtpClient smtp = new SmtpClient();
            string smtpServer = ConfigurationManager.AppSettings["smtpServer"];
            if (string.IsNullOrEmpty(smtpServer))
            {
                LblRegistratieMessage.Text = "SMTP server is not configured.";
                return;
            }
            smtp.Host = smtpServer;
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential(
                ConfigurationManager.AppSettings["smtpUsername"],
                ConfigurationManager.AppSettings["smtpPassword"]
            );

            try
            {
                smtp.Send(gebruiker);
                smtp.Send(beheerder);
            }
            catch (Exception ex)
            {
                LblRegistratieMessage.Text = "Er is iets misgelopen tijdens het versturen van de mail: " + ex.Message;
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
