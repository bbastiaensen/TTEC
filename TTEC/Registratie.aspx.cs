using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using TTECLogic.Object;

namespace TTEC
{
    public partial class WebForm1 : System.Web.UI.Page
    {
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
                CampusBoomgaard = campusBoomgaard,
                RolId = int.Parse(HiddenRolId.Value)
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

        protected void Page_load(object sender, EventArgs e)
        {
            RegistratieManager.ConnectionString = ConfigurationManager.ConnectionStrings["TTEC"].ConnectionString;

            if (!IsPostBack)
            {
                // Controleer of de gebruiker écht op Registratie.aspx hoort te zijn
                if (Request.UrlReferrer != null && Request.UrlReferrer.AbsolutePath.Contains("Registratie.aspx"))
                {
                    ResetVelden();
                    LblRegistratieMessage.Visible = false;
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

        private void SendEmails(string userEmail)
        {
            try
            {
                string adminEmail = "meulenbroekjesse250@gmail.com"; // Vervang door het e-mailadres van de beheerder
                string registratieUrl = "https://localhost:44365/Registraties.aspx";

                // E-mail naar de geregistreerde gebruiker
                MailMessage userMail = new MailMessage();
                userMail.From = new MailAddress("meulenbroekjesse250@gmail.com");
                userMail.To.Add(userEmail);
                userMail.Subject = "Registratie ontvangen - Talentenschool Turnhout";
                userMail.Body = $@"
                    <p>Beste {TxtVoornaam.Text} {TxtAchternaam.Text},</p>
                    <p>Bedankt voor je registratie bij Talentenschool Turnhout.</p>
                    <p>Wij hebben je gegevens ontvangen en zullen deze zo snel mogelijk verwerken.</p>
                    <p>Met vriendelijke groeten,</p>
                    <p><b>Talentenschool Turnhout</b></p>";
                userMail.IsBodyHtml = true;

                // E-mail naar de beheerder
                MailMessage adminMail = new MailMessage();
                adminMail.From = new MailAddress("meulenbroekjesse250@gmail.com");
                adminMail.To.Add(adminEmail);
                adminMail.Subject = "Nieuwe registratie ontvangen";
                adminMail.Body = $@"
                    <p>Beste beheerder,</p>
                    <p>Er is een nieuwe registratie binnengekomen:</p>
                    <ul>
                        <li><b>Naam:</b> {TxtVoornaam.Text} {TxtAchternaam.Text}</li>
                        <li><b>Email:</b> {userEmail}</li>
                        <li><b>Campus Zenit:</b> {(CheckZenit.Checked ? "Ja" : "Nee")}</li>
                        <li><b>Campus Boomgaard:</b> {(CheckBoomgaard.Checked ? "Ja" : "Nee")}</li>
                    </ul>
                    <p>Bekijk de registratie en keur deze goed via de volgende link:</p>
                    <p><a href='{registratieUrl}'>Registraties bekijken</a></p>
                    <p>Met vriendelijke groeten,</p>
                    <p><b>Talentenschool Turnhout</b></p>";
                adminMail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("meulenbroekjesse250@gmail.com", "xapg ghzb dvwn tvud\r\n"); // Vervang met beheerder credentials

                // E-mails verzenden
                smtp.Send(userMail);
                smtp.Send(adminMail);

                LblRegistratieMessage.ForeColor = Color.Green;
                LblRegistratieMessage.Text = "Registratie succesvol! Controleer uw e-mail.";
            }
            catch (Exception ex)
            {
                LblRegistratieMessage.ForeColor = Color.Red;
                LblRegistratieMessage.Text = "Er is een fout opgetreden: " + ex.Message;
            }
        }
    }
}