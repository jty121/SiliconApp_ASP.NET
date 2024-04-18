using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SiliconWebApi.Attributes;


[AttributeUsage(AttributeTargets.All)]
public class UseApiKeyAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();
        var secret = configuration?["ApiKey:Secret"]; //hämtar upp en "secret key" från configfil

        if(!string.IsNullOrEmpty(secret) && context.HttpContext.Request.Query.TryGetValue("key", out var key)) //kontrollera att vår url har ett key attribut i sig
        {
            if(!string.IsNullOrEmpty(key) && secret == key) //kontrollerar att nyckeln inte är tom och att nyckeln och secret key är samma.
            {
                await next();
                return;
            }
        }
        context.Result = new UnauthorizedResult();
    }
}
