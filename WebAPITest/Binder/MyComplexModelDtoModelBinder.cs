using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Http.ModelBinding;
using System.Web.Http.ModelBinding.Binders;

namespace WebAPITest.Binder
{
    public class MyComplexModelDtoModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            ComplexModelDto dto = bindingContext.Model as ComplexModelDto;
            if(dto==null)
            {
                return false;
            }
            foreach (ModelMetadata property in dto.PropertyMetadata)
            {
                ModelBindingContext subContext = new ModelBindingContext(bindingContext) {
                    ModelMetadata = property,
                    ModelName = ModelNameBuiler.CreatePropertyModelName(bindingContext.ModelName, property.PropertyName)
                };
                if(actionContext.Bind(subContext))
                {
                    dto.Results[property] = new ComplexModelDtoResult(subContext.Model, subContext.ValidationNode);
                }
            }
            return true;
        }
    }

    public static class ModelNameBuiler
    {
        public  static string CreatePropertyModelName(string prefix,string propertyName)
        {
            if(string.IsNullOrEmpty(prefix))
            {
                return propertyName ?? string.Empty;
            }
            if(!string.IsNullOrEmpty(propertyName))
            {
                return prefix + "." + propertyName;
            }
            return prefix ?? string.Empty;
        }
    }
}