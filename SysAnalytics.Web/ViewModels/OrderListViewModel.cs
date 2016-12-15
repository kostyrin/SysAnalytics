using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SysAnalytics.Web.Core.ViewModels;

namespace SysAnalytics.Web.ViewModels
{
    public class OrderListViewModel
    {
        public OrderListViewModel()
        {
            //Grid Definitions
            this.GridData = new GridViewModel<OrderFormModel>();
            this.GridData.Url = "/Orders/GetOrders";
            this.GridData.UrlTempl = "/Orders/jqGridTempl";
            this.GridData.Id = "OrderSearchGrid";
            this.GridData.Width = 1200;
            this.GridData.Caption = "Orders";
            this.GridData.Height = 400;
        }

        public GridViewModel<OrderFormModel> GridData { get; set; }
        
    }
}