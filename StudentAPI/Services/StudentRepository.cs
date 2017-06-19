using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudentAPI.Models;

namespace StudentAPI.Services
{
    public class StudentRepository
    {
        public List<StudentManager> GetAllStudents()
        {
            StudentManager studentManager = new StudentManager();
            List<StudentManager> studentList = new List<StudentManager>();
            studentList = studentManager.GetStudent("","","");
            return studentList;
        }

        public int AddNewStudent(StudentManager studentManager)
        {
            int studentId = studentManager.AddStudent();
            return studentId;
        }
    }
}