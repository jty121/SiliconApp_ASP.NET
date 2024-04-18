using Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Helpers;

public class UserSessionMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;
    private static bool IsAjaxRequest(HttpRequest request)
    {
        return request.Headers.XRequestedWith == "XMLHttpRequest";
    }
    //asyncrones javascript and xml request, kan uppdatera sidor utan att själva sidan ska ladda om sig. bra vid kundvagnar. 


    public async Task InvokeAsync(HttpContext context, UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager)
    {
        if (context.User.Identity!.IsAuthenticated) //om en användare är autentiserad
        {
            var user = await userManager.GetUserAsync(context.User);
            if (user == null)
            {
                // lägg till en redirect to action här för att skicka till en annan vy
                await signInManager.SignOutAsync();

                //kontrollera om det inte är ett ajaxrequest, och vilken metod det gäller. IgnoreCase är för att ta emot små och stora bokstäver
                if (!IsAjaxRequest(context.Request) && context.Request.Method.Equals("GET", StringComparison.OrdinalIgnoreCase))
                {
                    var signInPath = "/signin";
                    context.Response.Redirect(signInPath);
                    return; //viktigt att ha med return för att programmet inte ska gå vidare i koden utan skickas vidare här. 
                }
            }
        }
        await _next(context);  //annars gör det här = skickas vidare
    }
}

