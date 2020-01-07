using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFTShop.Middlewares
{
    public class ChangeRequestUrl : IMiddleware
    {
        public ChangeRequestUrl()
        {

        }
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.Request.Host = new HostString("https://www.google.com");

            return next.Invoke(context);
        }
    }
}
