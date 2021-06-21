using Microsoft.AspNetCore.Builder;
namespace Salon.Common.Exceptions
{
    public static class RegisterExceptionHandler
    {
        public static void ExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandler>();
        }
    }
}
