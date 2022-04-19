using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using RESTuserAPI.Models;

namespace TSDWebApp.Services
{
    public class DatabaseService
    {
        //private static SqlDataAdapter dataAdapter = new SqlDataAdapter();
        private DBConnectionService dbConnectionService { get; }

        public DatabaseService(DBConnectionService connService)
        {
            dbConnectionService = connService;

            if (dbConnectionService.GetConnectionStatus != ConnectionState.Open)
            {
                dbConnectionService.OpenConnection();
            }
        }

        public Int32 GetUserCount()
        {
            try
            {
                if (dbConnectionService.GetConnectionStatus != ConnectionState.Open)
                {
                    dbConnectionService.OpenConnection();
                }

                //DataSet ds = new DataSet();
                Int32 recordCount = -1;

                string sql = @"SELECT COUNT(""UserID"") FROM {tableName};";
                sql = sql.Replace("{tableName}", "User");

                //dataAdapter.SelectCommand = new SqlCommand(maxIdxCommand, dbConnection.GetConnection);
                //dataAdapter.Fill(ds);

                MySqlCommand comm = new MySqlCommand(sql, dbConnectionService.GetConnection);

                Object result = comm.ExecuteScalar();

                recordCount = Convert.ToInt32(result);

                return recordCount;
            }
            catch (Exception)
            {
                return 1;
                //throw;
            }
        }

        private Int32 MaxRecNo(string tableName)
        {
            try
            {
                if (dbConnectionService.GetConnectionStatus != ConnectionState.Open)
                {
                    dbConnectionService.OpenConnection();
                }

                DataSet ds = new DataSet();
                Int32 maxIdx = -1;

                string maxIdxCommand = @"SELECT MAX(UserID) FROM {tableName};";
                maxIdxCommand = maxIdxCommand.Replace("{tableName}", tableName);

                //dataAdapter.SelectCommand = new SqlCommand(maxIdxCommand, dbConnection.GetConnection);
                //dataAdapter.Fill(ds);

                MySqlCommand comm = new MySqlCommand(maxIdxCommand, dbConnectionService.GetConnection);

                Object Result = comm.ExecuteScalar();

                maxIdx = Convert.ToInt32(Result);

                return ++maxIdx;
            }
            catch (Exception)
            {
                return 1;
                //throw;
            }
        }

        public void InsertNewUser(User user)
        {
            try
            {
                if (dbConnectionService.GetConnectionStatus != ConnectionState.Open)
                {
                    dbConnectionService.OpenConnection();
                }

                DataSet ds = new DataSet();
                Int32 maxIdx = MaxRecNo("User");

                string sql = @"INSERT INTO {tableName} (UserID, Name, NRIC, DOB, Age) VALUES ({NextID}, '{Name}', '{NRIC}', STR_TO_DATE('{DOB}', '%d/%m/%Y'), {Age});";
                sql = sql.Replace("{tableName}", "User");
                sql = sql.Replace("{NextID}", maxIdx.ToString());
                sql = sql.Replace("{Name}", user.Name);
                sql = sql.Replace("{NRIC}", user.NRIC);
                sql = sql.Replace("{DOB}", user.DOB);
                sql = sql.Replace("{Age}", user.Age.ToString());

                MySqlCommand comm = new MySqlCommand(sql, dbConnectionService.GetConnection);

                comm.ExecuteNonQuery();

                return;
            }
            catch (Exception)
            {
                return;
                //throw;
            }
        }

        public void Update(User user)
        {
            try
            {
                if (dbConnectionService.GetConnectionStatus != ConnectionState.Open)
                {
                    dbConnectionService.OpenConnection();
                }

                DataSet record = new DataSet();
                //Int32 recordCount = -1;

                string sqlUpdate = @"UPDATE USER SET Name = '{Name}' WHERE UserID = {UserID};";
                sqlUpdate = sqlUpdate.Replace("{tableName}", "User");
                sqlUpdate = sqlUpdate.Replace("{Name}", user.Name);
                sqlUpdate = sqlUpdate.Replace("{UserID}", user.UserID.ToString());

                MySqlCommand comm = new MySqlCommand(sqlUpdate, dbConnectionService.GetConnection);

                comm.ExecuteNonQuery();
                //Object result = comm.ExecuteNonQuery();
                //recordCount = Convert.ToInt32(result);

                return;
            }
            catch (Exception)
            {
                return;
                //throw;
            }
        }

        public void Delete(User user)
        {
            try
            {
                if (dbConnectionService.GetConnectionStatus != ConnectionState.Open)
                {
                    dbConnectionService.OpenConnection();
                }

                DataSet record = new DataSet();

                string sqlDelete = @"DELETE FROM USER WHERE UserID = {UserID};";
                sqlDelete = sqlDelete.Replace("{tableName}", "User");
                sqlDelete = sqlDelete.Replace("{UserID}", user.UserID.ToString());

                MySqlCommand comm = new MySqlCommand(sqlDelete, dbConnectionService.GetConnection);

                comm.ExecuteNonQuery();

                return;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetUserList()
        {
            List<User> userList = new List<User>();

            try
            {
                if (dbConnectionService.GetConnectionStatus != ConnectionState.Open)
                {
                    dbConnectionService.OpenConnection();
                }

                //DataSet ds = new DataSet();
                string sql = @"SELECT * FROM {tableName}";
                sql = sql.Replace("{tableName}", "User");

                //dataAdapter.SelectCommand = new SqlCommand(maxIdxCommand, dbConnection.GetConnection);
                //dataAdapter.Fill(ds);

                MySqlCommand comm = new MySqlCommand(sql, dbConnectionService.GetConnection);

                MySqlDataReader mysqlReader = comm.ExecuteReader();

                while (mysqlReader.Read())
                {
                    User _user = new User();
                    _user.UserID = Convert.ToInt32(mysqlReader[0]);
                    _user.Name = mysqlReader[1].ToString();
                    _user.NRIC = mysqlReader[2].ToString();
                    _user.DOB = mysqlReader[3].ToString();

                    userList.Add(_user);
                }

                mysqlReader.Close();

                string obj = JsonConvert.SerializeObject(userList, Formatting.Indented);

                return obj;
            }
            catch (Exception)
            {
                return null;
                //throw;
            }
        }


    }
}
