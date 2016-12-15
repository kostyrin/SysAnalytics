using System.Collections.Generic;
using SysAnalytics.Model.Commands;
using SysAnalytics.Core.Common;
using SysAnalytics.Data.Repositories;
using SysAnalytics.Data.Infrastructure;
using SysAnalytics.Model;
using SysAnalytics.CommandProcessor.Command;

namespace SysAnalytics.Domain.Handlers
{
    public class CanAddCategory : IValidationHandler<CreateOrUpdateCategoryCommand>
    {
        private readonly ICategoryRepository categoryRepository;

        public CanAddCategory(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            this.categoryRepository = categoryRepository;
        }

        public IEnumerable<ValidationResult> Validate(CreateOrUpdateCategoryCommand command)
        {
            Category isCategoryExists = null;
            if (command.CategoryId == 0)
                isCategoryExists = categoryRepository.Get(c => c.Name == command.Name);
            else
                isCategoryExists = categoryRepository.Get(c => c.Name == command.Name && c.CategoryId != command.CategoryId);

            if (isCategoryExists != null)
            {
                yield return new ValidationResult("Name", SysAnalytics.Domain.Resources.CategoryExists);
            }
        }
    }
}
