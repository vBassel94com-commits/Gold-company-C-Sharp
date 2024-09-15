using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace iGOLD
{
   public class dbClass
    {
        public SqlConnection con;
        public SqlCommand cmd;
        public SqlDataReader dr;

        public  dbClass()
        {
            con = new SqlConnection(GlobalVar.dataBaseLocation);
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
        }
    }

}
