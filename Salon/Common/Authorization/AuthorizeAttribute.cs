using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Salon.DAL.Models;
namespace Salon.Common.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var LoginDetails = (LoginDetailsModel)context.HttpContext.Items["User"];
            if (LoginDetails == null)
            {
                // not logged in
                context.Result = new JsonResult(new { Message = "Unauthorized", StatusCode = StatusCodes.Status401Unauthorized }) 
                { 
                    StatusCode = StatusCodes.Status401Unauthorized 
                };
            }
           // To do resource checked for authenticate user 
        }
    }
}
