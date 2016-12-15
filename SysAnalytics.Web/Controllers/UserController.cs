using AutoMapper;
using SysAnalytics.Data.Repositories;
using SysAnalytics.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SysAnalytics.Core.Common;
using SysAnalytics.Model;
using SysAnalytics.Model.Enums;
using SysAnalytics.Model.Expressions;
using SysAnalytics.Web.Core.ViewModels;
using SysAnalytics.Web.Helpers;
using SysAnalytics.Web.Models.Grid;
using SysAnalytics.CommandProcessor.Dispatcher;

namespace SysAnalytics.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IMappingEngine _mapper;
        private readonly ICommandBus _commandBus;
        private readonly IUserRepository _userRepository;

        public UserController(ICommandBus commandBus, IMappingEngine mapper, IUserRepository userPerository)
        {
            _mapper = mapper;
            _commandBus = commandBus;
            _userRepository = userPerository;
        }

        // GET: User
        public ActionResult Index()
        {
            //var model = new List<UserFormModel>();
            //var users = _userRepository.GetAll().Take(100).ToList();
            //var model = Mapper.Map<IEnumerable<User>, IEnumerable<UserFormModel>>(users);
            //var users = _userRepository.GetAll().ToList();
            //users.ForEach(u => model.Add(new UserFormModel { Email = u.Email }));
            return View();
        }

        public JsonResult GetUsers(GridSettings grid)
        {

            var query = _userRepository.GetAll();

            //filtring
            if (grid.IsSearch)
            {
                //And
                if (grid.Where.groupOp == "AND")
                    foreach (var rule in grid.Where.rules)
                        query = query.Where<User>(
                            rule.field, rule.data,
                            (WhereOperation)StringEnum.Parse(typeof(WhereOperation), rule.op));
                else
                {
                    //Or
                    var temp = (new List<User>()).AsQueryable();
                    foreach (var rule in grid.Where.rules)
                    {
                        var t = query.Where<User>(
                        rule.field, rule.data,
                        (WhereOperation)StringEnum.Parse(typeof(WhereOperation), rule.op));
                        temp = temp.Concat<User>(t);
                    }
                    //remove repeating records
                    query = temp.Distinct<User>();
                }
            }

            //sorting
            query = query.OrderBy<User>(grid.SortColumn, grid.SortOrder);

            //count
            var count = query.Count();

            //paging
            var data = query.Skip((grid.PageIndex - 1) * grid.PageSize).Take(grid.PageSize).ToArray();

            //converting in grid format
            var result = new
            {
                total = (int)Math.Ceiling((double)count / grid.PageSize),
                page = grid.PageIndex,
                records = count,
                rows = data.ToArray()
            };

            //convert to JSON and return to client
            return Json(result, JsonRequestBehavior.AllowGet);

        }
    }
}