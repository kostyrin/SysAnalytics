using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SysAnalytics.Model;
using SysAnalytics.Model.Commands;

namespace SysAnalytics.Data
{
    public class DomainToCommandMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToCommandMappingProfile"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Category, CreateOrUpdateCategoryCommand>();
            Mapper.CreateMap<Category, DeleteCategoryCommand>();
            Mapper.CreateMap<Expense, CreateOrUpdateExpenseCommand>();
            Mapper.CreateMap<Expense, DeleteExpenseCommand>();
            Mapper.CreateMap<User, UserRegisterCommand>().ForMember(x => x.Password, o => o.Ignore());
        }
    }
}
