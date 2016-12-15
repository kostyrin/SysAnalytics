using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace SysAnalytics.Web.Core.ActionFilters
{
    public class JsonFilter : ActionFilterAttribute
    {
        public string Parameter { get; set; }
        public Type JsonDataType { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.ContentType.Contains("application/json"))
            {
                string inputContent;
                using (var stream = filterContext.HttpContext.Request.InputStream)
                {
                    stream.Position = 0;
                    using (var reader = new System.IO.StreamReader(stream))
                    {
                        inputContent = reader.ReadToEnd();
                    }
                }


                var result = JsonConvert.DeserializeObject<IDictionary<string, object>>(inputContent)["content"].ToString();
                //var result = JsonConvert.DeserializeObject(inputContent, JsonDataType);
                filterContext.ActionParameters[Parameter] = result;
            }
        }
    }
}
