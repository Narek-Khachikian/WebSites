using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platform
{
    public class ConsentMiddleware
    {
        private RequestDelegate next;

        public ConsentMiddleware(RequestDelegate nextDelegate)
        {
            next = nextDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/consent")
            {
                ITrackingConsentFeature consentFeature =
                    context.Features.Get<ITrackingConsentFeature>();
                if (consentFeature.HasConsent)
                {
                    consentFeature.WithdrawConsent();
                }
                else
                {
                    consentFeature.GrantConsent();
                }

                await context.Response.WriteAsync(consentFeature.HasConsent ? "Consent Granted\n" : "Consent Withdrawn\n");

            }

            await next(context);
        }
    }
}
