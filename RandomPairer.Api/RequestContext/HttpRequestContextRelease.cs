using RandomPairer.Common.RequestContext;
using Microsoft.AspNetCore.Http;
using System;

namespace RandomPairer.Api.RequestContext
{
    public class HttpRequestContextRelease : IRequestContext
    {
        public string CurrentUserAd { get; }
        public string RequestId { get; }

        public HttpRequestContextRelease(IHttpContextAccessor httpContextAccessor)
        {
            RequestId = Guid.NewGuid().ToString();
            CurrentUserAd = httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "systemuser";
        }
    }
}
