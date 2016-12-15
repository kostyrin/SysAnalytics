using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysAnalytics.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class CommentCategoryDescriptionAttribute : Attribute
    {
        public string Name { get; set; }
        public bool IsSystemGeneratedOnly { get; set; }
        public bool? IsPositive { get; set; }

        public CommentCategoryDescriptionAttribute(string aName, bool aIsSystemGeneratedOnly, bool aIsPosiitve)
        {
            Name = aName;
            IsSystemGeneratedOnly = aIsSystemGeneratedOnly;
            IsPositive = aIsPosiitve;
        }

        public CommentCategoryDescriptionAttribute(string aName, bool aIsSystemGeneratedOnly)
        {
            Name = aName;
            IsSystemGeneratedOnly = aIsSystemGeneratedOnly;
            IsPositive = null;
        }
    }
}
