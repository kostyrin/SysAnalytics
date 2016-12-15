using System;

namespace SysAnalytics.Web.Core.ViewModels
{
    [AttributeUsage(AttributeTargets.Property)]
    public class JQGridColumnAttribute : Attribute
    {
        public int Width { get; set; }
        public string Align { get; set; }
        public bool IsSortable { get; set; }
        public bool IsHidden { get; set; }
        public string Formatter { get; set; }
        public string Entity { get; set; }
    }
}