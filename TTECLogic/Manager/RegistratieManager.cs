using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTECLogic.Object;

namespace TTECLogic.Manager
{
    public static class RegistratieManager
    {
        public static string ConnectionString { get; set; }

        public static List<Registratie> GetRegistraties()
        {
            List<Registratie> registraties = null;

            using (SqlConnection objcn = new SqlConnection())
            {
                objcn.ConnectionString = ConnectionString;

                using (SqlCommand objCmd = new SqlCommand())
                {
                    objCmd.Connection = objcn;
                    objCmd.CommandText = "select * from Registraties";

                    objcn.Open();

                    SqlDataReader ObjRea = objCmd.ExecuteReader();

                    List<Registratie> list = null;

                    while (ObjRea.Read())
                    {
                        if (list == null)
                        {
                            list = new List<Registratie>();
                        }
                        Registratie r = new Registratie();

                    }



                    string query = @"
                    INSERT INTO Registraties (Voornaam, Achternaam, Gebruikersnaam, CampusZenit, CampusBoomgaard)
                    VALUES (@Voornaam, @Achternaam, @Gebruikersnaam, @CampusZenit, @CampusBoomgaard)";

                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@Voornaam", voornaam);
                    command.Parameters.AddWithValue("@Achternaam", achternaam);
                    command.Parameters.AddWithValue("@Gebruikersnaam", gebruikersnaam);
                    command.Parameters.AddWithValue("@CampusZenit", CampusZenit);
                    command.Parameters.AddWithValue("@CampusBoomgaard", CampusBoomgaard);

                }

                return registraties;
            }
        }

        public static void SaveRegistratie(Registratie registratie)
        {

        }
    }
}
