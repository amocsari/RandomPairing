using RandomPairer.Api.RequestContext;
using RandomPairer.Common.RequestContext;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace RandomPairer.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static readonly string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        public static IServiceCollection RegisterRequestContext(this IServiceCollection serviceCollection)
        {
            return env switch
            {
                "Development" => serviceCollection.AddScoped<IRequestContext, HttpRequestContextDevelopment>(),
                "Release" => serviceCollection.AddScoped<IRequestContext, HttpRequestContextRelease>(),
                _ => serviceCollection.AddScoped<IRequestContext, HttpRequestContextDevelopment>(),
            };
        }
    }
}
