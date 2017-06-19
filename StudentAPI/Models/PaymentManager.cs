using Newtonsoft.Json;
using StudentAPI.DataManager;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace StudentAPI.Models
{
    public class PaymentManager
    {
        [ScaffoldColumn(false)]
        public int PayId { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        [JsonProperty("amountToPay")]
        public string AmountToPay { get; set; }

        [Required]
        [JsonProperty("paid")]
        public string Paid { get; set; } 

        [JsonProperty("pendingAmount")]
        public string PendingAmount { get; set; } = "0";

        public void AddPaymentDetails(int studentId)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@StudentId", SqlDbType = SqlDbType.BigInt, Value = studentId },
                new SqlParameter() {ParameterName = "@AmountToPay", SqlDbType = SqlDbType.NVarChar, Value = AmountToPay },
                new SqlParameter() {ParameterName = "@Paid", SqlDbType = SqlDbType.NVarChar, Value = Paid },
                new SqlParameter() {ParameterName = "@PendingAmount", SqlDbType = SqlDbType.NVarChar, Value = PendingAmount }
            };

            DataConnection dataConnection = DataConnection.Instance();
            DataTable dataTable = dataConnection.ExecuteNonQueryData("AddPaymentDetails", sqlParameters);
            int id = Convert.ToInt32(dataTable.Rows[0][0].ToString());
        }
    }
}