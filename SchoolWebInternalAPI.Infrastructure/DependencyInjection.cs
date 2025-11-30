using Microsoft.Extensions.DependencyInjection;
using SchoolWebInternalAPI.Application.Interfaces;
using SchoolWebInternalAPI.Infrastructure.Repositories;
using SchoolWebInternalAPI.Infrastructure.Services;

namespace SchoolWebInternalAPI.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<ITeacherService, TeacherService>();

        return services;
    }
}
