using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SysAnalytics.Web.Models.Grid
{
    //public class JsonBinderAttribute : CustomModelBinderAttribute
    //{
    //    public override IModelBinder GetBinder()
    //    {
    //        return new ColumnChooserModelBinder();
    //    }

    //    public class ColumnChooserModelBinder : IModelBinder
    //    {
    //        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
    //        {
    //            //if (controllerContext == null)
    //            //    throw new ArgumentNullException("controllerContext");
    //            //if (bindingContext == null)
    //            //    throw new ArgumentNullException("bindingContext");

    //            //try
    //            //{
    //            //    var json = controllerContext.HttpContext.Request.Params[bindingContext.ModelName];

    //            //    if (String.IsNullOrWhiteSpace(json)) return null;

    //            //    // Swap this out with whichever Json deserializer you prefer.
    //            //    return Newtonsoft.Json.JsonConvert.DeserializeObject(json, typeof(ColumnChooserTemplate));
    //            //    //var model = new ColumnChooserTemplate();
    //            //    ////var request = controllerContext.HttpContext.Request;
    //            //    ////ValueProviderResult searchingFiltersResult = bindingContext.ValueProvider.GetValue("name");
    //            //    ////JavaScriptSerializer serializer = new JavaScriptSerializer();
    //            //    ////var content = serializer.Deserialize("tpl-content", typeof(string));
    //            //    //return model;
    //            //}
    //            //catch (Exception ex)
    //            //{
    //            //    return null;
    //            //}

    //            return null;
    //        }
    //    }
    //}
}