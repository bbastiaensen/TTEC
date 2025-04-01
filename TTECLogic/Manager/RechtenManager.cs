using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace TTECLogic.Manager
{
    public static class RechtenManager
    {
        public static string ConnectionString { get; set; }

        public static void LoadUsers(DropDownList ddlUsers)
        {
            string query = "SELECT Gebruikersnaam, (Voornaam + ' ' + Achternaam) AS FullName FROM Gebruikers";
            using (SqlConnection con = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    ddlUsers.DataSource = reader;
                    ddlUsers.DataTextField = "FullName";
                    ddlUsers.DataValueField = "Gebruikersnaam";
                    ddlUsers.DataBind();
                }
            }
        }

        public static void LoadUserDetails(string gebruikersnaam, out string voornaam, out string achternaam, out int rolId)
        {
            string query = "SELECT Voornaam, Achternaam, RolId FROM Gebruikers WHERE Gebruikersnaam = @Gebruikersnaam";
            using (SqlConnection con = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Gebruikersnaam", gebruikersnaam);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        voornaam = reader["Voornaam"].ToString();
                        achternaam = reader["Achternaam"].ToString();
                        rolId = (int)reader["RolId"];
                    }
                    else
                    {
                        voornaam = null;
                        achternaam = null;
                        rolId = 0;
                    }
                }
            }
        }

        public static void UpdateUserRole(string gebruikersnaam, int rolId)
        {
            string query = "UPDATE Gebruikers SET RolId = @RolId WHERE Gebruikersnaam = @Gebruikersnaam";
            using (SqlConnection con = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Gebruikersnaam", gebruikersnaam);
                cmd.Parameters.AddWithValue("@RolId", rolId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}