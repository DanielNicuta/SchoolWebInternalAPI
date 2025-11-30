using Microsoft.Extensions.DependencyInjection;

namespace SchoolWebInternalAPI.Application.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Add MediatR, FluentValidation, etc later if needed
        return services;
    }
}
