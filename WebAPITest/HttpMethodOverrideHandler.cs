﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebAPITest
{
    public class HttpMethodOverrideHandler:DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken token)
            {
            IEnumerable<string> methodOverrideHeader;
            if(request.Headers.TryGetValues("X-HTTP-Method-Overrider",out methodOverrideHeader))
            {
                request.Method = new HttpMethod(methodOverrideHeader.First());
            }
              return base.SendAsync(request, token);

            }
    }
}