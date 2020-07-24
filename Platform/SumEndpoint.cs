using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Diagnostics;
using Microsoft.Extensions.Caching.Distributed;

namespace Platform
{
    public class SumEndpoint
    {
        public async Task Endpoint(HttpContext context, IDistributedCache cache)
        {
            Stopwatch st = new Stopwatch();
            int count = int.Parse((string)context.Request.RouteValues["count"]);
            string cacheKey = $"sum_{count}";
            st.Restart();
            string resultNum = cache.GetString(cacheKey);
            st.Stop();
            string result;
            
            if(resultNum == null)
            {
                long total = 0;
                st.Restart();

                for (int i = 1; i <= count; i++)
                {
                    total += i;
                }
                st.Stop();
                await cache.SetStringAsync(cacheKey, total.ToString(), 
                    new DistributedCacheEntryOptions() { SlidingExpiration = TimeSpan.FromMinutes(2) });
                result = $"to calculate sum from 0 to {count}, it took {st.ElapsedMilliseconds} ms, the total value is {total}";
            }
            else
            {
                try
                {
                    await cache.RefreshAsync(cacheKey);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                result = $"to calculate sum from 0 to {count}, it took {st.ElapsedMilliseconds} ms, the total value is {resultNum}";


            }
            st.Stop();
            
            await context.Response.WriteAsync(result);
        }
    }
}
