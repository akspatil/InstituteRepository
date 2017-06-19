using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace StudentAPI.Filters
{
    public class BasicAuthenticationIdentity:GenericIdentity
    {
        public int UserId { get; set; }

        public string Password { get; set; }

        public string Username { get; set; }

        public BasicAuthenticationIdentity(string username, string password): base(username, "Basic")
        {
            Password = password;
            Username = username;
        }
    }
}