using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SysAnalytics.Model.Commands;
using SysAnalytics.Data.Repositories;
using SysAnalytics.Data.Infrastructure;
using SysAnalytics.Model;
using AutoMapper;
using SysAnalytics.CommandProcessor.Command;

namespace SysAnalytics.Domain.Handlers
{
    public class CreateOrUpdateExpenseHandler : ICommandHandler<CreateOrUpdateExpenseCommand>
    {
        private readonly IMappingEngine mapper;
        private readonly IExpenseRepository expenseRepository;
        private readonly IUnitOfWork unitOfWork;

        public CreateOrUpdateExpenseHandler(IMappingEngine mapper, IExpenseRepository expenseRepository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.expenseRepository = expenseRepository;
            this.unitOfWork = unitOfWork;
        }

        public ICommandResult Execute(CreateOrUpdateExpenseCommand command)
        {
            var expense = this.mapper.Map<Expense>(command);

            if (expense.ExpenseId == 0)
                expenseRepository.Add(expense);
            else
                expenseRepository.Update(expense);

            unitOfWork.Commit();
            return new CommandResult(true);
        }
    }
}
