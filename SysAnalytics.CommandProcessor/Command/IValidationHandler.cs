using System.Collections.Generic;
using SysAnalytics.Core.Common;

namespace SysAnalytics.CommandProcessor.Command
{
    public interface IValidationHandler<in TCommand> where TCommand : ICommand
    {
        IEnumerable<ValidationResult>  Validate(TCommand command);
    }
}
