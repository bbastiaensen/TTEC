using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTECLogic.Manager
{
    public static class RollenManager
    {
        public static string ConnectionString { get; set; }

        public static List<Rol> GetRollen()
        {
            List<Rol> rollen = new List<Rol>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Naam FROM Rollen ORDER BY Naam", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    rollen.Add(new Rol
                    {
                        Id = (int)reader["Id"],
                        Naam = reader["Naam"].ToString()
                    });
                }
            }
            return rollen;
        }

        public static Rol GetRol(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Naam FROM Rollen WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Rol
                    {
                        Id = (int)reader["Id"],
                        Naam = reader["Naam"].ToString()
                    };
                }
            }
            return null;
        }

        public static void InsertRol(string naam)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Rollen (Naam) VALUES (@Naam)", conn);
                cmd.Parameters.AddWithValue("@Naam", naam);
                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateRol(int id, string naam)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Rollen SET Naam = @Naam WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Naam", naam);
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteRol(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Rollen WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}