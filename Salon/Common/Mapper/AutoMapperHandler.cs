using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Salon.Profiles.User;
using System.Reflection;

namespace Salon.Common.Mapper
{
    public static class AutoMapperHandler
    {
        public static void AutoMapperRegisterService(this IServiceCollection service)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            service.AddSingleton(mapper);
        }
    }
}
