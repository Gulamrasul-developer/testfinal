using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace Salon.Common.Exceptions
{
    public static class UnauthorizedException
    {
        public static Task HandleException(HttpContext context, Exception ex)
        {
            //using (LogException log = new LogException())
            //    log.LogError(context, ex);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return context.Response.WriteAsync(new ExceptionDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = "Unauthorized"
            }.ToString());
        }
    }
}
