using AutoMapper;

namespace SysAnalytics.Web.Mapping
{
    public class AutoMapperConfigurationWebCore
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<DomainToViewModelMappingProfile>();
            });
        }
    }
}
