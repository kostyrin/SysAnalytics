using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using SysAnalytics.Web.Core.ViewModels;

namespace SysAnalytics.Web.Models.Grid
{
    public class JqGridConverter : CustomCreationConverter<JqGridModel>
    {
        public override JqGridModel Create(Type objectType)
        {
            return new JqGridModel();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var mappedObj = new JqGridModel();
            // Load JObject from stream
            JObject jObject = JObject.Load(reader);
            // Create target object based on JObject
            
            var colState = jObject["columnsState"].ToString();
            mappedObj.GridSettings = JsonConvert.DeserializeObject<GridSettings>(colState);
            if (colState.Contains("colStates"))
            {
                var col = JsonConvert.DeserializeObject<IDictionary<string, object>>(colState)["colStates"].ToString();
                mappedObj.ColStates = JsonConvert.DeserializeObject<IDictionary<string, ColumnAttribute>>(col);
            }

            if (colState.Contains("filters"))
            {
                var filterJson = JsonConvert.DeserializeObject<IDictionary<string, object>>(colState)["filters"].ToString();
                var filter = JsonConvert.DeserializeObject<JObject>(filterJson);
                mappedObj.GridSettings.Where = filter.ToObject<Filter>();
            }


            return mappedObj;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<KeyValuePair<string, JToken>> GetKeys(JObject obj)
        {
            var list = new List<KeyValuePair<string, JToken>>();
            foreach (var t in obj)
            {
                list.Add(t);
            }
            return list;
        }
    }
}