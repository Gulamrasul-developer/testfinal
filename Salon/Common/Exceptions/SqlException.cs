using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace Salon.Common.Exceptions
{
    public static class SqlException
    {
        public static Task HandleException(HttpContext context, Exception ex)
        {
            using (LogException logException = new LogException())
                logException.Log(context, ex);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(new ExceptionDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error"
            }.ToString());
        }
    }
}
