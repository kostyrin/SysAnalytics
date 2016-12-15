using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using AutoMapper;
using SysAnalytics.Common.Logging;
using SysAnalytics.Data.Repositories;
using SysAnalytics.Logic.Interfaces;
using SysAnalytics.Model.Entities;
using SysAnalytics.Model.Enums;
using SysAnalytics.Model.Expressions;
using SysAnalytics.Web.Helpers;
using SysAnalytics.Web.Models.Grid;
using SysAnalytics.Web.ViewModels;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SysAnalytics.Common.Converterts;
using SysAnalytics.Common.Utilities;
using SysAnalytics.Model;
using SysAnalytics.Web.Core.ViewModels;
using SysAnalytics.CommandProcessor.Dispatcher;

namespace SysAnalytics.Web.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ApplicationUserManager _userManager;
        private readonly ILifeTimeDiscountService _lifeTimeDiscountService;
        private readonly IEmailingService _emailingService;
        private readonly ILogger _log;

        public CustomersController(ICustomerRepository customerRepository
            , ApplicationUserManager userManager
            , ILifeTimeDiscountService lifeTimeDiscountService
            , IEmailingService emailingService
            , ILogger log)

        {
            _customerRepository = customerRepository;
            _userManager = userManager;
            _lifeTimeDiscountService = lifeTimeDiscountService;
            _emailingService = emailingService;
            _log = log;
        }
        // GET: Customer
        public async Task<ActionResult> Index()
        {
            var model = new CustomerListViewModel();
            var user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
            model.GridData.Templates = JsonTemplateConverter.ConvertFromDB(user.Templates, model.GridData.Caption);
            return View(model);
        }

        private IEnumerable<Customer> GetData(GridSettings grid, out int totalRecords, bool allData = false)
        {
            IEnumerable<Customer> data;
            var query = _customerRepository.GetAll();


            //filtring
            if (grid.Where != null)
                if (grid.Where.groupOp == "AND")
                    foreach (var rule in grid.Where.rules)
                        query = query.Where(rule.field, rule.data, (WhereOperation)StringEnum.Parse(typeof(WhereOperation), rule.op));
                else
                {
                    //Or
                    var temp = (new List<Customer>()).AsQueryable();
                    foreach (var rule in grid.Where.rules)
                    {
                        var t = query.Where(
                        rule.field, rule.data,
                        (WhereOperation)StringEnum.Parse(typeof(WhereOperation), rule.op));
                        temp = temp.Concat(t);
                    }
                    //remove repeating records
                    query = temp.Distinct();
                }

            query = query.Where(c => c.UserType == UserType.Customer);

            //sorting
            if (!string.IsNullOrEmpty(grid.SortColumn)) query = query.OrderBy(grid.SortColumn, grid.SortOrder);

            totalRecords = query.Count();
            if (!allData)
                data = query.Skip((grid.PageIndex - 1) * grid.PageSize).Take(grid.PageSize);
            else
                data = query.AsEnumerable().ToList();

            

            return data;
        }

        [HttpPost]
        public JsonResult GetCustomers(GridSettings grid)
        {
            if (!grid.IsSearch) return null;

            int totalRecords;
            var data = GetData(grid, out totalRecords);
            

            var pageSize = grid.PageSize;
            var totalPages = (int)Math.Ceiling(totalRecords / (float)pageSize);

            var jsonData = new
            {
                total = totalPages,
                page = grid.PageIndex,
                records = totalRecords,
                //rows = JQGridConvert.GetJQGridRows(listVm).ToArray()
                rows = JQGridConvert.GetJQGridRowsWithEntity<Customer, CustomerFormModel>(data).ToArray()
            };


            //convert to JSON and return to client
            //return Json(jsonData, JsonRequestBehavior.AllowGet);
            var jsonResult = Json(jsonData, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public FileResult Export(int i)
        {
            string excelFilename = HttpContext.Request["filename"];
            return new FilePathResult(excelFilename, "xlsx");
        }

        [HttpPost]
        public async Task<ActionResult> jqGridTempl(ColumnChooserTemplate temp, CustomerListViewModel vm)
        {
            string jsonPostData;
            using (var stream = Request.InputStream)
            {
                stream.Position = 0;
                using (var reader = new System.IO.StreamReader(stream))
                {
                    jsonPostData = reader.ReadToEnd();
                }
            }

            var user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
            user.Templates += JsonTemplateConverter.ConvertToDB(jsonPostData, vm.GridData.Caption);
            var result = await _userManager.UpdateAsync(user);

            return Json(new { success = result.Succeeded });
        }


        [HttpPost]
        public async Task<JsonResult> ExportAndSend(CustomerListViewModel vm, JqGridModel model)
        {
            var result = false;
            var htmlBuilder = new StringBuilder();

            try
            {
                string jsonPostData;
                using (var input = Request.InputStream)
                {
                    input.Position = 0;
                    using (var reader = new System.IO.StreamReader(input))
                    {
                        jsonPostData = reader.ReadToEnd();
                    }
                }

                var jqGrid = JsonConvert.DeserializeObject<JqGridModel>(jsonPostData, new JqGridConverter());
                int totalRecords;
                var data = GetData(jqGrid.GridSettings, out totalRecords, true);
                var colStates = ConvertDataToExcel.ConvertColStateToColumnModels(jqGrid.ColStates);
                foreach (var columnModel in colStates)
                {
                    if (!string.IsNullOrEmpty(columnModel.Header))
                        htmlBuilder.AppendFormat(@"{0};", columnModel.Header);
                }

                htmlBuilder.AppendFormat(Environment.NewLine);
                htmlBuilder = ConvertDataToExcel.ConvertDataToArray(data, jqGrid.ColStates, htmlBuilder);

                var fileName = String.Format("{1}{0}.csv", vm.GridData.Caption, Server.MapPath(WebConfigurationManager.AppSettings["ReportsPath"]));

                //var attach = new Dictionary<string, string> {[fileName] = htmlBuilder.ToString() };
                var user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
                //result = Email.SendZipEmail(user.Email, Server.MapPath("~/Reports/"), fileName, htmlBuilder.ToString());
                var resp = await _emailingService.SendMessage(user.Email, fileName, htmlBuilder.ToString());
                result = resp.RejectReason == null;
            }
            catch (Exception ex)
            {
                //_log.Error(ex);
                return new JsonResult
                {
                    Data = new { ErrorMessage = ex.Message, Success = false },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };
            }

            return new JsonResult() { Data = new { Success = result }, };
        }
    }
}