using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.Routing;
using System.Web.Http.ValueProviders;
using WebAPITest.Binder;
using WebAPITest.Models;

namespace WebAPITest.Controllers
{
    public class DemoController : ApiController
    {

        //public IEnumerable<UriResolutionResult> RouteDataAction()
        //{
        //    string routeTemplate = "api/movies/{genre}/{title}/{id}";
        //    IHttpRoute route = new HttpRoute(routeTemplate);
        //    HttpMethodConstraint constraint = new HttpMethodConstraint(HttpMethod.Post);
        //    route.Constraints.Add("httpMethod", constraint);
        //    string requestUri = "http://localhost:38232/api/movies/romance/titanic/001";
        //    var request1 = new HttpRequestMessage(HttpMethod.Get, requestUri);
        //    var request2 = new HttpRequestMessage(HttpMethod.Post, requestUri);
        //    string root1 = "/";
        //    string root2 = "/api/";
        //    IHttpRouteData rootData1 = route.GetRouteData(root1, request1);
        //    IHttpRouteData rootData2 = route.GetRouteData(root1, request2);
        //    IHttpRouteData rootData3 = route.GetRouteData(root2, request1);
        //    IHttpRouteData rootData4 = route.GetRouteData(root2, request2);

        //    yield return new UriResolutionResult(root1, "Get", rootData1 != null);
        //    yield return new UriResolutionResult(root1, "Post", rootData2 != null);
        //    yield return new UriResolutionResult(root2, "Get", rootData3 != null);
        //    yield return new UriResolutionResult(root2, "Post", rootData4 != null);

        //}

        [HttpGet]
        public IEnumerable<string> HGet()
        {
            string routeTemplate = "weather/{areacode}/{days}";
            IHttpRoute route = new HttpRoute(routeTemplate);
            route.Defaults.Add("days", 2);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "/");
            IHttpVirtualPathData pathData;
            Dictionary<string, object> values = new Dictionary<string, object>();
            pathData = route.GetVirtualPath(request, values);
            yield return pathData == null ? "" : pathData.VirtualPath;
            values.Add("areacode", "028");
            pathData = route.GetVirtualPath(request, values);
            yield return pathData == null ? "" : pathData.VirtualPath;
            values.Add("httproute", true);
            values.Add("days", 3);
            IHttpRouteData routeData = new HttpRouteData(route);
            routeData.Values.Add("areacode", "0512");
            routeData.Values.Add("days", "4");
            request.SetRouteData(routeData);
            pathData = route.GetVirtualPath(request, values);
            yield return pathData == null ? "" : pathData.VirtualPath;

            values.Clear();
            values.Add("httproute", true);
            pathData = route.GetVirtualPath(request, values);
            yield return pathData == null ? "" : pathData.VirtualPath;

            routeData.Values.Remove("days");
            pathData = route.GetVirtualPath(request, values);
            yield return pathData == null ? "" : pathData.VirtualPath;

        }
        [HttpGet]
        public Tuple<IEnumerable<string>,IEnumerable<string>> ServerTest()
        {
            HttpConfiguration config = new HttpConfiguration();
            config.MessageHandlers.Add(new FooHandler());
            config.MessageHandlers.Add(new BarHandler());
            config.MessageHandlers.Add(new BazHandler());

            MyHttpServer httpserver = new MyHttpServer(config);
            var chain1= GetHandleChain(httpserver).ToArray();
            httpserver.Initialize();
            var chain2 = GetHandleChain(httpserver).ToArray();
            return new Tuple<IEnumerable<string>, IEnumerable<string>>(chain1, chain2);
        }

        private IEnumerable<string> GetHandleChain(DelegatingHandler handler)
        {
            yield return handler.GetType().Name;
            while(handler.InnerHandler!=null)
            {
                yield return handler.InnerHandler.GetType().Name;
                handler = handler.InnerHandler as DelegatingHandler;
                if(handler==null)
                {
                    break;
                }
            }
        }
        [HttpGet]
        public IEnumerable<string> PrincipalTest()
        {
            Thread.CurrentPrincipal = null;
            HttpRequestMessage request = new HttpRequestMessage();
            MyHttpServer httpserver = new WebAPITest.MyHttpServer();
            httpserver.SendAsync(request);
           var principal= Thread.CurrentPrincipal;
            string identity1 = string.IsNullOrEmpty(principal.Identity.Name) ? "" : principal.Identity.Name;

            GenericIdentity identity = new GenericIdentity("Artech");
            Thread.CurrentPrincipal = new GenericPrincipal(identity, new string[0]);
            request = new HttpRequestMessage();
            httpserver.SendAsync(request);
            principal = Thread.CurrentPrincipal;
            string identity2 = string.IsNullOrEmpty(principal.Identity.Name) ? "" : principal.Identity.Name;
            return new string[] { identity1,identity2 };
        }
        [HttpGet]
        public async Task<IDictionary<string,object>> Dispatcher()
        {
           
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute("default", "wheather/{areacode}/{days}");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "http://www.artech.com/wheather/010/2");
            MyHttpRoutingDispatcher dispatcher = new WebAPITest.MyHttpRoutingDispatcher(config);
            await dispatcher.SendAsync(request, CancellationToken.None);
            var routedata= request.GetRouteData();
            return routedata.Values;
        }

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            StaticValueProviderFactory.Clear();
            StaticValueProviderFactory.Add("Name", "张三");
            StaticValueProviderFactory.Add("PhoneNo", "18949881803");
            StaticValueProviderFactory.Add("EmailAddress", "378917466@qq.com");
            StaticValueProviderFactory.Add("Address.Province", "安徽省");
            StaticValueProviderFactory.Add("Address.City", "合肥");
            StaticValueProviderFactory.Add("Address.District", "庐阳产业园");
            StaticValueProviderFactory.Add("Address.Street", "阜阳路1008号");

        }
        [HttpGet]
        public Contact Get([ValueProvider(typeof(StaticValueProviderFactory))]Contact contact)
        {
            return contact;
        }
        [HttpGet]
        public Tuple<string,int,int?> SimpleGet(string foo,int bar,int? baz)
        {
            return new Tuple<string, int, int?>(foo, bar, baz);
        }
        [HttpGet]
        public Dictionary<string, object> ShowModelState([ValueProvider(typeof(StaticValueProviderFactory))]Contact contact)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            foreach (string key in ModelState.Keys)
            {
                dic.Add(key, ModelState[key].Value.RawValue);
            }
            return dic;
        }

    }
}
