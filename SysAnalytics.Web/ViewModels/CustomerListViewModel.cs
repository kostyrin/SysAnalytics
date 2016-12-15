using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysAnalytics.Web.ViewModels
{
    public class CustomerListViewModel
    {
        public CustomerListViewModel()
        {
            //Grid Definitions
            this.GridData = new GridViewModel<CustomerFormModel>
            {
                Url = "/Customers/GetCustomers",
                UrlTempl = "/Customers/jqGridTempl",
                Id = "CustomerSearchGrid",
                Width = 1200,
                Caption = "Customers",
                Height = 400
            };
        }

        public GridViewModel<CustomerFormModel> GridData { get; set; }
    }
}