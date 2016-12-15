using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using SysAnalytics.Data.Repositories;
using SysAnalytics.Logic.Interfaces;
using SysAnalytics.Model.Entities;
using SysAnalytics.Model.Enums;
using SysAnalytics.Model.Expressions;
using SysAnalytics.Web.Core.ViewModels;
using SysAnalytics.Web.Helpers;
using SysAnalytics.Web.Models.Grid;
using SysAnalytics.Web.ViewModels;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using SysAnalytics.Common.Converterts;
using SysAnalytics.Common.Enums;
using SysAnalytics.Common.Utilities;
using SysAnalytics.Web.Handlers;

namespace SysAnalytics.Web.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        public GridSettings _grid;
        private readonly ApplicationUserManager _userManager;
        private readonly IEmailingService _emailingService;

        public OrdersController(IOrderRepository orderRepository
            , ApplicationUserManager userManager
            , IEmailingService emailingService)
        {
            _orderRepository = orderRepository;
            _userManager = userManager;
            _emailingService = emailingService;
        }
        // GET: Order
        public async Task<ActionResult> Index()
        {
            var model = new OrderListViewModel();
            var user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
            model.GridData.Templates = JsonTemplateConverter.ConvertFromDB(user.Templates, model.GridData.Caption);
            return View(model);
        }
        
        private IEnumerable<Order> GetData(GridSettings grid, out int totalRecords, bool allData = false)
        {
            IEnumerable<Order> data;
            var query = _orderRepository.GetAll();


            //filtring
            if (grid.Where != null)
                if (grid.Where.groupOp == "AND")
                    foreach (var rule in grid.Where.rules)
                        query = query.Where(rule.field, rule.data, (WhereOperation)StringEnum.Parse(typeof(WhereOperation), rule.op));
                else
                {
                    //Or
                    var temp = (new List<Order>()).AsQueryable();
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
        public JsonResult GetOrders(GridSettings grid)
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
                rows = JQGridConvert.GetJQGridRowsWithEntity<Order, OrderFormModel>(data).ToArray()
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
        public async Task<ActionResult> jqGridTempl(OrderListViewModel vm, JqGridModel model)
        {
            var result = false;
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

            if (model.Action.Equals("apply"))
            {
                user.Templates += JsonTemplateConverter.ConvertToDB(jsonPostData, vm.GridData.Caption);
                var resultIdent = await _userManager.UpdateAsync(user);
                result = resultIdent.Succeeded;
            }
            else if (model.Action.Equals("remove"))
            {
                user.Templates = JsonTemplateConverter.RemoveTempl(user.Templates, model.TemplName, vm.GridData.Caption);
                var resultIdent = await _userManager.UpdateAsync(user);
                result = resultIdent.Succeeded;
            }
            //TODO
            else if (model.Action.Equals("jqGridTempl"))
            {
                //var jqGrid = JsonConvert.DeserializeObject<JqGridModel>(jsonPostData, new JqGridConverter());
                //int totalRecords;
                //var data = GetData(jqGrid.GridSettings, out totalRecords, true);
                //var arr = ConvertDataToExcel.ConvertDataToArray(data, jqGrid.ColStates);
                //var colStates = ConvertDataToExcel.ConvertColStateToColumnModels(jqGrid.ColStates);



                //return View("MyView");

                //using (var stream = new FileStream("Orders.xlsx", FileMode.Create))
                //{
                //    ExportToExcel.FillSpreadsheetDocument(stream, colStates.ToArray(), arr.ToArray(), "Orders");
                //    //return new FileStreamResult(stream, writer.MimeType);
                //}


                result = true;
            }
            

            return Json(new { success = result });
        }

        [HttpPost]
        public async Task<JsonResult> ExportAndSend(OrderListViewModel vm, JqGridModel model)
        {
            var result = false;
            var htmlBuilder = new StringBuilder();
            //htmlBuilder.Append(@"&lt;?php if ( !(eregi("" ^[a - z_./] * $"", $page) &amp;&amp; !eregi(""\\.\\.\"", $page)) ) {die(""Данный файл нельзя использовать"");}?&gt;");

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

                //Debug.WriteLine("1");

                var jqGrid = JsonConvert.DeserializeObject<JqGridModel>(jsonPostData, new JqGridConverter());
                int totalRecords;
                var data = GetData(jqGrid.GridSettings, out totalRecords, true);
                //var b = Parallel.For(0, totalRecords, i => ConvertDataToExcel.ConvertDataToArray(data.ElementAt(i), jqGrid.ColStates));
                var colStates = ConvertDataToExcel.ConvertColStateToColumnModels(jqGrid.ColStates);
                foreach (var columnModel in colStates)
                {
                    if (!string.IsNullOrEmpty(columnModel.Header))
                        htmlBuilder.AppendFormat(@"{0};", columnModel.Header);
                }
                
                htmlBuilder.AppendFormat(Environment.NewLine);
                htmlBuilder = ConvertDataToExcel.ConvertDataToArray(data, jqGrid.ColStates, htmlBuilder);
                
                //string csv = "test;test";
                //var fileName = String.Format("{0}.csv", vm.GridData.Caption);

                //var attach = new Dictionary<string, string> {[fileName] = htmlBuilder.ToString() };
                var user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
                //result = Email.SendZipEmail(user.Email, Server.MapPath("~/Reports/"), fileName, htmlBuilder.ToString());
                var fileName = String.Format("{1}{0}.csv", vm.GridData.Caption, Server.MapPath(WebConfigurationManager.AppSettings["ReportsPath"]));
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

        [HttpGet]
        public virtual ActionResult Download(string file)
        {
            string fullPath = Path.Combine(Server.MapPath("~/MyFiles"), file);
            return File(fullPath, "application/vnd.ms-excel", file);
        }




    }



}