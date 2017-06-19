using StudentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentAPI.Services
{
    public class PaymentRepository
    {
        public void AddPaymentDetails(int studentId, PaymentManager paymentManager)
        {
            paymentManager.AddPaymentDetails(studentId);
        }
    }
}