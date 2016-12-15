using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SysAnalytics.Data.Repositories;
using SysAnalytics.Core.Common;
using SysAnalytics.Model;
using SysAnalytics.Model.Commands;
using SysAnalytics.CommandProcessor.Command;

namespace SysAnalytics.Domain.Handlers
{
    public class CanChangePassword : IValidationHandler<ChangePasswordCommand>
    {
        private readonly IUserRepository userRepository;

        public CanChangePassword(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IEnumerable<ValidationResult> Validate(ChangePasswordCommand command)
        {
            User user = userRepository.GetById(command.UserId);
            var encoded = Md5Encrypt.Md5EncryptPassword(command.OldPassword);

            //if (!user.PasswordHash.Equals(encoded))
            //{
                yield return new ValidationResult("OldPassword", SysAnalytics.Domain.Resources.Password);
            //}
        }
    }
}
