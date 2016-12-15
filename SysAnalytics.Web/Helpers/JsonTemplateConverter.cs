using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SysAnalytics.Web.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SysAnalytics.Web.Helpers
{
    public class JsonTemplateConverter
    {
        public static string ConvertToDB(string jsonPostData, string type)
        {
            var result = string.Empty;
            var content = JsonConvert.DeserializeObject<JObject>(jsonPostData)["content"].ToString(Formatting.None);
            var templname = JsonConvert.DeserializeObject<JObject>(jsonPostData)["templname"].ToString(Formatting.None);
            result = @"{""type"":""" + type + @""", ""templname"":" + templname + @", ""content"":" + content + "}&";

            return result;
        }

        public static IEnumerable<TemplateViewModel> ConvertFromDB(string templatesJson, string stringType)
        {
            var result = new List<TemplateViewModel>();
            if (string.IsNullOrEmpty(templatesJson)) return result;
            var templ = templatesJson.Split('&');
            foreach (var t in templ)
            {
                if (string.IsNullOrEmpty(t)) continue;
                var fields = JsonConvert.DeserializeObject<IDictionary<string, object>>(t);
                object type;
                fields.TryGetValue("type", out type);
                if (!type.Equals(stringType)) continue;

                result.Add(new TemplateViewModel { Name = fields["templname"].ToString(), Content = fields["content"].ToString(), Type = fields["type"].ToString() });
            }
            return result;
        }

        public static string RemoveTempl(string templ, string name, string stringType)
        {
            var result = templ;
            if (string.IsNullOrEmpty(templ)) return result;
            string templForRemove = string.Empty;
            var templArr = templ.Split('&');
            foreach (var t in templArr)
            {
                if (string.IsNullOrEmpty(t)) continue;
                var fields = JsonConvert.DeserializeObject<IDictionary<string, object>>(t);
                object type;
                fields.TryGetValue("type", out type);
                object templname;
                fields.TryGetValue("templname", out templname);
                if (type.Equals(stringType) && templname.Equals(name))
                {
                    templForRemove = t;
                    break;
                }
            }

            if (!string.IsNullOrEmpty(templForRemove)) result = templ.Replace(templForRemove + "&", "");

            return result;
        }
    }
}