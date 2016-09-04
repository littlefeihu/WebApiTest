using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace WebAPITest.Binder
{
    public class MyComplexDtoModelBinderProvider:ModelBinderProvider
    {
        public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
        {
            return new MyComplexModelDtoModelBinder();
        }
    }
}