using AutoMapper;
using SchoolWebInternalAPI.Application.Mapping;

namespace SchoolWebInternalAPI.Tests.Mapping
{
    public class PagesProfileTests
    {
        [Fact]
        public void AutoMapper_Profile_IsValid()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PagesProfile>();
            });

            config.AssertConfigurationIsValid();
        }
    }
}
