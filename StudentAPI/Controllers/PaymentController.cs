using StudentAPI.Models;
using StudentAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudentAPI.Controllers
{
    public class PaymentController : ApiController
    {
        private PaymentRepository paymentRepository;

        public PaymentController()
        {
            this.paymentRepository = new PaymentRepository();
        }

        public void AddPayment(PaymentManager paymentManager)
        {
           // paymentRepository.AddPaymentDetails(paymentManager);
        }
    }
}
