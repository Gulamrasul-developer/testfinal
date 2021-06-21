using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
namespace Salon.Common.Exceptions
{
    public class ExceptionHandler
    {
        //private readonly ILogger<ExceptionHandler> _logger;
        private readonly RequestDelegate _next;
        public ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
        {
            //_logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                await SqlException.HandleException(context, ex);
            }
            catch (DataNotFoundException ex) 
            {
                await DataNotFoundException.HandleException(context, ex);
            }
            catch (SecurityTokenException ex)
            {
                await UnauthorizedException.HandleException(context, ex);
            }
            catch (Exception ex)
            {
                await BaseException.HandleException(context, ex);
            }
        }
    }
}
