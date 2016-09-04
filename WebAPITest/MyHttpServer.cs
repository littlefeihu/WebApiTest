using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace WebAPITest
{
    public class MyHttpServer:HttpServer
    {
        public MyHttpServer( ) : base()
        {

        }
        public MyHttpServer(HttpConfiguration configuration):base(configuration)
        {

        }
        public new void Initialize()
        {
            base.Initialize();
        }
        public  Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return base.SendAsync(request, CancellationToken.None);
        }
    }
}