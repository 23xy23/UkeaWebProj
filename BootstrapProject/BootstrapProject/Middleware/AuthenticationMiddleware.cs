using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BootstrapProject.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Session.Keys.Contains("Username") && 
                context.Request.Path != "/Login" &&
                context.Request.Path != "/Login/Login")
            {
                context.Response.Redirect("/Login/Login");
                return;
            }

            await _next(context);
        }
    }
}
