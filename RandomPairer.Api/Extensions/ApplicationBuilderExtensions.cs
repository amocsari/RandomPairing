using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using RandomPairer.Common.Exceptions;
using System.Net;

namespace RandomPairer.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddExceptionHandling(this IApplicationBuilder app)
        {
            return app.UseExceptionHandler(err =>
            {
                err.Run(async context =>
                {
                    context.Response.ContentType = "text/plain";
                    var exception = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;

                    switch (exception)
                    {
                        case RandomPairerValidationException ex:
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            break;
                        default:
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            break;
                    }

                    if (!string.IsNullOrEmpty(exception.Message))
                    {
                        await context.Response.WriteAsync(exception.Message);
                    }
                    else
                    {
                        await context.Response.WriteAsync(exception.StackTrace);
                    }
                });
            });
        }
    }
}
