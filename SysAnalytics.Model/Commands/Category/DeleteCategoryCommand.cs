using SysAnalytics.CommandProcessor.Command;

namespace SysAnalytics.Model.Commands
{
    public class DeleteCategoryCommand : ICommand
    {
        public int CategoryId { get; set; }
    }
}
