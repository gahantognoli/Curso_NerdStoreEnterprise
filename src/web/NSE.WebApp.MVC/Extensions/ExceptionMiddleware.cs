using Grpc.Core;
using Microsoft.AspNetCore.Http;
using NSE.WebApp.MVC.Services;
using Polly.CircuitBreaker;
using Refit;
using System;
using System.Net;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private static IAutenticacaoService _autenticacaoService;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IAutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;

            try
            {
                await _next(httpContext);
            }
            catch (CustomHttpResponseException ex)
            {
                HandleRequestExpectionAsync(httpContext, ex.StatusCode);
            }
            catch(ValidationApiException ex)
            {
                HandleRequestExpectionAsync(httpContext, ex.StatusCode);
            }
            catch (ApiException ex)
            {
                HandleRequestExpectionAsync(httpContext, ex.StatusCode);
            }
            catch (BrokenCircuitException)
            {
                HandleCircuitBreakerExceptionAsync(httpContext);
            }
            catch (RpcException ex)
            {
                var statusCode = ex.StatusCode switch
                {
                    StatusCode.Internal => 400,
                    StatusCode.Unauthenticated => 401,
                    StatusCode.PermissionDenied => 403,
                    StatusCode.Unimplemented => 404,
                    _ => 500
                };

                var httpStatusCode = Enum.Parse<HttpStatusCode>(statusCode.ToString());

                
                HandleRequestExpectionAsync(httpContext, httpStatusCode);
            }
        }

        public static void HandleRequestExpectionAsync(HttpContext httpContext, HttpStatusCode statusCode)
        {
            if (statusCode == HttpStatusCode.Unauthorized) {

                if (_autenticacaoService.TokenExpirado())
                {
                    if (_autenticacaoService.RefreshTokenValido().Result)
                    {
                        httpContext.Response.Redirect(httpContext.Request.Path);
                        return;
                    }
                }

                _autenticacaoService.Logout();
                httpContext.Response.Redirect($"/login?ReturnUrl={httpContext.Request.Path}");
                return;
            }

            httpContext.Response.StatusCode = (int)statusCode;
        }

        private static void HandleCircuitBreakerExceptionAsync(HttpContext context)
        {
            context.Response.Redirect("/sistema-indisponivel");
        }
    }
}
