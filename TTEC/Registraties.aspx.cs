using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Web.UI.WebControls;

namespace TTEC
{
    public partial class Registraties : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["TTCn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LaadRegistraties();
            }
        }

        private void LaadRegistraties()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT ID, Voornaam, Achternaam, Gebruikersnaam, " +
                                       "CASE WHEN CampusZenit = 1 AND CampusBoomgaard = 1 THEN 'Beide' " +
                                       "WHEN CampusZenit = 1 THEN 'Zenit' " +
                                       "WHEN CampusBoomgaard = 1 THEN 'Boomgaard' " +
                                       "ELSE 'Geen' END AS Campus " +
                                       "FROM Registraties WHERE Goedgekeurd = 0";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataAdapter DataA = new SqlDataAdapter(cmd);
                    DataTable DataT = new DataTable();
                    DataA.Fill(DataT);
                    gvRegistraties.DataSource = DataT;
                    gvRegistraties.DataBind();
                }
            }
        }

        protected void BtnGoedkeuren_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int userId = Convert.ToInt32(btn.CommandArgument);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Registraties SET Goedgekeurd = 1 WHERE ID = @ID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", userId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                // E-mail sturen naar gebruiker
                VerstuurBevestigingsEmail(userId);
            }

            // Pagina opnieuw laden
            LaadRegistraties();
        }

        protected void BtnAfkeuren_Click(object sender, EventArgs e)
        {
            string connectionString = RegistratieManager.ConnectionString;

            Button btn = (Button)sender;
            int registratieId = int.Parse(btn.CommandArgument);

            // Fetch the email address of the user
            string email = GetEmailById(registratieId);

            if (!string.IsNullOrEmpty(email))
            {
                SendRejectionEmail(email);

                DeleteRegistration(registratieId);

                LblRegistratieMessage.Text = "Registratie is afgekeurd en verwijderd.";
                LblRegistratieMessage.Visible = true;
                gvRegistraties.DataBind();
            }
        }

        private string GetEmailById(int id)
        {
            string email = string.Empty;
            string connectionString = RegistratieManager.ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Gebruikersnaam FROM Registraties WHERE ID = @ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", id);

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

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("meulenbroekjesse250@gmail.com", "xapg ghzb dvwn tvud\r\n");
            }
        }

        private void VerstuurBevestigingsEmail(int userId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Gebruikersnaam, Voornaam, Achternaam FROM Registraties WHERE ID = @ID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", userId);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string email = reader["Gebruikersnaam"].ToString();
                        string voornaam = reader["Voornaam"].ToString();
                        string achternaam = reader["Achternaam"].ToString();

                        try
                        {
                            MailMessage mm = new MailMessage();
                            mm.From = new MailAddress("meulenbroekjesse250@gmail.com");
                            mm.To.Add(email);
                            mm.Subject = "Registratie goedgekeurd - Talentenschool Turnhout";
                            mm.Body = $@"
                                <p>Beste {voornaam} {achternaam},</p>
                                <p>Goed nieuws! Jouw registratie is goedgekeurd.</p>
                                <p>Je kunt nu inloggen via onze website.</p>
                                <p>Met vriendelijke groeten,</p>
                                <p><b>Talentenschool Turnhout</b></p>";
                            mm.IsBodyHtml = true;

                            SmtpClient smtp = new SmtpClient();
                            smtp.Host = "smtp.gmail.com";
                            smtp.Port = 587;
                            smtp.EnableSsl = true;
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = new NetworkCredential("meulenbroekjesse250@gmail.com", "xapg ghzb dvwn tvud\r\n");

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

        private void DeleteRegistration(int id)
        {
            string connectionString = "TTCn";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Registraties WHERE ID = @ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}