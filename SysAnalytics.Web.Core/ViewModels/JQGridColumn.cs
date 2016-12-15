using System;

namespace SysAnalytics.Web.Core.ViewModels
{
    public class JQGridColumn
    {
        public string Name { get; set; }
        public int Width { get; set; }
        public string Align { get; set; }
        public bool IsSortable { get; set; }
        public bool IsHidden { get; set; }
        public string Formatter { get; set; }
        public string Entity { get; set; }
        public string TypeName { get; set; }
    }
}