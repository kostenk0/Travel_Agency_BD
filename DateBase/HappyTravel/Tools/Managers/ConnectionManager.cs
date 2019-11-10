using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyTravel.Tools.Managers
{
    internal static class ConnectionManager
    {
        public static MySqlConnection Connection { get; private set; }

        public static string ConnString { get; private set; }

        internal static void Init()
        {
            ConnString = "server=us-cdbr-gcp-east-01.cleardb.net;user=b08c8f9d8c7549;database=gcp_b59f698ea7b4607d7c34;password=1ab93de2;Allow Zero DateTime = true;Convert Zero Datetime = true";
            Connection = new MySqlConnection(ConnString);
            Connection.Open();
        }
    }
}
