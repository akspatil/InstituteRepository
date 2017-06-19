using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentAPI.Models
{
    public class DataBindModel
    {
        public StudentManager studentManager { get; set; }
        public PaymentManager paymentManager { get; set; }
    }
}