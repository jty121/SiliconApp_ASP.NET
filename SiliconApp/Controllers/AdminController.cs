using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SiliconApp.Controllers;

[Authorize(Roles = "Admin")]    //bara den som har rollen som administratör ska få tillgång till sidan
public class AdminController : Controller
{
    [Route("/admin")]
    public IActionResult AdminPortal()
    {
        return View();
    }
}
