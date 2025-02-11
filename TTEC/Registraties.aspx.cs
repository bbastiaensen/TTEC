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
                               "CASE WHEN CampusZenit = 1 THEN 'Zenit' WHEN CampusBoomgaard = 1 THEN 'Boomgaard' ELSE 'Beide' END AS Campus " +
                               "FROM Registraties WHERE Goedgekeurd = 0";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvRegistraties.DataSource = dt;
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
                            // Log foutmelding indien gewenst
                        }
                    }
                }
            }
        }
    }
}