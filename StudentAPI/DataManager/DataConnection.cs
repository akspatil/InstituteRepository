using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace StudentAPI.DataManager
{
    public class DataConnection
    {
        private static DataConnection _instance;

        private DataConnection()
        {
        }

        public static DataConnection Instance()
        {
            if (_instance == null)
            {
                _instance = new DataConnection();
            }
            return _instance;
        }

        public SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString; //AppSettings["ConnectionString"].
            connection.Open();
            return connection;
        }

        public void CloseConnection(SqlConnection connection)
        {
            connection.Close();
        }

        public DataTable ExecuteNonQueryData(string procedureName, List<SqlParameter> sqlParameters)
        {
            using (var connection = OpenConnection())
            {
                using (SqlCommand command = new SqlCommand(procedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    //command.CommandText = procedureName;
                    command.Parameters.AddRange(sqlParameters.ToArray());
                    SqlDataAdapter dataAdapter = new SqlDataAdapter();
                    dataAdapter.SelectCommand = command;
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }
    }
}