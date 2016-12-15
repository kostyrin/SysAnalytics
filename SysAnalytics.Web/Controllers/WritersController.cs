using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using AutoMapper;
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
    public class WritersController : Controller
    {
        //private readonly IMappingEngine _mapper;
        //private readonly ICommandBus _commandBus;
        private readonly IWriterRepository _writerRepository;
        private readonly ApplicationUserManager _userManager;
        private readonly IEmailingService _emailingService;

        public WritersController(IWriterRepository writerRepository
            , ApplicationUserManager userManager
            , IEmailingService emailingService)
        {
            _writerRepository = writerRepository;
            _userManager = userManager;
            _emailingService = emailingService;
        }
        // GET: Writer
        public async Task<ActionResult> Index()
        {
            var model = new WriterListViewModel();
            var user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
            model.GridData.Templates = JsonTemplateConverter.ConvertFromDB(user.Templates, model.GridData.Caption);
            return View(model);
        }

        private IEnumerable<Writer> GetData(GridSettings grid, out int totalRecords, bool allData = false)
        {
            IEnumerable<Writer> data;
            var query = _writerRepository.GetAll();


            //filtring
            if (grid.Where != null)
                if (grid.Where.groupOp == "AND")
                    foreach (var rule in grid.Where.rules)
                        query = query.Where(rule.field, rule.data, (WhereOperation)StringEnum.Parse(typeof(WhereOperation), rule.op));
                else
                {
                    //Or
                    var temp = (new List<Writer>()).AsQueryable();
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

            query = query.Where(c => c.UserType == UserType.Writer);

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
        public JsonResult GetWriters(GridSettings grid)
        {
            if (!grid.IsSearch) return null;

            int totalRecords;
            var data = GetData(grid, out totalRecords);

            //var listVm = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderFormModel>>(data);
            //if (listVm == null) return Json(null);

            var pageSize = grid.PageSize;
            var totalPages = (int)Math.Ceiling(totalRecords / (float)pageSize);

            var jsonData = new
            {
                total = totalPages,
                page = grid.PageIndex,
                records = totalRecords,
                //rows = JQGridConvert.GetJQGridRows(listVm).ToArray()
                rows = JQGridConvert.GetJQGridRowsWithEntity<Writer, WriterFormModel>(data).ToArray()
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
        public async Task<ActionResult> jqGridTempl(ColumnChooserTemplate temp, WriterListViewModel vm)
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
        public async Task<JsonResult> ExportAndSend(WriterListViewModel vm, JqGridModel model)
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


    }
}