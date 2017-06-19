using StudentAPI.DataManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace StudentAPI.Models
{
    public class TokenManager
    {
        public int TokenId { get; set; }

        public string AuthToken { get; set; }

        public int Userid { get; set; }

        public DateTime IssuedOn { get; set; }

        public DateTime ExpiresOn { get; set; }

        private DataConnection dataConnection = DataConnection.Instance();

        public TokenManager GenerateToken(int userId)
        {
            string token = Guid.NewGuid().ToString();
            DateTime issuedOn = DateTime.Now;
            DateTime expiresOn = DateTime.Now.AddSeconds(Convert.ToDouble(ConfigurationManager.AppSettings["AuthTokenExpiry"]));

            List<SqlParameter> sqlParameter = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "AuthToken", SqlDbType = SqlDbType.NVarChar, Value = token },
                new SqlParameter() { ParameterName = "UserId", SqlDbType = SqlDbType.BigInt, Value = userId },
                new SqlParameter() { ParameterName = "IssuedOn", SqlDbType = SqlDbType.DateTime, Value = issuedOn },
                new SqlParameter() { ParameterName = "ExpiresOn", SqlDbType = SqlDbType.DateTime, Value = expiresOn }
            };
            DataTable dataTable = new DataTable();
            dataTable = dataConnection.ExecuteNonQueryData("GenerateToken", sqlParameter);
            //TokenManager tokenObject = new TokenManager()
            //{
            //    AuthToken = token,
            //    Userid = userId,
            //    IssuedOn = issuedOn,
            //    ExpiresOn = expiresOn
            //};
            this.AuthToken = token;
            this.Userid = userId;
            this.IssuedOn = issuedOn;
            this.ExpiresOn = expiresOn;

            return this;
        }

        public bool ValidateToken(string token)
        {
            int seconds = Convert.ToInt32(ConfigurationManager.AppSettings["AuthTokenExpiry"]);
            List<SqlParameter> sqlParameter = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "TokenStr", SqlDbType = SqlDbType.NVarChar, Value = token },
                new SqlParameter() { ParameterName = "ExpirySeconds", SqlDbType = SqlDbType.Int, Value = seconds }
            };
            DataTable dataTable = new DataTable();
            dataTable = dataConnection.ExecuteNonQueryData("sp_ValidateToken", sqlParameter);
            if (dataTable.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public bool DeleteToken(int id, string type)
        {
            List<SqlParameter> sqlParameter = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "TokenId", SqlDbType = SqlDbType.BigInt, Value = id},
                new SqlParameter() { ParameterName = "IdType", SqlDbType = SqlDbType.NVarChar, Value = type }
            };
            DataTable dataTable = dataConnection.ExecuteNonQueryData("sp_DeleteTokens", sqlParameter);
            if (dataTable.Rows.Count > 0 && dataTable.Rows[0][0].ToString() == "0")
            {
                return true;
            }
            return false;
        }                   
    }
}