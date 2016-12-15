using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysAnalytics.Web.ViewModels
{
    public class WriterListViewModel
    {
        public WriterListViewModel()
        {
            //Grid Definitions
            this.GridData = new GridViewModel<WriterFormModel>
            {
                Url = "/Writers/GetWriters",
                UrlTempl = "/Writers/jqGridTempl",
                Id = "WriterSearchGrid",
                Width = 1200,
                Caption = "Writers",
                Height = 400
            };
        }

        public GridViewModel<WriterFormModel> GridData { get; set; }
    }
}