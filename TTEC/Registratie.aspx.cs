using System;
using System.Configuration;
using TTECLogic.Object;
using System.Net.Mail;
using System.Data.SqlClient;

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

            // Succesvolle registratie
            LblRegistratieMessage.Text = "Registratie succesvol! Controleer uw e-mail.";
            LblRegistratieMessage.Visible = true;


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
            string adminEmail = "meulenbroekjesse250@gmail.com";
            string registratieLink = "http://localhost/Registraties.aspx";

            // Mail naar de beheerder
            string adminSubject = "Nieuwe registratie";
            string adminBody = $"Er is een nieuwe registratie. Klik <a href='{registratieLink}'>hier</a> om deze te bekijken.";
            SendEmail(adminEmail, adminSubject, adminBody);

            // Mail naar de gebruiker
            string userSubject = "Registratie ontvangen";
            string userBody = "Uw registratie is ontvangen en wordt verwerkt.";
            SendEmail(userEmail, userSubject, userBody);
        }

        private void SendEmail(string to, string subject, string body)
        {
            MailMessage mail = new MailMessage
            {
                To = { to },
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            using (SmtpClient smtp = new SmtpClient("uit.telenet.be"))
            {
                smtp.Credentials = new System.Net.NetworkCredential("Meulenbroekjesse250@gmail.com", "Wachtwoord");
                smtp.Send(mail);
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
