using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;

namespace Zoro.Application.Services
{
    public class AntiForgeryHelper
    {
        public static string GenerateAntiForgeryToken(HttpContext httpContext)
        {
            var antiforgery = httpContext.RequestServices.GetRequiredService<IAntiforgery>();
            var tokenSet = antiforgery.GetAndStoreTokens(httpContext);
            return tokenSet.RequestToken;
        }
    }
}
