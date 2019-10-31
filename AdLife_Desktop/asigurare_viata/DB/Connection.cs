using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asigurare_viata.DB
{
    class Connection
    {
        public static SqlConnection sqlcon()
        {

            SqlConnection conn = new SqlConnection("Data Source=SQL6004.site4now.net;Initial Catalog=DB_A495DC_licenta;User Id=DB_A495DC_licenta_admin;Password=licenta1234;");
            conn.Open();
            return conn;
        }
    }
}
