using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace SysAnalytics.Web.Models.Grid
{
    public class Filters
    {
        [JsonProperty("groupOp")]
        public string GroupOp { get; set; }
        [JsonProperty("rules")]
        public Rules[] Rules { get; set; }
    }

    public class Rules
    {
        [JsonProperty("field")]
        public string Field { get; set; }
        [JsonProperty("op")]
        public string Op { get; set; }
        [JsonProperty("data")]
        public string Data { get; set; }
    }
}