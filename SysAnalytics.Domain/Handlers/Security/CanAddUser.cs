using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SysAnalytics.Model.Commands;
using SysAnalytics.Data.Infrastructure;
using SysAnalytics.Data.Repositories;
using SysAnalytics.Core.Common;
using SysAnalytics.Model;
using SysAnalytics.CommandProcessor.Command;

namespace SysAnalytics.Domain.Handlers
{
    public class CanAddUser : IValidationHandler<UserRegisterCommand>
    {
        private readonly IUserRepository userRepository;

        public CanAddUser(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IEnumerable<ValidationResult> Validate(UserRegisterCommand command)
        {
            User isUserExists = null;
            //isUserExists = userRepository.Get(c => c.Email == command.Email);

            if (isUserExists != null)
            {
                yield return new ValidationResult("EMail", SysAnalytics.Domain.Resources.UserExists);
            }
        }
    }
}
