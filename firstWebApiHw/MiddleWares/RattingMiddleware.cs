﻿using Entities;
using Services;
namespace webApiShopSite.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RattingMiddleware
    {
        private readonly RequestDelegate _next;
        
        public RattingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext,IRatingService _ratingService)
        {
            
            Rating rating = new Rating();
            rating.Host = httpContext.Request.Headers.Host;
            rating.Method = httpContext.Request.Method;
            rating.Path = httpContext.Request.Path;
            rating.Referer = httpContext.Request.Headers.Referer;
            rating.UserAgent = httpContext.Request.Headers.UserAgent;
            rating.RecordDate = DateTime.Now;
            _ratingService.createRatingAsync(rating);
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RattingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRattingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RattingMiddleware>();
        }
    }
}
