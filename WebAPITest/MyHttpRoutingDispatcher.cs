using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace WebAPITest
{
    public class MyHttpRoutingDispatcher:HttpRoutingDispatcher
    {
        public MyHttpRoutingDispatcher(HttpConfiguration config):base(config)
        {

        }
        public new Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken token)
        {
            return base.SendAsync(request,CancellationToken.None);
        }
    }
}