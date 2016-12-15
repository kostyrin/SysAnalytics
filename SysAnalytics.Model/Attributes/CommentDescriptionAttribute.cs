using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysAnalytics.Model.Enums;

namespace SysAnalytics.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class CommentDescriptionAttribute : Attribute
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public CommentCategory Category { get; set; }
        public WriterMistakeType WriterMistakeType { get; set; }

        //public bool IsSystemComment
        //{
        //    get
        //    {
        //        var lAttr = Writers.Library.ReflectionHelper.GetMemberAttribute<CommentCategoryDescriptionAttribute>(() => Category);
        //        return lAttr != null && lAttr.IsSystemGeneratedOnly;
        //    }
        //}

        //public bool IsPositive
        //{
        //    get
        //    {
        //        var lAttr = Writers.Library.ReflectionHelper.GetEnumAttribute<CommentCategory, CommentCategoryDescriptionAttribute>(() => Category);
        //        if (lAttr != null)
        //            return lAttr.IsPositive ?? false;

        //        return false;
        //    }
        //}

        public CommentDescriptionAttribute(string aName, string aAbbreviation, CommentCategory aCategory, WriterMistakeType aWriterMistakeType)
        {
            Name = aName;
            Abbreviation = aAbbreviation;
            Category = aCategory;
            WriterMistakeType = aWriterMistakeType;
        }

        public CommentDescriptionAttribute(string aName, string aAbbreviation, CommentCategory aCategory)
        {
            Name = aName;
            Abbreviation = aAbbreviation;
            Category = aCategory;
            WriterMistakeType = WriterMistakeType.Unknown;
        }

        public CommentDescriptionAttribute(string aName, string aAbbreviation)
        {
            Name = aName;
            Abbreviation = aAbbreviation;
            Category = CommentCategory.Other;
            WriterMistakeType = WriterMistakeType.Unknown;
        }

        public CommentDescriptionAttribute(string aName)
        {
            Name = aName;
            Abbreviation = "Manual";
            Category = CommentCategory.Other;
            WriterMistakeType = WriterMistakeType.Unknown;
        }
    }
}
