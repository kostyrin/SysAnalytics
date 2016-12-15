using System.Collections.Generic;
using Newtonsoft.Json;

namespace SysAnalytics.Web.Core.ViewModels
{
    public class JqGridModel
    {
        [JsonProperty("columnsState")]
        public GridSettings GridSettings { get; set; }
        [JsonProperty("action")]
        public string Action { get; set; }
        [JsonProperty("templname")]
        public string TemplName { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("type")]
        public string TypeAction { get; set; }
        [JsonProperty("colStates")]
        public IDictionary<string, ColumnAttribute> ColStates { get; set; }
    }

    public class ColumnAttribute
    {
        [JsonProperty("width")]
        public int Width { get; set; }
        [JsonProperty("hidden")]
        public bool Hidden { get; set; }
        [JsonProperty("formatter")]
        public string Formatter { get; set; }
    }
}