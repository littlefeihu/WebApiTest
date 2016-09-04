using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;
using System.Web.Routing;
using WebAPITest.Binder;

namespace WebAPITest
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
           GlobalConfiguration.Configure(WebApiConfig.Register);

            // GlobalConfiguration.Configuration.MessageHandlers.Add(new HttpMethodOverrideHandler());
            //GlobalConfiguration.Configuration.Services.ReplaceRange(typeof(ModelBinderProvider), new ModelBinderProvider[] {
            //   new MyTypeConverterModelBinderProvider()
            //});
            GlobalConfiguration.Configuration.Services.ReplaceRange(typeof(ModelBinderProvider), new ModelBinderProvider[] {
               new MyTypeConverterModelBinderProvider(),
               new MyComplexDtoModelBinderProvider(),
               new MyMutableObjectModelBinderProvider()
            });

            GlobalConfiguration.Configuration.Services.Replace(typeof(ValueProviderFactory), new StaticValueProviderFactory());
          var s=  GlobalConfiguration.Configuration.Services.GetServices(typeof(ValueProviderFactory));
            Console.WriteLine(s.Count());

        }
    }
}
