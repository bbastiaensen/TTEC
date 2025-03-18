using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TTECLogic.Object;

namespace TTECLogic.Manager
{
    public class LoginManager
    {

        public static string ConnectionString { get; set; }
        public static bool Login(login login)
        {
            string connectionString = LoginManager.ConnectionString;
            bool isAuthenticated = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Wachtwoord FROM Gebruikers WHERE Gebruikersnaam = @Gebruikersnaam";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Gebruikersnaam", login.Gebruikersnaam);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            string storedPassword = result.ToString();

                            if (storedPassword == HashManager.HashPassword(login.Wachtwoord))
                            {
                                isAuthenticated = true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Fout bij inloggen: " + ex.Message);
                    }
                }
            }

            return isAuthenticated;
        }



    }
}
