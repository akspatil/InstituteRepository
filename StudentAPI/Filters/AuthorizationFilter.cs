using StudentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace StudentAPI.Filters
{
    public class AuthorizationFilter : ActionFilterAttribute
    {
        private const string Token = "Token";

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Contains(Token))
            {
                var tokenValue = actionContext.Request.Headers.GetValues(Token).First();
                TokenManager tokenManager = new TokenManager();
                if (!tokenManager.ValidateToken(tokenValue))
                {
                    var responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized) { ReasonPhrase = "Invalid Request" };
                    actionContext.Response = responseMessage;
                }
            }
            else
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
            base.OnActionExecuting(actionContext);
        }
    }
}