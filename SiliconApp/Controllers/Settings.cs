using Microsoft.AspNetCore.Mvc;

namespace SiliconApp.Controllers;

public class Settings : Controller
{
    public IActionResult ChangeTheme(string theme) //förväntar oss en parameter in genom deklarerad variabel i js.
    {
        //här vill du sätta en cookie för en bestämd session. 
        var option = new CookieOptions
        {
            Expires = DateTime.Now.AddDays(60),
        };
        Response.Cookies.Append("ThemeMode", theme, option);
        return Ok(); //i js filen, tittar på res.ok för att få ett ok och då kunna ladda om sidan. 
    }

    [HttpPost]
    public IActionResult CookieConsent()
    {
        var option = new CookieOptions
        {
            Expires = DateTime.Now.AddYears(1),
            HttpOnly = true, //den här ska bara vara tillgänglig från servern och inte från webbläsaren! 
            Secure = true
        };
        Response.Cookies.Append("CookieConsent", "true", option);
        return Ok();
    }
}
