using System;
using SysAnalytics.Model;
using System.Collections.Generic;
using SysAnalytics.CommandProcessor.Command;

namespace SysAnalytics.Model.Commands
{
    public class CreateOrUpdateExpenseCommand : ICommand
    {
        public int ExpenseId { get; set; }
        public Category Category { get; set; }
        public string TransactionDesc { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
    }
}
