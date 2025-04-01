using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace TTEC
{
    public partial class Registraties : System.Web.UI.Page
    {

        private readonly string connectionString = ConfigurationManager.ConnectionStrings["TTEC"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["rol"] != "Bevoegd" && Session["rol"] != "Beheerder")
            {
                // Gebruiker heeft geen geldige sessie, doorsturen naar loginpagina
                Response.Redirect("LoginPage.aspx");
            }

            if (!IsPostBack)
            {
                LaadRegistraties();
            }
        }

        private void LaadRegistraties()
        {
            string query = @"
        SELECT Voornaam, Achternaam, Gebruikersnaam, CampusZenit, CampusBoomgaard
        FROM Registraties
        ORDER BY Achternaam, Voornaam";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    var registraties = new List<dynamic>();
                    while (reader.Read())
                    {
                        string campus = "Geen";
                        if (Convert.ToBoolean(reader["CampusZenit"]) && Convert.ToBoolean(reader["CampusBoomgaard"]))
                        {
                            campus = "Beide";
                        }
                        else if (Convert.ToBoolean(reader["CampusZenit"]))
                        {
                            campus = "Zenit";
                        }
                        else if (Convert.ToBoolean(reader["CampusBoomgaard"]))
                        {
                            campus = "Boomgaard";
                        }

                        registraties.Add(new
                        {
                            Voornaam = reader["Voornaam"].ToString(),
                            Achternaam = reader["Achternaam"].ToString(),
                            Gebruikersnaam = reader["Gebruikersnaam"].ToString(),
                            Campus = campus
                        });
                    }
                    rptRegistraties.DataSource = registraties;
                    rptRegistraties.DataBind();
                }
            }
        }

        protected void BtnGoedkeuren_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string gebruikersnaam = btn.CommandArgument;

            using (SqlConnection con = new SqlConnection(connectionString))
            {

                string query = "UPDATE Registraties SET RolId = 1 WHERE Gebruikersnaam = @Gebruikersnaam";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Gebruikersnaam", gebruikersnaam);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                MaakGebruikersAccount(gebruikersnaam);

                VerstuurBevestigingsEmail(gebruikersnaam);

                DeleteRegistration(gebruikersnaam);
            }

            LaadRegistraties();
        }

        protected void BtnAfkeuren_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string gebruikersnaam = btn.CommandArgument;

            string email = GetEmailByGebruikersnaam(gebruikersnaam);

            if (!string.IsNullOrEmpty(email))
            {
                SendRejectionEmail(email);

                DeleteRegistration(gebruikersnaam);

                LblRegistratieMessage.Text = "Registratie is afgekeurd en verwijderd.";
                LblRegistratieMessage.Visible = true;
                LaadRegistraties();
            }
        }

        private string GetEmailByGebruikersnaam(string gebruikersnaam)
        {
            string email = string.Empty;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Gebruikersnaam FROM Registraties WHERE Gebruikersnaam = @Gebruikersnaam";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Gebruikersnaam", gebruikersnaam);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    email = reader["Gebruikersnaam"].ToString();
                }
            }

            return email;
        }

        private void SendRejectionEmail(string email)
        {
            string fromAddress = "meulenbroekjesse250@gmail.com";
            string subject = "Registratie Afgekeurd";
            string body = "Uw registratie is afgekeurd.";

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(fromAddress);
                mail.To.Add(email);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("meulenbroekjesse250@gmail.com", "xapg ghzb dvwn tvud\r\n")
                };

                smtp.Send(mail);
            }
        }

        private void VerstuurBevestigingsEmail(string gebruikersnaam)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Gebruikersnaam, Voornaam, Achternaam FROM Registraties WHERE Gebruikersnaam = @Gebruikersnaam";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Gebruikersnaam", gebruikersnaam);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string email = reader["Gebruikersnaam"].ToString();
                        string voornaam = reader["Voornaam"].ToString();
                        string achternaam = reader["Achternaam"].ToString();
                        string wachtwoord = GenerateRandomPassword();

                        try
                        {
                            MailMessage mm = new MailMessage();
                            mm.From = new MailAddress("meulenbroekjesse250@gmail.com");
                            mm.To.Add(email);
                            mm.Subject = "Registratie goedgekeurd - Talentenschool Turnhout";
                            mm.Body = $@"
                            <p>Beste {voornaam} {achternaam},</p>
                            <p>Goed nieuws! Jouw registratie is goedgekeurd.</p>
                            <p>Je kunt nu inloggen met de volgende gegevens:</p>
                            <p>Gebruikersnaam: {email}</p>
                            <p>Wachtwoord: {wachtwoord}</p>
                            <p>Met vriendelijke groeten,</p>
                            <p><b>Talentenschool Turnhout</b></p>";
                            mm.IsBodyHtml = true;

                            SmtpClient smtp = new SmtpClient
                            {
                                Host = "smtp.gmail.com",
                                Port = 587,
                                EnableSsl = true,
                                UseDefaultCredentials = false,
                                Credentials = new NetworkCredential("meulenbroekjesse250@gmail.com", "xapg ghzb dvwn tvud\r\n")
                            };

                            smtp.Send(mm);
                        }
                        catch (Exception ex)
                        {
                            LblRegistratieMessage.ForeColor = Color.Red;
                            LblRegistratieMessage.Text = "Er is een fout opgetreden: " + ex.Message;
                        }
                    }
                }
            }
        }

        private void DeleteRegistration(string gebruikersnaam)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Registraties WHERE Gebruikersnaam = @Gebruikersnaam";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Gebruikersnaam", gebruikersnaam);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void MaakGebruikersAccount(string gebruikersnaam)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Gebruikersnaam, Voornaam, Achternaam, CampusZenit, CampusBoomgaard FROM Registraties WHERE Gebruikersnaam = @Gebruikersnaam";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Gebruikersnaam", gebruikersnaam);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string email = reader["Gebruikersnaam"].ToString();
                        string voornaam = reader["Voornaam"].ToString();
                        string achternaam = reader["Achternaam"].ToString();
                        bool campusZenit = Convert.ToBoolean(reader["CampusZenit"]);
                        bool campusBoomgaard = Convert.ToBoolean(reader["CampusBoomgaard"]);
                        string wachtwoord = GenerateRandomPassword();
                        string hashedWachtwoord = HashPassword(wachtwoord);

                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            string insertQuery = "INSERT INTO Gebruikers (Gebruikersnaam, Wachtwoord, Voornaam, Achternaam, CampusZenit, CampusBoomgaard, RolId) VALUES (@Gebruikersnaam, @Wachtwoord, @Voornaam, @Achternaam, @CampusZenit, @CampusBoomgaard, @RolId)";
                            using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                            {
                                insertCmd.Parameters.AddWithValue("@Gebruikersnaam", email);
                                insertCmd.Parameters.AddWithValue("@Wachtwoord", hashedWachtwoord);
                                insertCmd.Parameters.AddWithValue("@Voornaam", voornaam);
                                insertCmd.Parameters.AddWithValue("@Achternaam", achternaam);
                                insertCmd.Parameters.AddWithValue("@CampusZenit", campusZenit);
                                insertCmd.Parameters.AddWithValue("@CampusBoomgaard", campusBoomgaard);
                                insertCmd.Parameters.AddWithValue("@RolId", 1);
                                conn.Open();
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
        }

        private string GenerateRandomPassword()
        {
            const int length = 12;
            const string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            StringBuilder res = new StringBuilder();
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                for (int i = 0; i < length; i++)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    res.Append(validChars[(int)(num % (uint)validChars.Length)]);
                }
            }
            string password = res.ToString();
            return HashPassword(password);
        }

        public static string HashPassword(string pass)
        {
            using (SHA512Managed sha512 = new SHA512Managed())
            {
                byte[] hashedValue = sha512.ComputeHash(Encoding.UTF8.GetBytes(pass));
                string hashpass = BitConverter.ToString(hashedValue).Replace("-", string.Empty);
                return hashpass;
            }
        }
    }
}