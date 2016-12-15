using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace SysAnalytics.Web.Models.Grid
{
    //[ModelBinder(typeof(JsonBinderAttribute.ColumnChooserModelBinder))]
    [JsonObject]
    public class ColumnChooserTemplate
    {
        //[JsonProperty("search")]
        //public bool Search { get; set; }
        //[JsonProperty("page")]
        //public int Page { get; set; }
        [JsonProperty("templname")]
        public string TemplName { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("action")]
        public string Action { get; set; }
        //[JsonProperty("permutation")]
        //public string Permutation { get; set; }
        //[JsonProperty("colStates")]
        //public string ColStates { get; set; }
        //public string[] Content { get; set; }
    }
}