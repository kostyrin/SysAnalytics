using System;
using System.Collections.Generic;
using System.Linq;
using SysAnalytics.Web.Core.ViewModels;

namespace SysAnalytics.Web.ViewModels
{
    public class GridViewModel<T>
    {
        public GridViewModel()
        {
            this.Data = new List<T>();
            CreateColumns();
        }

        public string Id { get; set; }
        public string Url { get; set; }
        public string Caption { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string UrlTempl { get; set; }

        public List<T> Data { get; set; }
        public IEnumerable<JQGridColumn> Columns { get; set; }
        public IEnumerable<TemplateViewModel> Templates { get; set; }

        private void CreateColumns()
        {
            var modelType = typeof(T);
            var columns = new List<JQGridColumn>();

            var properties = modelType.GetProperties();
            if (properties != null)
            {
                foreach (var property in properties)
                {
                    var column = new JQGridColumn();
                    column.Name = property.Name;

                    var attrs = Attribute.GetCustomAttributes(property);
                    foreach (var attr in attrs)
                    {
                        var a = attr as JQGridColumnAttribute;
                        if (a != null)
                        {
                            column.Width = a.Width;
                            column.IsHidden = a.IsHidden;
                            column.IsSortable = a.IsSortable;
                            column.Align = a.Align;
                            column.Formatter = a.Formatter;
                            column.Entity = a.Entity;
                            column.TypeName = property.PropertyType.Name;
                        }
                    }
                    columns.Add(column);

                    //if (property.Name.Equals("Customer"))
                    //{
                    //    var customerProperties = property.PropertyType.GetProperties();
                    //    foreach (var customerProperty in customerProperties)
                    //    {
                    //        var customerColumn = new JQGridColumn();
                    //        customerColumn.Name = customerProperty.Name;

                    //        var customerAttrs = Attribute.GetCustomAttributes(customerProperty);
                    //        foreach (var attr in customerAttrs)
                    //        {
                    //            var a = attr as JQGridColumnAttribute;
                    //            if (a != null)
                    //            {
                    //                customerColumn.Width = a.Width;
                    //                customerColumn.IsHidden = a.IsHidden;
                    //                customerColumn.IsSortable = a.IsSortable;
                    //                customerColumn.Align = a.Align;
                    //                customerColumn.Formatter = a.Formatter;
                    //                customerColumn.Entity = a.Entity;
                    //            }
                    //        }
                    //        columns.Add(customerColumn);
                    //    }
                    //}
                }
            }
            Columns = columns;
        }
    }
}