using SysAnalytics.Model.Commands;
using SysAnalytics.Data.Repositories;
using SysAnalytics.Data.Infrastructure;
using SysAnalytics.CommandProcessor.Command;

namespace SysAnalytics.Domain.Handlers
{
    public class DeleteCategoryHandler : ICommandHandler<DeleteCategoryCommand>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IUnitOfWork unitOfWork;

        public DeleteCategoryHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            this.categoryRepository = categoryRepository;
            this.unitOfWork = unitOfWork;
        }

        public ICommandResult Execute(DeleteCategoryCommand command)
        {
            var category = categoryRepository.GetById(command.CategoryId);
            categoryRepository.Delete(category);
            unitOfWork.Commit();
            return new CommandResult(true);
        }
    }
}
