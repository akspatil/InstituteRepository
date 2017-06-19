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
    public class StudentManager
    {
        [ScaffoldColumn(false)]
        public int StudentId { get; set; }
        [Required]
        [JsonProperty("firstname")]
        public string FirstName { get; set; }

        [JsonProperty("lastname")]
        public string LastName { get; set; }

        [JsonProperty("suburb")]
        public string Suburb { get; set; }

        [JsonProperty("emailid")]
        public  string EmailId { get; set; }

        [JsonProperty("phonenumber")]
        public string PhoneNumber { get; set; }
          
        [JsonProperty("enrolledhours")]
        public int EnrolledHours { get; set; }

        public List<StudentManager> GetStudent(string phoneNumber, string firstName, string lastName)
        {
            //write data connection class
            List<SqlParameter> sqlParameters = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@FirstName", SqlDbType = SqlDbType.NVarChar, Value = firstName },
                new SqlParameter() {ParameterName = "@LastName", SqlDbType = SqlDbType.NVarChar, Value = lastName },
                new SqlParameter() {ParameterName = "@PhoneNumber", SqlDbType = SqlDbType.NVarChar, Value = phoneNumber }
            };
            DataConnection dataConnection = DataConnection.Instance();
            DataTable dataTable = dataConnection.ExecuteNonQueryData("GetStudentDetails", sqlParameters);
            List<StudentManager> students = new List<StudentManager>();
            students = dataTable.AsEnumerable().Select(dataRow => new StudentManager
            {
                FirstName = dataRow.Field<string>("FirstName"),
                LastName = dataRow.Field<string>("LastName"),
                Suburb = dataRow.Field<string>("Suburb"),
                EmailId = dataRow.Field<string>("EmailId"),
                PhoneNumber = dataRow.Field<string>("PhoneNumber"),
                EnrolledHours = dataRow.Field<int>("EnrolledHours")
            }).ToList();
            return students;
        }

        public int AddStudent()
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@FirstName", SqlDbType = SqlDbType.NVarChar, Value = FirstName },
                new SqlParameter() {ParameterName = "@LastName", SqlDbType = SqlDbType.NVarChar, Value = LastName },
                new SqlParameter() {ParameterName = "@Suburb", SqlDbType = SqlDbType.NVarChar, Value = Suburb },
                new SqlParameter() {ParameterName = "@EmailId", SqlDbType = SqlDbType.NVarChar, Value = EmailId },
                new SqlParameter() {ParameterName = "@PhoneNumber", SqlDbType = SqlDbType.NVarChar, Value = PhoneNumber },
                new SqlParameter() {ParameterName = "@EnrolledHours", SqlDbType = SqlDbType.NVarChar, Value = EnrolledHours }
            };

            DataConnection dataConnection = DataConnection.Instance();
            DataTable dataTable = dataConnection.ExecuteNonQueryData("AddNewStudentDetails", sqlParameters);
            int id = Convert.ToInt32(dataTable.Rows[0][0 ].ToString());
            return id;
        }
    }
}