using System;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using System.Web;
using SysAnalytics.Common.Logging;
using SysAnalytics.Logic.Interfaces;
using SysAnalytics.Logic.Services;
using SysAnalytics.Model;
using SysAnalytics.Data.Infrastructure;
using SysAnalytics.Data.Repositories;
using Microsoft.AspNet.Identity;
using NHibernate;
using AutoMapper;
using Microsoft.Owin.Security;
using SysAnalytics.Model.Entities;
using SysAnalytics.Web.Mapping;
using SysAnalytics.Web.Models;
using SysAnalytics.CommandProcessor.Command;
using SysAnalytics.CommandProcessor.Dispatcher;

namespace SysAnalytics.Web
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacContainer();
            AutoMapperConfiguration.Configure();
            AutoMapperConfigurationWebCore.Configure();
        }

        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();

            //Infrastructure objects
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterAssemblyTypes(typeof(MvcApplication).Assembly).AsImplementedInterfaces();
            builder.RegisterType<DefaultCommandBus>().As<ICommandBus>().InstancePerRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterModule(new AutofacWebTypesModule());

            builder.Register(l => LoggingModule.GetLogger(typeof(Object))).As<ILogger>().InstancePerLifetimeScope();

            //Command Query Responsibility Separation objects
            var services = Assembly.Load("SysAnalytics.Domain");
            builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(ICommandHandler<>)).InstancePerRequest();
            builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(IValidationHandler<>)).InstancePerRequest();

            //Repositories objects
            builder.RegisterAssemblyTypes(typeof(IRepository<Expense>).Assembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(IRepository<Category>).Assembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(IRepository<User>).Assembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(IRepository<Customer>).Assembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(IRepository<Writer>).Assembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(IRepository<Order>).Assembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(IRepository<Emailing>).Assembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(IRepository<EmailingDetail>).Assembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();

            //Logics
            builder.RegisterType<EmailingService>().As<IEmailingService>().InstancePerRequest();
            builder.RegisterType<LifeTimeDiscountService>().As<ILifeTimeDiscountService>().SingleInstance();

            //NHibernate objects
            builder.Register(c => ConnectionHelper.BuildSessionFactory("SysAnalyticsContainer")).As<ISessionFactory>().SingleInstance();
            builder.Register(c => c.Resolve<ISessionFactory>().OpenSession()).InstancePerRequest();

            //Automapper objects
            builder.Register(c => new ConfigurationStore(new TypeMapFactory(), AutoMapper.Mappers.MapperRegistry.Mappers)).AsImplementedInterfaces().SingleInstance();
            builder.Register(c => Mapper.Engine).As<IMappingEngine>().SingleInstance();
            builder.RegisterType<TypeMapFactory>().As<ITypeMapFactory>().SingleInstance();

            //Microsoft Identity objects
            //TODO
            builder.RegisterType<ApplicationUserStore>().As<IUserStore<ApplicationUser>>().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            //builder.RegisterType<ApplicationUser>().InstancePerRequest();
            //builder.RegisterType<DefaultUserRoleStore>().As<IUserRoleStore<AuthUser, string>>().InstancePerRequest();
            //builder.RegisterType<ApplicationUserManager<ApplicationUser, string>>().InstancePerRequest();
            //builder.RegisterType<EmailService>().As<IIdentityMessageService>().InstancePerRequest();
            //builder.RegisterType<SmsService>().As<IIdentityMessageService>().InstancePerRequest();

            builder.RegisterModelBinderProvider();
            builder.RegisterFilterProvider();

            IContainer container = builder.Build();
 
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }        
    }
}
