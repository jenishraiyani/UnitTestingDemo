using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using UniversityManagementApi.Extentions;

namespace UniversityManagementApi.Middleware
{
    public class RateLimitMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDistributedCache _cache;
       
        public RateLimitMiddleware(RequestDelegate next, IDistributedCache cache)
        {
            _next = next;
            _cache = cache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
        
            if (!context.HasRateLimitAttribute(out var decorator))
            {
                await _next(context);
                return;
            }

            var consumptionData = await _cache.GetCustomerConsumptionDataFromContextAsync(context);
            if (consumptionData is not null)
            {
             
                if (consumptionData.HasConsumedAllRequests(decorator!.timeWindowInSeconds, decorator!.maxRequests))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                    return;
                }
                consumptionData.IncreaseRequests(decorator!.maxRequests);
            }

            await _cache.SetCacheValueAsync(context.GetCustomerKey(), consumptionData);
            await _next(context);
        }
    }
}
