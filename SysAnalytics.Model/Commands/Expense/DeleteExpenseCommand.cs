using SysAnalytics.CommandProcessor.Command;

namespace SysAnalytics.Model.Commands
{
    public class DeleteExpenseCommand : ICommand
    {
        public int ExpenseId { get; set; }
    }
}
