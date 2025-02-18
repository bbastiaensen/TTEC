using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTECLogic.Object;

namespace TTECLogic.Manager
{
    public class SessieManager
    {
        public static int GetSessionID(login logindata)
        {
            Sessie sessie = new Sessie();
            using (SqlConnection objCn = new SqlConnection())
            {
                objCn.ConnectionString = LoginManager.ConnectionString;

                using (SqlCommand objCmd = new SqlCommand())
                {
                    objCmd.Connection = objCn;
                    objCmd.CommandType = System.Data.CommandType.Text;
                    objCmd.CommandText = "SELECT * FROM Gebruikers WHERE Gebruikersnaam = @Gebruikersnaam;";

                    objCmd.Parameters.AddWithValue("@Gebruikersnaam", logindata.Gebruikersnaam);

                    objCn.Open();
                    SqlDataReader objRea = objCmd.ExecuteReader();
                    if (objRea.Read())
                    {
                        return Convert.ToInt16(objRea["RolId"]);
                    }
                    else return 0;

                    

                }
            }
        }

    }
}
