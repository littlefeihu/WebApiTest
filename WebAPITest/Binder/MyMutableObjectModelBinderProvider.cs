using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace WebAPITest.Binder
{
    public class MyMutableObjectModelBinderProvider:ModelBinderProvider
    {
        public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
        {
            if(MyMutableObjectBinder.CanBindType(modelType))
            {
                return new MyMutableObjectBinder();
            }
            return null;
        }
    }
}