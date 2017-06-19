using StudentAPI.DataManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace StudentAPI.Models
{
    public class Users
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int ValidateUser(string userName, string password)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "UserName", SqlDbType = System.Data.SqlDbType.NVarChar, Value = userName },
                new SqlParameter() {ParameterName = "Password", SqlDbType = System.Data.SqlDbType.NVarChar, Value = password }
            };
            DataConnection dataConnection = DataConnection.Instance();
            DataTable dataTable = dataConnection.ExecuteNonQueryData("ValidateUser", sqlParameters);
            int userId = 0;
            if (dataTable.Rows.Count > 0)
            {
                userId = Convert.ToInt32(dataTable.Rows[0][0].ToString());
            }
            return userId;
        }
    }
}