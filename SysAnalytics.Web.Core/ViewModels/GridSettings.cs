using System.Web.Mvc;
using Newtonsoft.Json;
using SysAnalytics.Web.Core.Extensions;

namespace SysAnalytics.Web.Core.ViewModels
{
    [ModelBinder(typeof(GridModelBinder))]
    public class GridSettings
    {
        [JsonProperty("search")]
        public bool IsSearch { get; set; }
        public int PageSize { get; set; }
        [JsonProperty("page")]
        public int PageIndex { get; set; }
        [JsonProperty("sortname")]
        public string SortColumn { get; set; }
        [JsonProperty("sortorder")]
        public string SortOrder { get; set; }
        [JsonProperty("permutation")]
        public int[] Permutation { get; set; }
        //[JsonProperty("filters")]
        public Filter Where { get; set; }
    }
}