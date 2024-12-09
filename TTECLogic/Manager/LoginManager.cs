using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;
using System.Threading.Tasks;
using TTECLogic.Object;

namespace TTECLogic.Manager
{
    public class LoginManager
    {

        public static string ConnectionString { get; set; }
        public static bool IsLoginValid(login logindata)
        {
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

                    if (CheckIfUserExist(objRea))
                    {
                        if (CheckIfPasswordIsCorrect(objRea, logindata.Wachtwoord))
                        {
                            SessieManager.InitializeSession(objRea["RolId"].ToString());
                            logindata.FoutBoodschap = "suc6";
                            return true;
                        }
                        else
                        {
                            logindata.FoutBoodschap = "Fout ww";
                            return false;
                        }
                    }
                    else
                    {
                        logindata.FoutBoodschap = "Foute gebruiker";
                        return false;
                    }
                }
            }
        }

        public static bool CheckIfUserExist(SqlDataReader objRea)
        {
            if (objRea.Read()) {
                return true;
            }
            return false;
        }

        public static bool CheckIfPasswordIsCorrect(SqlDataReader objRea, string pass)
        {
            
            if (objRea["Wachtwoord"].ToString() == HashManager.HashPassword(pass))
            {
                return true;
            }
            return false;
        }

        public static Sessie StartSession()
        {

        }
    

    }
}
