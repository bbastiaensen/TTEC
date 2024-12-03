using System.Data.SqlClient;
using TTECLogic.Object;

public static class RegistratieManager
{
    public static string ConnectionString { get; set; }

    public static bool IsEmailInUse(string email)
    {
        string query = @"
        SELECT 
            (SELECT COUNT(*) FROM Registraties WHERE Gebruikersnaam = @Email) +
            (SELECT COUNT(*) FROM Gebruikers WHERE Gebruikersnaam = @Email)";

        using (SqlConnection conn = new SqlConnection(ConnectionString))
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@Email", email);
            conn.Open();

            int count = (int)cmd.ExecuteScalar();
            return count > 0; 
        }
    }


    public static void SaveRegistratie(Registratie registratie)
    {
        string query = @"
            INSERT INTO Registraties (Voornaam, Achternaam, Gebruikersnaam, CampusZenit, CampusBoomgaard)
            VALUES (@Voornaam, @Achternaam, @Gebruikersnaam, @CampusZenit, @CampusBoomgaard)";
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@Voornaam", registratie.Voornaam);
            cmd.Parameters.AddWithValue("@Achternaam", registratie.Achternaam);
            cmd.Parameters.AddWithValue("@Gebruikersnaam", registratie.Gebruikersnaam);
            cmd.Parameters.AddWithValue("@CampusZenit", registratie.CampusZenit);
            cmd.Parameters.AddWithValue("@CampusBoomgaard", registratie.CampusBoomgaard);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
