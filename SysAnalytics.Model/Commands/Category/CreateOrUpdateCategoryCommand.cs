using System;
using SysAnalytics.Model;
using System.Collections.Generic;
using SysAnalytics.CommandProcessor.Command;

namespace SysAnalytics.Model.Commands
{
    public class CreateOrUpdateCategoryCommand : ICommand
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
