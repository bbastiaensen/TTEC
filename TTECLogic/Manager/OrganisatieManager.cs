using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTECLogic.Object;

namespace TTECLogic.Manager
{
    public static class OrganisatieManager
    {
        public static string ConnectionString { get; set; }

        public static List<Organisatie> GetOrganisaties()
        {
            List<Organisatie> organisaties = new List<Organisatie>();
            using (SqlConnection conne = new SqlConnection(ConnectionString))
            {
                conne.Open();
                var cmd = new SqlCommand("SELECT Id, Naam FROM Organisaties ORDER BY Naam", conne);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        organisaties.Add(new Organisatie
                        {
                            Id = (int)reader["Id"],
                            Naam = reader["Naam"].ToString()
                        });
                    }
                }
            }
            return organisaties;
        }

        public static Organisatie GetOrganisatie(int id)
        {
            using (var conne = new SqlConnection(ConnectionString))
            {
                conne.Open();
                var cmd = new SqlCommand("SELECT Id, Naam FROM Organisaties WHERE Id = @Id", conne);
                cmd.Parameters.AddWithValue("@Id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Organisatie
                        {
                            Id = (int)reader["Id"],
                            Naam = reader["Naam"].ToString()
                        };
                    }
                }
            }
            return null;
        }

        public static void InsertOrganisatie(string naam)
        {
            using (var conne = new SqlConnection(ConnectionString))
            {
                conne.Open();
                var cmd = new SqlCommand("INSERT INTO Organisaties (Naam) VALUES (@Naam)", conne);
                cmd.Parameters.AddWithValue("@Naam", naam);
                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateOrganisatie(int id, string naam)
        {
            using (var conne = new SqlConnection(ConnectionString))
            {
                conne.Open();
                var cmd = new SqlCommand("UPDATE Organisaties SET Naam = @Naam WHERE Id = @Id", conne);
                cmd.Parameters.AddWithValue("@Naam", naam);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteOrganisatie(int id)
        {
            using (var conne = new SqlConnection(ConnectionString))
            {
                conne.Open();
                var cmd = new SqlCommand("DELETE FROM Organisaties WHERE Id = @Id", conne);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}