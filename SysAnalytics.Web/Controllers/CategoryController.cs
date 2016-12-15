using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SysAnalytics.Web.ViewModels;
using SysAnalytics.Model.Commands;
using SysAnalytics.Core.Common;
using SysAnalytics.Web.Core.Extensions;
using SysAnalytics.Data.Repositories;
using SysAnalytics.Web.Core.ActionFilters;
using AutoMapper;
using SysAnalytics.Web.Core.ViewModels;
using SysAnalytics.CommandProcessor.Dispatcher;
using SysAnalytics.Web.Core.ActionFilters;

namespace SysAnalytics.Web.Controllers
{
    [CompressResponse]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IMappingEngine mapper;
        private readonly ICommandBus commandBus;
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICommandBus commandBus, IMappingEngine mapper, ICategoryRepository categoryRepository)
        {
            this.mapper = mapper;
            this.commandBus = commandBus;
            this.categoryRepository = categoryRepository;
        }

        public ActionResult Index()
        {
            var categories = categoryRepository.GetAll();
            return View(categories);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {

            var category = categoryRepository.GetById(id);
            var viewModel = new CategoryFormModel(category);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CategoryFormModel form)
        {
            if (ModelState.IsValid)
            {
                //TODO: Create Automapper form => command
                var command = new CreateOrUpdateCategoryCommand() 
                { 
                    CategoryId = form.CategoryId, 
                    Name = form.Name, 
                    Description = form.Description 
                };

                IEnumerable<ValidationResult> errors = commandBus.Validate(command);
                ModelState.AddModelErrors(errors);
                if (ModelState.IsValid)
                {
                    var result = commandBus.Submit(command);
                    if (result.Success) return RedirectToAction("Index");
                }
            }

            if (form.CategoryId == 0)
                return View("Create", form);
            else
                return View("Edit", form);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var command = new DeleteCategoryCommand { CategoryId = id };
            var result = commandBus.Submit(command);
            var categories = categoryRepository.GetAll();
            return PartialView("_CategoryList", categories);
        }
    }
}
