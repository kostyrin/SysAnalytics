using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysAnalytics.Common.Enums;
using SysAnalytics.Web.Core.ViewModels;

namespace SysAnalytics.Common.Converterts
{
    public class ConvertDataToExcel
    {
        public static StringBuilder ConvertDataToArray<T>(IEnumerable<T> src, IDictionary<string, ColumnAttribute> colStates, StringBuilder result)
        {
            //var result = new string[];
            var modelTypes = typeof(T);
            var properties = modelTypes.GetProperties();
            //var result = new StringBuilder();

            foreach (var obj in src)
            {
                
                foreach (var columnAttribute in colStates)
                {
                    if (columnAttribute.Value.Hidden) continue;
                    string stringValue = string.Empty;
                    var propNames = columnAttribute.Key.Split(' ');
                    var propParent = obj.GetType().GetProperty(propNames[0]);
                    if (propParent != null)
                    {
                        var parentValue = propParent.GetValue(obj, null);
                        if (propNames.Length > 1 && parentValue != null)
                        {
                            var childProp = parentValue.GetType().GetProperty(propNames[1]);
                            if (childProp != null)
                                stringValue = (childProp.GetValue(parentValue) ?? string.Empty).ToString();
                            else if (propNames.Length == 3)
                            {
                                childProp = parentValue.GetType().GetProperty(propNames[1] + propNames[2]);
                                if (childProp != null)
                                    stringValue = (childProp.GetValue(parentValue) ?? string.Empty).ToString();
                            }
                        }
                        else
                            stringValue = (parentValue ?? string.Empty).ToString();
                    }
                    //Debug.WriteLine(stringValue);
                    //var value = (obj.GetType().GetProperty(columnAttribute.Key).GetValue(obj, null) ?? string.Empty).ToString();
                    result.AppendFormat(stringValue.Replace("1/1/1970 12:00:00 AM +00:00", "")
                                                   .Replace("1/1/0001 12:00:00 AM +00:00", "")
                                                   .Replace("&#39;", "")
                                                   .Replace("{", "{{")
                                                   .Replace("}", "}}")
                                                   .Replace("\"", "")
                                                   .Replace(@"""", "")
                                                   .Replace(@";", "")
                                                   .Replace("&lt;", "")
                                                   .Replace("&amp;", "")
                                                   .Replace("&gt;", "")
                                                   .Replace("\n", "")
                                                   .Replace("\r", ""));
                    result.AppendFormat(";");
                }
                //result.AppendFormat(";");
                result.AppendFormat(Environment.NewLine);
            }
            
            return result;

        }

        public static IEnumerable<ColumnModel> ConvertColStateToColumnModels(IDictionary<string, ColumnAttribute> colStates)
        {
            var result = new List<ColumnModel>();
            
            foreach (var columnAttribute in colStates)
            {
                if (columnAttribute.Value.Hidden) continue;
                

                result.Add(new ColumnModel
                {
                    Header = columnAttribute.Key,
                    Alignment = HorizontalAlignment.Left,
                    Type = ConvertFormaterToDateType(columnAttribute.Value.Formatter)
                });
            }

            return result;
        }

        public static DataType ConvertFormaterToDateType(string formatter)
        {
            switch (formatter)
            {
                case "date":
                    return DataType.Date;
                case "number":
                case "integer":
                    return DataType.Integer;
                default:
                    return DataType.String;
            }
        }
    }
}
