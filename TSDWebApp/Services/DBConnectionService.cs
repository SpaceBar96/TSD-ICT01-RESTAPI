using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace TSDWebApp.Services
{
    public class DBConnectionService
    {
        public DBConnectionService()
        {

        }

        private static string ConnectionString = "server=localhost;user=elvis;database=tsd;port=3306;password=P@ssword123";

        private MySqlConnection conn = new MySqlConnection(ConnectionString);
        public ConnectionState GetConnectionStatus { get { return conn.State; } }
        public void OpenConnection() { conn.Open(); }
        public void CloseConnection() { conn.Close(); }
        public MySqlConnection GetConnection { get { return conn; } }
    }
}
