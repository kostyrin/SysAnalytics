using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysAnalytics.Web.ViewModels
{
    public class EmailingListViewModel
    {
        public EmailingListViewModel()
        {
            //Grid Definitions
            this.GridData = new GridViewModel<EmalingStatisticFormModel>();
            this.GridData.Url = "/Emailings/GetEmailings";
            this.GridData.UrlTempl = "/Emailings/jqGridTempl";
            this.GridData.Id = "EmailingSearchGrid";
            this.GridData.Width = 800;
            this.GridData.Caption = "Emailings";
            this.GridData.Height = 400;
        }

        public GridViewModel<EmalingStatisticFormModel> GridData { get; set; }
    }
}