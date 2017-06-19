using StudentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace StudentAPI.Filters
{
    public class AuthenticationFilter : AuthorizationFilterAttribute
    {
        public AuthenticationFilter()
        {
        }

        private bool _isActive = true;

        public AuthenticationFilter(bool isActive)
        {
            _isActive = isActive;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (!_isActive) return;
            var identity = FetchAuthenticationHeader(actionContext);
            if (identity == null)
            {
                ChallengeAuthenticationRequest(actionContext);
                return;
            }
            var genericPrincipal = new GenericPrincipal(identity, null);
            Thread.CurrentPrincipal = genericPrincipal;
            if (!OnAuthorizeUser(identity.Name, identity.Password, actionContext))
            {
                ChallengeAuthenticationRequest(actionContext);
                return;
            }
            base.OnAuthorization(actionContext);
        }

        private bool OnAuthorizeUser(string username, string password, HttpActionContext filterContext)
        {
            Users users = new Users();
            if (!String.IsNullOrEmpty(username) || !String.IsNullOrEmpty(password))
            {
                int userId = users.ValidateUser(username, password);
                if (userId > 0)
                {
                    var basicAuthenticationIdentity = Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
                    if (basicAuthenticationIdentity != null)
                    {
                        basicAuthenticationIdentity.UserId = userId;
                        return true;
                    }
                }
            }            
            return false;
        }
        
        private BasicAuthenticationIdentity FetchAuthenticationHeader(HttpActionContext actionContext)
        {
            string authHeadervalue = null;
            var authRequest = actionContext.Request.Headers.Authorization;
            if (authRequest != null && !String.IsNullOrEmpty(authRequest.Scheme) && authRequest.Scheme == "Basic")
            {
                authHeadervalue = authRequest.Parameter;
            }
            if (String.IsNullOrEmpty(authHeadervalue)) return null;
            authHeadervalue = Encoding.Default.GetString(Convert.FromBase64String(authHeadervalue));
            var credentials = authHeadervalue.Split(':');
            return credentials.Length < 2 ? null : new BasicAuthenticationIdentity(credentials[0], credentials[1]);
        }

        private static void ChallengeAuthenticationRequest(HttpActionContext filterContext)
        {
            var dnsHost = filterContext.Request.RequestUri.DnsSafeHost;
            filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            filterContext.Response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", dnsHost));
        }
    }
}