using System;
using System.Collections.Generic;
using System.Linq;

namespace SysAnalytics.Common.Converterts
{
    public class JQGridConvert
    {
        public static List<JQGridRows> GetJQGridRows<T>(IEnumerable<T> src)
        {
            var modelTypes = typeof(T);
            var properties = modelTypes.GetProperties();

            int j = 1;
            string[] arr = new string[] { };
            var list = new List<JQGridRows>();
            foreach (var obj in src)
            {
                arr = properties.Select(property => (obj.GetType().GetProperty(property.Name).GetValue(obj, null) ?? string.Empty).ToString()).ToArray();
                list.Add(new JQGridRows { i = j, cell = arr });
            }

            return list;
        }

        public static List<JQGridRows> GetJQGridRowsWithEntity<T1,T2>(IEnumerable<T1> src)
        {
            //var sourceModelTypes = typeof(T1);
            //var sourceProperties = sourceModelTypes.GetProperties();
            var modelTypes = typeof(T2);
            var properties = modelTypes.GetProperties();

            int j = 1;
            //string[] arr = new string[] { };
            var list = new List<JQGridRows>();
            foreach (var obj in src)
            {
                //arr = properties.Select(property => (obj.GetType().GetProperty(property.Name).GetValue(obj, null) ?? string.Empty).ToString()).ToArray();
                List<string> valueCollection = new List<string>();
                foreach (var property in properties)
                {
                    string stringValue = string.Empty;
                    var propNames = property.Name.Split('_');
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
                                childProp = parentValue.GetType().GetProperty(propNames[1]+propNames[2]);
                                if (childProp != null)
                                    stringValue = (childProp.GetValue(parentValue) ?? string.Empty).ToString();
                            }

                        }
                        else
                            stringValue = (parentValue ?? string.Empty).ToString();
                    }
                    valueCollection.Add(stringValue.Replace("1/1/1970 12:00:00 AM +00:00","").Replace("1/1/0001 12:00:00 AM +00:00",""));
                }
                list.Add(new JQGridRows { i = j, cell = valueCollection.ToArray() });
            }

            return list;
        }
    }



    public struct JQGridRows
    {
        public int i { get; set; }
        public string[] cell { get; set; }
    }
}
