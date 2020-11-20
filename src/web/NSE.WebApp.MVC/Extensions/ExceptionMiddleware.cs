using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (CustomHttpResponseException ex)
            {
                HandleRequestExpectionAsync(httpContext, ex);
            }
        }

        public static void HandleRequestExpectionAsync(HttpContext httpContext, CustomHttpResponseException httpResponseException)
        {
            if (httpResponseException.StatusCode == HttpStatusCode.Unauthorized) { 
                httpContext.Response.Redirect($"/login?ReturnUrl={httpContext.Request.Path}");
                return;
            }

            httpContext.Response.StatusCode = (int)httpResponseException.StatusCode;
        }
    }
}
