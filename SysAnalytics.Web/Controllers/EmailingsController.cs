using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using SysAnalytics.Common.Converterts;
using SysAnalytics.Data.Repositories;
using SysAnalytics.Logic.Interfaces;
using SysAnalytics.Model;
using SysAnalytics.Model.Entities;
using SysAnalytics.Model.Enums;
using SysAnalytics.Model.Expressions;
using SysAnalytics.Web.Core.ViewModels;
using SysAnalytics.Web.Models.Grid;
using SysAnalytics.Web.ViewModels;

namespace SysAnalytics.Web.Controllers
{
    public class EmailingsController : Controller
    {
        private readonly IEmailingRepository _emailingRepository;
        private readonly IEmailingDetailRepository _emailingDetailRepository;
        private readonly IEmailingService _emailingService;
        private readonly ApplicationUserManager _userManager;

        public EmailingsController(IEmailingRepository emailingRepository
            , IEmailingDetailRepository emailingDetailRepository
            , IEmailingService emailingService
            , ApplicationUserManager userManager)
        {
            _emailingRepository = emailingRepository;
            _emailingDetailRepository = emailingDetailRepository;
            _emailingService = emailingService;
            _userManager = userManager;
        }

        // GET: Emailing
        public ActionResult Index()
        {
            var model = new EmailingListViewModel();
            model.GridData.Templates = new List<TemplateViewModel>();
            //var emailings = _emailingRepository.GetAll().ToList();
            return View(model);
        }

        private IEnumerable<EmailingDetail> GetData(GridSettings grid, out int totalRecords, bool allData = false)
        {
            IEnumerable<EmailingDetail> data;
            var query = _emailingDetailRepository.GetAll();


            //filtring
            if (grid.Where != null)
                if (grid.Where.groupOp == "AND")
                    foreach (var rule in grid.Where.rules)
                        query = query.Where(rule.field, rule.data, (WhereOperation)StringEnum.Parse(typeof(WhereOperation), rule.op));
                else
                {
                    //Or
                    var temp = (new List<EmailingDetail>()).AsQueryable();
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
            else query = query.OrderByDescending(e => e.Emailing.DeliveryDate);

            totalRecords = query.Count();
            if (!allData)
                data = query.Skip((grid.PageIndex - 1) * grid.PageSize).Take(grid.PageSize);
            else
                data = query.AsEnumerable().OrderByDescending(e => e.Emailing.DeliveryDate).ToList();

            return data;
        }

        [HttpPost]
        public async Task<JsonResult> GetEmailings(GridSettings grid)
        {
            //if (!grid.IsSearch) return null;

            int totalRecords;
            var data = GetData(grid, out totalRecords);

            foreach (var emailingDetail in data)
            {
                var emailingInfo = await _emailingService.GetInfoMessage(emailingDetail.EmailId);
                if (emailingInfo == null) continue;
                emailingDetail.State = emailingInfo.State;
                emailingDetail.Opens = emailingInfo.Opens;
                emailingDetail.Clicks = emailingInfo.Clicks;
            }

            var pageSize = grid.PageSize;
            var totalPages = (int)Math.Ceiling(totalRecords / (float)pageSize);

            var jsonData = new
            {
                total = totalPages,
                page = grid.PageIndex,
                records = totalRecords,
                //rows = JQGridConvert.GetJQGridRows(listVm).ToArray()
                rows = JQGridConvert.GetJQGridRowsWithEntity<EmailingDetail, EmalingStatisticFormModel>(data).ToArray()
            };
            
            //convert to JSON and return to client
            //return Json(jsonData, JsonRequestBehavior.AllowGet);
            var jsonResult = Json(jsonData, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public FileResult Export(int i, string action)
        {
            string excelFilename = HttpContext.Request["filename"];
            return new FilePathResult(excelFilename, "xlsx");
        }

        public async Task<ActionResult> Details(int id)
        {
            
            var emailing = _emailingRepository.GetById(id);
            var model = new EmailingDetailsFormModel()
            {
                Template = emailing.Template,
                EmailingId = emailing.EmailingId,
                CreatorId = emailing.CreatorId,
                DeliveryDate = emailing.DeliveryDate,
                Status = emailing.Status,
                Details = new List<EmailingInfoDto>() 
            
            };
            var emailingDetails = _emailingDetailRepository.GetMany(ed => ed.Emailing == emailing);
            foreach (var emailingDetail in emailingDetails)
            {
                var statistic = await _emailingService.GetInfoMessage(emailingDetail.EmailId);
                if (statistic == null) continue;
                statistic.RejectReason = emailingDetail.RejectReason;
                statistic.ResultStatus = emailingDetail.ResultStatus;
                statistic.EmailingDetailId = emailingDetail.EmailingDetailId;
                statistic.EmailId = emailingDetail.EmailId;
                statistic.RecipientId = emailingDetail.RecipientId;
                model.Details.Add(statistic);
            }
            
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> ExportAndSend(EmailingListViewModel vm, JqGridModel model)
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