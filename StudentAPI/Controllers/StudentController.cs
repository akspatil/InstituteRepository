using StudentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using System.Web.Http;
using StudentAPI.Services;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StudentAPI.Filters;

namespace StudentAPI.Controllers
{
    //[RoutePrefix("api/Student")]
    [AuthorizationFilter]
    public class StudentController : ApiController
    {
        private StudentRepository studentRepository;
        private PaymentRepository paymentRepository;

        public StudentController()
        {
            this.studentRepository = new StudentRepository();
            this.paymentRepository = new PaymentRepository();
        }

        [HttpGet]
        public List<StudentManager> GetAll()
        {
            return studentRepository.GetAllStudents();
        }

        //[AuthenticationFilter(true)]
        [HttpPost]
        [ActionName("PostStudent")]
        public HttpResponseMessage   AddStudentDetails(JObject json)
        {
            //DataBindModel dataBindModel = JsonConvert.DeserializeObject<DataBindModel>(json.ToString());
            StudentManager studentManager = JsonConvert.DeserializeObject<StudentManager>(json["student"].ToString());//dataBindModel.studentManager;
            PaymentManager paymentManager = JsonConvert.DeserializeObject<PaymentManager>(json["payment"].ToString());
            int studentId = studentRepository.AddNewStudent(studentManager);
            if (studentId > 0 && paymentManager != null)
            {
                paymentManager.AddPaymentDetails(studentId);
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
            //bool success = paymentRepository.AddPaymentDetails(studentId, paymentRepository);
        }
    }
}
