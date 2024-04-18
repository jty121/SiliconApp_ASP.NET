using Microsoft.AspNetCore.Builder;

namespace Infrastructure.Helpers;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseUserSession(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UserSessionMiddleware>();
    }
}

//här kan man lägga till flera olika middleware delar
