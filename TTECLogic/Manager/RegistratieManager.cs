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
        public static List<Registratie> GetRegistraties()
        {
            using (SqlConnection objcn = new SqlConnection())

                objcn.ConnectionString = "Data Source=localhost;Initial Catalog=TTEC;Integrated Security=True";
        }
    }
}
